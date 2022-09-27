using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POLIMIGameCollective;

public class AsteroidFieldManager : MonoBehaviour
{

    [SerializeField] AsteroidController[] _asteroidControllers;
    private float _top;
    private float _bottom;
    private float _left;
    private float _right;
    private float _width;
    private float _height;

    [SerializeField] private float _margin = 1f;

    [Header("Percentage of screen as target")]
    [Range(0.25f, 0.75f)]
    [SerializeField] private float _targetAreaRatio = .5f;

    [SerializeField] private AsteroidFieldParameters _asteroidFieldParameters;

    private float _targetAreaRay;

    private void Awake()
    {
        ScreenBounds.ComputeScreenBounds();
        _top = ScreenBounds.top;
        _bottom = ScreenBounds.bottom;
        _left = ScreenBounds.left;
        _right = ScreenBounds.right;
        _width = _right - _left + 2*_margin;
        _height = _top - _bottom + 2*_margin;

        _targetAreaRay = _targetAreaRatio * _height;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            CreateAsteroid(AsteroidController.Size.Large);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetStartPosition()
    {
        Vector3 position = Vector3.zero;

        Vector3 unit = (Random.insideUnitCircle).normalized;
        position.x = unit.x * _width;
        position.y = unit.y * _height;
        position.z = 0;

        return position;
    }

    private void CreateAsteroid(AsteroidController.Size size)
    {
        Vector3 position = GetStartPosition();
        Vector3 target = GetTargetPosition();

        Vector3 direction = (target - position).normalized;
        int asteroidType = (int) (_asteroidControllers.Length * Random.value);

        AsteroidController asteroid = Instantiate(_asteroidControllers[asteroidType], position, Quaternion.identity);
        asteroid.SetDirection(direction);
        SetUpAsteroid(ref asteroid, size);


    }

    private void SetUpAsteroid(ref AsteroidController asteroidController,
                                AsteroidController.Size size)   //ref esplicita il fatto che voglio modificare il riferimento
    {
        switch (size)
        {
            case AsteroidController.Size.Large:
                asteroidController.setSize(AsteroidController.Size.Large);
                asteroidController.setSpeed(_asteroidFieldParameters.LargeAsteroidSpeed);
                asteroidController.setScale(_asteroidFieldParameters.LargeAsteroidScale);
            break;
            case AsteroidController.Size.Medium:
                asteroidController.setSize(AsteroidController.Size.Large);
                asteroidController.setSpeed(_asteroidFieldParameters.MediumAsteroidSpeed);
                asteroidController.setScale(_asteroidFieldParameters.MediumAsteroidScale);
            break;
            case AsteroidController.Size.Small:
                asteroidController.setSize(AsteroidController.Size.Large);
                asteroidController.setSpeed(_asteroidFieldParameters.SmallAsteroidSpeed);
                asteroidController.setScale(_asteroidFieldParameters.SmallAsteroidScale);
            break;

        }
    }


    private Vector3 GetTargetPosition()
    {
        Vector3 position = Vector3.zero;
        Vector3 unit = (Vector3) (Random.insideUnitCircle).normalized;
        position = unit * _targetAreaRay;
        return position;
    }
}
