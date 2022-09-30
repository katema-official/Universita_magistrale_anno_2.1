using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POLIMIGameCollective;

public class AsteroidFieldManager : MonoBehaviour
{
    [SerializeField] AsteroidController[] _asteroidControllers; //initialized from outside, from the unity editor. These are the AsteroidController components
                                                                //of the asteroid prefabs you are going to use.
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

    [SerializeField] private AsteroidFieldParameters _asteroidFieldParameters;  //the parameters we are going to use for this AsteroidFieldManager are saved here!

    private float _targetAreaRay;
    private int _numberOfActiveAsteroids = 0;

    [SerializeField] private GameObject ExplosionParticleEffect;

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
        
    }

    public void PlayLevel(AsteroidLevel gameLevel)
    {
        for (int i = 0; i < gameLevel.NumberOfLargeAsteroids; i++)
        {
            CreateAsteroid(AsteroidController.Size.Large);
            
        }
    }

    private void CreateOneAsteroid(AsteroidController.Size size, Vector3 position)
    {
        int asteroidType = (int)(_asteroidControllers.Length * Random.value);

        AsteroidController asteroid = Instantiate(_asteroidControllers[asteroidType], position, Quaternion.identity);

        Vector3 direction = Random.insideUnitCircle.normalized;
        asteroid.SetDirection(direction);
        SetUpAsteroid(ref asteroid, size);
        _numberOfActiveAsteroids += 1;
    }

    public void AsteroidDestroyed(AsteroidController.Size size, Vector3 position)
    {
        Instantiate(ExplosionParticleEffect, position, Quaternion.identity);

        if(size == AsteroidController.Size.Large)
        {
            CreateOneAsteroid(AsteroidController.Size.Medium, position);
            CreateOneAsteroid(AsteroidController.Size.Medium, position);
        }
        if (size == AsteroidController.Size.Medium)
        {
            CreateOneAsteroid(AsteroidController.Size.Small, position);
            CreateOneAsteroid(AsteroidController.Size.Small, position);
        }
        
        _numberOfActiveAsteroids -= 1;

        Debug.Log("active asteroids = " + _numberOfActiveAsteroids);

        if(_numberOfActiveAsteroids == 0)
        {
            GameManager.Instance.PlayNextLevel();
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

        _numberOfActiveAsteroids += 1;


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
                asteroidController.setSize(AsteroidController.Size.Medium);
                asteroidController.setSpeed(_asteroidFieldParameters.MediumAsteroidSpeed);
                asteroidController.setScale(_asteroidFieldParameters.MediumAsteroidScale);
            break;
            case AsteroidController.Size.Small:
                asteroidController.setSize(AsteroidController.Size.Small);
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
