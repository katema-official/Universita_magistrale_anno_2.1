using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AsteroidLevel[] asteroidLevels;
    // Start is called before the first frame update
    private int _currentLevel = 0;
    public void Reset()
    {
        _currentLevel = 0;
    }
    public AsteroidLevel GetLevel()
    {
        return asteroidLevels[_currentLevel];
    }

    public AsteroidLevel GetNextLevel()
    {
        _currentLevel = Mathf.Min(_currentLevel + 1, asteroidLevels.Length - 1);
        Debug.Log("Level = " + _currentLevel + ", number of asteroids to spawn = " + asteroidLevels[_currentLevel].NumberOfLargeAsteroids);
        return asteroidLevels[_currentLevel];
    }
}
