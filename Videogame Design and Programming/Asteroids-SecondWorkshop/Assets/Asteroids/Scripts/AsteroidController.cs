using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public enum Size
    {
        Large=2, Medium=1, Small=0
    }

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 90f;
    private Size _size = Size.Large;

    private Transform _transform;

    private Vector3 _direction = Vector3.zero;
    private float _rotationDirection = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _direction = Vector3.right;
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        if (Random.value < 0.5)
        {
            _rotationDirection = 1;
        }
        else
        {
            _rotationDirection = -1;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void setSize(Size size)
    {
        _size = size;
    }

    public void setScale(float scale)
    {
        _transform.localScale = new Vector3(scale, scale, _transform.localScale.z);
    }

    public void setSpeed(float speed)
    {
        _speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _transform.position = _transform.position + _speed * Time.fixedDeltaTime * _direction;
        _transform.Rotate(0f, 0f, _rotationSpeed * Time.fixedDeltaTime * _rotationDirection);
    }
}
