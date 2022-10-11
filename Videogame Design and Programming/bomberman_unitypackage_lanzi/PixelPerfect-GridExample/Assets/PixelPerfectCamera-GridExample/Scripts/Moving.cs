using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{

	private bool _pixelPerfect = false;
	private bool _pause = false;
	private float velocity = .5f;
	private Transform _transform;
	[SerializeField] private Toggle toggle; 
	
	[SerializeField] public PixelPerfectCamera _pixelPerfectCamera;
    
	void Awake() {
		_transform = GetComponent<Transform>();
		_pixelPerfectCamera.enabled = _pixelPerfect;
	}

	public void SetPixelCamera(bool pixelCamera)
	{
		_pixelPerfect = pixelCamera;
		_pixelPerfectCamera.enabled = pixelCamera;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_pause = !_pause;
		}

		if (!_pause)
		{
			float d = Mathf.Sin(Time.time*.5f);
		
			if (d>=0)
				_transform.position = _transform.position + _transform.up * velocity * Time.deltaTime;
			else 
				_transform.position = _transform.position - _transform.up * velocity * Time.deltaTime;
		}

	}
}
