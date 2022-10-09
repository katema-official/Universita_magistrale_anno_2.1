using System;
using System.Collections;
using System.Collections.Generic;
using POLIMIGameCollective;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidFieldManager : MonoBehaviour
{

    private enum Quadrant
    {
        Top = 0,
        Right = 1,
        Bottom = 2,
        Left = 3
    };

    //our spaceship
    [SerializeField] private SpaceShipController _spaceShipController;
    [SerializeField] private GameObject _spaceShipExplosion;

    private SpaceShipController _spaceship;
    private int _numberOfSpaceShips;
    private int _initialNumberOfSpaceships = 3;
    
    [SerializeField] private float _quadrantCentralArea = 1f;

    [SerializeField] private AsteroidController[] _asteroidControllers;

    private float _top;
    private float _bottom;
    private float _left;
    private float _right;
    private float _width;
    private float _height;

    [SerializeField] private float _margin = 1f;
    
    [Header("Percentage of Screen as Target")]
    [Range(0.25f,0.75f)]
    [SerializeField] private float _targetAreaRatio = .5f;

    [SerializeField] private AsteroidFieldParameters _asteroidFieldParameters;
    [SerializeField] private GameObject ExplosionParticleEffect;

    private float _targetAreaRay;
    private int _numberOfActiveAsteroids = 0;
    private int _numberOfDestroyedAsteroids = 0;
    private int _totalNumberOfGeneratedAsteroid = 0;
    private int _numberOfGeneratedSmallSaucers = 0;
    private int _numberOfGeneratedBigSaucers = 0;

    private AsteroidLevel _asteroidLevel;
    
    private void Awake()
    {
        if (!ScreenBounds.ValidBounds)
        {
            ScreenBounds.ComputeScreenBounds();
        }
        _top = ScreenBounds.Top;
        _bottom = ScreenBounds.Bottom;
        _left = ScreenBounds.Left;
        _right = ScreenBounds.Right;
        _width = _right - _left + 2*_margin;
        _height = _top - _bottom + 2*_margin;
        _targetAreaRay = _targetAreaRatio * _height;

        _spaceship = Instantiate(_spaceShipController);
        _spaceship.Init(this);
        _spaceship.gameObject.SetActive(false);

    }

    public void PlayLevel(AsteroidLevel level)
    {
        StartCoroutine(PlayLevelCoroutine(level));

    }



    public IEnumerator PlayLevelCoroutine(AsteroidLevel level)
    {
        _asteroidLevel = level;

        _numberOfGeneratedSmallSaucers = level.NumberOfSmallSaucer;

        _numberOfGeneratedBigSaucers = level.NumberOfBigSaucer;

        _numberOfDestroyedAsteroids = 0;

        _totalNumberOfGeneratedAsteroid = 0;

        _numberOfSpaceShips = _initialNumberOfSpaceships;

        _spaceship.transform.position = Vector3.zero;
        _spaceship.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < level.NumberOfLargeAsteroids; i++)
        {
            CreateStartingAsteroid(AsteroidController.Size.Large);
            _totalNumberOfGeneratedAsteroid = _totalNumberOfGeneratedAsteroid + 7;
            yield return null;
        }

        
    }

    
    private void CreateOneAsteroid(AsteroidController.Size size,
        Vector3 position)
    {
        int asteroidType = (int)(_asteroidControllers.Length * Random.value);

        AsteroidController asteroid = Instantiate(_asteroidControllers[asteroidType],
            position,
            Quaternion.identity);

        Vector3 direction = Random.insideUnitCircle.normalized;
        
        asteroid.SetDirection(direction);
        
        SetupAsteroid(ref asteroid, size);

        _numberOfActiveAsteroids = _numberOfActiveAsteroids + 1;
    }



    public void SpaceShipDestroyed()
    {
        Vector3 position = _spaceship.transform.position;
        Instantiate(_spaceShipExplosion,
            position, Quaternion.identity);

        //create another spaceship with cooldown
        _spaceship.gameObject.SetActive(false);
        _numberOfSpaceShips -= 1;
        if(_numberOfSpaceShips == 0)
        {
            //GAME OVER
        }
        else
        {
            StartCoroutine(SpaceShipDestroyedCoroutine());
        }

    }

    IEnumerator SpaceShipDestroyedCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _spaceship.gameObject.SetActive(true);
        _spaceship.transform.position = Vector3.zero;
        _spaceship.transform.rotation = Quaternion.identity;
        _spaceship.ActivateCoolDown();
    }

    public void AsteroidDestroyed(AsteroidController.Size size,
        Vector3 position)
    {
        AudioManager.Instance.PlayAsteroidExplosion(size);
        Instantiate(ExplosionParticleEffect, 
            position, Quaternion.identity);

        if (size == AsteroidController.Size.Large)
        {
            CreateOneAsteroid(AsteroidController.Size.Medium, position);
            CreateOneAsteroid(AsteroidController.Size.Medium, position);
        }

        if (size == AsteroidController.Size.Medium)
        {
            CreateOneAsteroid(AsteroidController.Size.Small, position);
            CreateOneAsteroid(AsteroidController.Size.Small, position);
        }
        
        _numberOfActiveAsteroids = _numberOfActiveAsteroids - 1;
        
        _numberOfDestroyedAsteroids = _numberOfActiveAsteroids + 1;

        if (_numberOfActiveAsteroids == 0)
        {
            Debug.Log("LEVEL COMPLETED!");
            GameManager.Instance.PlayNextLevel();
        }
    }
    
    private void CreateStartingAsteroid(AsteroidController.Size size = AsteroidController.Size.Large)
    {
        Vector3 position = GetStartPosition();

        Vector3 target = GetTargetPosition();

        Vector3 direction = (target - position).normalized;
        
        int asteroidType = (int)(_asteroidControllers.Length * Random.value);

        AsteroidController asteroid = Instantiate(_asteroidControllers[asteroidType],
            position,
            Quaternion.identity);
        
        asteroid.SetDirection(direction);
        
        SetupAsteroid(ref asteroid, size);
        
        _numberOfActiveAsteroids = _numberOfActiveAsteroids + 1;
        Debug.Log(_numberOfActiveAsteroids);
    }

    private void SetupAsteroid(ref AsteroidController asteroidController, 
        AsteroidController.Size size)
    {
        switch (size)
        {
            case AsteroidController.Size.Large:
                asteroidController.SetSize(AsteroidController.Size.Large);
                asteroidController.SetSpeed(_asteroidFieldParameters.LargeAsteroidSpeed);
                asteroidController.SetScale(_asteroidFieldParameters.LargeAsteroidScale);
                break;
            case AsteroidController.Size.Medium:
                asteroidController.SetSize(AsteroidController.Size.Medium);
                asteroidController.SetSpeed(_asteroidFieldParameters.MediumAsteroidSpeed);
                asteroidController.SetScale(_asteroidFieldParameters.MediumAsteroidScale);
                break;
            case AsteroidController.Size.Small:
                asteroidController.SetSize(AsteroidController.Size.Small);
                asteroidController.SetSpeed(_asteroidFieldParameters.SmallAsteroidSpeed);
                asteroidController.SetScale(_asteroidFieldParameters.SmallAsteroidScale);
                break;
            
        }
    }
    
    // private Vector3 GetStartPosition()
    // {
    //     Vector3 position = Vector3.zero;
    //
    //     Vector3 unit = (Random.insideUnitCircle).normalized;
    //
    //     position.x = unit.x * _width;
    //     position.y = unit.y * _height;
    //     position.z = 0;
    //     
    //     return position;
    // }

    private Vector3 GetTargetPosition()
    {
        Vector3 position = Vector3.zero;

        Vector3 unit = (Random.insideUnitCircle).normalized;

        position = unit * _targetAreaRay;

        return position;
    }
    
    private Vector3 GetStartPosition()
    {
        Vector3 position = new Vector3(0f, 0f, transform.position.z);

        Quadrant quadrant = (Quadrant)(UnityEngine.Random.value * 4f);

        switch (quadrant)
        {
            case Quadrant.Bottom:
                position.y = _bottom;
                position.x = RandomXPosition();
                break;

            case Quadrant.Top:
                position.y = _top;
                position.x = RandomXPosition();
                break;

            case Quadrant.Left:
                position.y = RandomYPosition();
                position.x = _left;
                break;

            case Quadrant.Right:
                position.y = RandomYPosition();
                position.x = _right;
                break;
        }

        return position;
    }
    
    private float RandomXPosition()
    {
        if (Random.value < .5f)
        {
            return Random.Range(_left, -_quadrantCentralArea);
        }
        else
        {
            return Random.Range(_quadrantCentralArea, _right);
        }
    }

    private float RandomYPosition()
    {
        if (Random.value < .5f)
        {
            return Random.Range(_bottom, -_quadrantCentralArea);
        }
        else
        {
            return Random.Range(_quadrantCentralArea, _top);
        }
    }


    private void Update()
    {
        #if UNITY_EDITOR
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                SpaceShipDestroyed();
            }
        }
        #endif
    }
}
