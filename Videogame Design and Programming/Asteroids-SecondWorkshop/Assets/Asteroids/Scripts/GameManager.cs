using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private LevelManager _levelManager;
    private AsteroidFieldManager _asteroidFieldManager;

    private void Awake()
    {
        base.Awake();
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
        _asteroidFieldManager = GameObject.FindObjectOfType<AsteroidFieldManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AsteroidLevel gameLevel = _levelManager.GetLevel();
        _asteroidFieldManager.PlayLevel(gameLevel);
    }

    public void PlayNextLevel()
    {
        AsteroidLevel gameLevel = _levelManager.GetNextLevel();
        _asteroidFieldManager.PlayLevel(gameLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
