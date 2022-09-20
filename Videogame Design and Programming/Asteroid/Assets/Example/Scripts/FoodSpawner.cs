using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POLIMIGameCollective;

public class FoodSpawner : MonoBehaviour
{

    public GameObject ApplePrefab;

    private void Awake()
    {
        ScreenBounds.ComputeScreenBounds();
        Debug.Log(ScreenBounds.left + "-" + ScreenBounds.right);
        Debug.Log(ScreenBounds.top + "-" + ScreenBounds.bottom);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        for(int i = 0; i < 10; i++) { 
            float x = Random.Range(-10f, 10f);
            float y = Random.Range(-7f, 7f);
            Vector3 position = new Vector3(x,y,transform.position.z);
            Instantiate(ApplePrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

}
