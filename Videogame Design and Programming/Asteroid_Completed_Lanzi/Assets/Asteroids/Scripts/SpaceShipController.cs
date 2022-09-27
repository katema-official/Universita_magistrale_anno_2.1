using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] private float thrustForce = 1f;
    [SerializeField] private float rotationSpeed = 45f;

    private Rigidbody2D _rigidbody;
    private SpaceShipThrust _spaceShipThrust;
    private GameObject _spaceShipThrustObject;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody)
        {
            _rigidbody.gravityScale = 0f;
        }
        
        _spaceShipThrust = GetComponentInChildren<SpaceShipThrust>();
        if (_spaceShipThrust)
        {
            _spaceShipThrustObject = _spaceShipThrust.gameObject;
            _spaceShipThrustObject.SetActive(false);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody.AddForce(transform.up*thrustForce, ForceMode2D.Impulse);
            _spaceShipThrustObject.SetActive(true);
        }
        else
        {
            _spaceShipThrustObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.MoveRotation(_rigidbody.rotation + rotationSpeed*Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.MoveRotation(_rigidbody.rotation - rotationSpeed*Time.deltaTime);
        }
        
    }
    
    
}
