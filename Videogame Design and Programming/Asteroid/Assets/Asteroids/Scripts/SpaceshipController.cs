using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    [SerializeField] private float thrustForce = 1f;
    [SerializeField] private float rotationSpeed = 45f;
    private Rigidbody2D _rigidbody;
    public GameObject ThrustSprite;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody.AddForce(transform.up * thrustForce, ForceMode2D.Impulse);
            ThrustSprite.SetActive(true);
        }
        else
        {
            ThrustSprite.SetActive(false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rigidbody.AddForce(-transform.up * thrustForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.MoveRotation(_rigidbody.rotation - rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.MoveRotation(_rigidbody.rotation + rotationSpeed * Time.deltaTime);
        }
    }
}
