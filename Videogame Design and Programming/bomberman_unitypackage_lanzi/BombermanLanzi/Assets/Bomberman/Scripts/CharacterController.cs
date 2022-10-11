using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //v pressed when dragging more objects
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed = 5f;
    private Vector2 _movement = Vector2.zero;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + speed * Time.fixedDeltaTime * _movement);

    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("HorizontalMovement", _movement.x);
        _animator.SetFloat("VerticalMovement", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

}
