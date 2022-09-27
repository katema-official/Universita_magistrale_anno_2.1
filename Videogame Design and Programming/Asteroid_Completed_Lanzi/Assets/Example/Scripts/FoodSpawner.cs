using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using POLIMIGameCollective;
public class FoodSpawner : MonoBehaviour
{
    public GameObject FoodPrefab;
    
    // Start is called before the first frame update

    private void Awake()
    {
       ScreenBounds.ComputeScreenBounds(); 
       Debug.Log("HORIZONTAL "+ScreenBounds.left+" "+ScreenBounds.right);
       Debug.Log("VERTICAL "+ScreenBounds.top+" "+ScreenBounds.bottom);
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-17f, 17f);
            float y = Random.Range(-9f, 9f);
            
            Vector3 position = new Vector3(x, y, transform.position.z);

            Instantiate(FoodPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(2f);
        }
        
        yield return null;
    }
}
