using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("Triggering Explode");
			SimpleEventManager.TriggerEvent("Explode");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log("Triggering RunAway");
			SimpleEventManager.TriggerEvent ("RunAway");			
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			Debug.Log("Triggering Spawn");
			SimpleEventManager.TriggerEvent("Spawn");
		}

	}
}
