using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _velocity = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position =
                transform.position + _velocity * Time.deltaTime * Vector3.right;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position =
                transform.position - _velocity * Time.deltaTime * Vector3.right;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position =
                transform.position + _velocity * Time.deltaTime * Vector3.up;
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position =
                transform.position - _velocity * Time.deltaTime * Vector3.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            Debug.Log("YUMMI");
            Destroy(col.gameObject);
        }
    }
}
