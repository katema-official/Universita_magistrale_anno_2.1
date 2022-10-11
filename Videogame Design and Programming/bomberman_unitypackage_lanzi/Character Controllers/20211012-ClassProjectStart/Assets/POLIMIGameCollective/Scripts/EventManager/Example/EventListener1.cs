using UnityEngine;

public class EventListener1 : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		SimpleEventManager.StartListening ("Explode",Explode);
		SimpleEventManager.StartListening ("RunAway",RunAway);
	}

	void Explode () {
		SimpleEventManager.StopListening ("Explode", Explode);
		Debug.Log ("Explode");
	}

	void RunAway () {
		SimpleEventManager.StopListening ("RunAway", RunAway);
		Debug.Log ("RunAway");
	}
}
