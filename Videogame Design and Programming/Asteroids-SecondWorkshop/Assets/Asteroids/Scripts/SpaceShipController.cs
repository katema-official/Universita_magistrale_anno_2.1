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

    private bool _thrust = false;
    private float _rotationMovement = 0f;

    [SerializeField] private MissileController _missileController;
    [SerializeField] private Transform _firingPosition;

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
        _thrust = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _thrust = true;
            _spaceShipThrustObject.SetActive(true);
        }
        else
        {
            _spaceShipThrustObject.SetActive(false);
        }

        _rotationMovement = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rotationMovement = rotationSpeed;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rotationMovement = -rotationSpeed;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            MissileController missile = Instantiate(_missileController, 
                _firingPosition.position, 
                _firingPosition.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (_thrust)
        {
            _rigidbody.AddForce(transform.up * thrustForce, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(_rotationMovement) > float.Epsilon)
        {
            _rigidbody.MoveRotation(_rigidbody.rotation + _rotationMovement * Time.fixedDeltaTime);
        }
        
    }


}
