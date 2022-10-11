using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frogger
{

    public class Player : MonoBehaviour
    {
        [Header("Player moving velocity")] public float velocity = 1f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            transform.position = transform.position
                                 + horizontalMovement * transform.right * velocity * Time.deltaTime
                                 + verticalMovement * transform.up * velocity * Time.deltaTime;
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
            }
            else if (other.CompareTag("Obstacle"))
            {
                print("YOU LOOSE!");
            }
        }
    }

}
