using UnityEngine;
using System.Collections;

public class EventListener2 : MonoBehaviour {

	void OnEnable() {
		SimpleEventManager.StartListening ("Spawn", Spawn);
	}

	void OnDisable() {
		SimpleEventManager.StopListening ("Spawn", Spawn);
	}

	// Update is called once per frame
	void Spawn () {
		SimpleEventManager.StopListening ("Spawn", Spawn);
		Debug.Log("SPAWN EVENT");
		SimpleEventManager.StartListening ("Spawn", Spawn);
	}
}
