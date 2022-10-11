using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallRogue
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControllerRB : MonoBehaviour
    {
        [Header("Player moving velocity")]
        public float velocity = 1f;

        public LayerMask obstacleLayer; 
        
        private Rigidbody2D _rb;

        private Transform _tr;

        public Transform _targetTransform;

        private bool _moving = false;
        private void Awake()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _tr = GetComponent<Transform>();
        }
        // Start is called before the first frame update
        void Start()
        {
            _targetTransform.parent = null;
            
        }

        bool CanMove(Vector2 direction, float deltaPosition)
        {
            Debug.DrawLine(_tr.position, 
                _tr.position+
                Mathf.Sign(deltaPosition) * (Vector3)direction);
            RaycastHit2D hit = Physics2D.Raycast(_tr.position, 
                direction, 
                deltaPosition+Mathf.Sign(deltaPosition)*0.5f,
                obstacleLayer);

            // RaycastHit2D hit = Physics2D.BoxCast(_tr.position,
            //     Vector2.one*0.95f, 0f, direction, deltaPosition,
            //     obstacleLayer);
            
            if (hit.collider != null)
            {
                Debug.Log("HIT A "+hit.collider.gameObject.name);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Update()
        {
            _tr.position = Vector3.MoveTowards(
                _tr.position,
                _targetTransform.position,
                velocity * Time.deltaTime);

            if (Vector3.Distance(
                _tr.position,
                _targetTransform.position)<=0.01f)
            {
                float horizontalMovement =
                    Input.GetAxisRaw("Horizontal");
                float verticalMovement =
                    Input.GetAxisRaw("Vertical");
                if (horizontalMovement != 0)
                    verticalMovement = 0;

                Vector3 direction;

                if (horizontalMovement != 0)
                {
                    direction = Vector3.right
                                * Mathf.Sign(horizontalMovement);
                }
                else if (verticalMovement!=0)
                {
                    direction = Vector3.up
                                * Mathf.Sign(verticalMovement);
                }
                else
                {
                    direction = Vector3.zero;
                }

                Vector3 next_position = 
                    _targetTransform.position + direction;

                if (!Physics2D.OverlapCircle(next_position, .1f, obstacleLayer))
                {
                    _targetTransform.position = next_position;
                }

            }
                
        }
        
        // Turn-based movement
        // void Update()
        // {
        //     if (_moving)
        //         return;
        //     
        //     float horizontalMovement = Input.GetAxisRaw("Horizontal");
        //     float verticalMovement = Input.GetAxisRaw("Vertical");
        //
        //     if (horizontalMovement != 0)
        //         verticalMovement = 0;
        //
        //     float deltaPosition = 0f;
        //     Vector2 direction = Vector2.zero;
        //     Vector3 destination = Vector3.zero; 
        //     if (horizontalMovement != 0)
        //     {
        //         // deltaPosition = horizontalMovement * velocity * Time.deltaTime;
        //         direction = Vector2.right;
        //         destination = 
        //             _tr.position +
        //             (Vector3) direction * Mathf.Sign(horizontalMovement);
        //     }
        //     else if (verticalMovement != 0)
        //     {
        //         deltaPosition = verticalMovement * velocity * Time.deltaTime;
        //         direction = Vector2.up;
        //         destination =
        //             _tr.position + (Vector3)
        //             direction * Mathf.Sign(verticalMovement);
        //     }
        //     else
        //     {
        //         direction = Vector2.zero;
        //         deltaPosition = 0f;
        //     }
        //
        //     if (direction!=Vector2.zero && CanMove(direction, 1f))
        //     {
        //         StartCoroutine(SmoothMove((Vector3) destination,.2f));
        //         // continuous moving
        //         //
        //         // _tr.position = transform.position
        //         //                + (Vector3) direction * deltaPosition;
        //     }
        // }

        IEnumerator SmoothMove(Vector3 target, float time)
        {
            float inverseTime = 1 / time;
            
            _moving = true;

            float distanceFromTarget =
                (_tr.position - target).sqrMagnitude;

            while (distanceFromTarget > float.Epsilon)
            {
                Vector3 next_position =
                    Vector3.MoveTowards(_tr.position,
                        target,
                        Time.deltaTime * inverseTime);
                _tr.position = next_position;
                distanceFromTarget =
                    (_tr.position - target).sqrMagnitude;
                yield return null;
            }

            _tr.position = target;
            _moving = false;
            yield return null;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Goal"))
            {
                print("YOU WIN!");
            }
            else if (other.CompareTag("Enemy"))
            {
                print("YOU LOOSE!");
            } else if (other.CompareTag("Obstacle"))
            {
                print("YOU LOOSE!");
            }
        }
    }
    
}
