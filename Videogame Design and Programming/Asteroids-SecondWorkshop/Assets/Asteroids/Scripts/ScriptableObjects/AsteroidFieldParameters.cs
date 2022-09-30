using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Asteroid Field Parameters")]
public class AsteroidFieldParameters : ScriptableObject
{

    //this is like a prefab but not for GameObjects put for their parameters. This alone doesn't do anything.
    //You have to create a proper Asteroid Field Parameters by right-clicking on the unity editor (while inside a folder
    //for example), go to Asteroids on the top, and then to Asteroid Field Parameters. This will be a "configuration file"
    //in which you can change the values below as you wish, and then use the values of the file for your customizations.

    [Header("Large Asteroid")]
    public float LargeAsteroidSpeed = 3f;
    public float LargeAsteroidScale = 1f;
    [Header("Medium Asteroid")]
    public float MediumAsteroidSpeed = 2f;
    public float MediumAsteroidScale = .5f;
    [Header("Small Asteroid")]
    public float SmallAsteroidSpeed = 1f;
    public float SmallAsteroidScale = .25f;


}
