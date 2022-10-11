using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Bandit : MonoBehaviour
{

    [SerializeField] private float maxHealth = 10f;
    private float _currentHealth;
    [SerializeField] private bool active = false;
    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayerMask;
    private int _attackedEnemies = 0;
    private bool _performingAttacks = false;
    
    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private BoxCollider2D       m_boxCollider2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        m_boxCollider2d = GetComponent<BoxCollider2D>();
        
        _currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!active)
            return;

        _performingAttacks = _attackedEnemies != 0;
        
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if(Input.GetMouseButtonDown(0)) {
            if (!_performingAttacks)
            {
                m_animator.SetTrigger("Attack");
                ExecuteAttack();
            }
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null && active)
        {
            Gizmos.DrawSphere(attackPoint.position, attackRange);
        }
    }

    private void ExecuteAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayerMask);
        if (enemies.Length!=0)
        {
            _attackedEnemies = 0;
            for (int i = 0; i < enemies.Length; i++)
            {
                Debug.Log("HIT "+enemies[i].gameObject.name);
                Bandit enemy = enemies[i].gameObject.GetComponent<Bandit>();
                enemy.ReceiveDamage(2);
            }
        }
        else
        {
            Debug.Log("HIT NOTHING!");
        }
    }

    public void ReceiveDamage(int points)
    {
        StartCoroutine(ReceiveDamageCoroutine(points));
    }

    private IEnumerator ReceiveDamageCoroutine(int points)
    {
        _attackedEnemies = _attackedEnemies + 1;
        _currentHealth = _currentHealth - points;
        
        m_animator.SetTrigger("Hurt");
        
        if (_currentHealth <= 0)
        {
            yield return new WaitForSeconds(.5f);
            m_animator.SetTrigger("Death");
            m_boxCollider2d.enabled = false;
        }
        
        _attackedEnemies = _attackedEnemies - 1;
    }
}
