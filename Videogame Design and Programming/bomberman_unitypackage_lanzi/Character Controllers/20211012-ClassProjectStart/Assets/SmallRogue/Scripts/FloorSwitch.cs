using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    public bool _switch_on = false;
    
    public Sprite FloorSwitchOn;
    public Sprite FloorSwitchOff;
    private SpriteRenderer _spriteRenderer;

    public SwitchControlledObject[] ControlledObjects;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("TRIGGERED");
        if (other.CompareTag("Player"))
        {
            _switch_on = !_switch_on;
            UpdateSwitch();
            for (int i = 0; i < ControlledObjects.Length; i++)
            {
                ControlledObjects[i].Switch();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print("TRIGGERED");
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            _switch_on = !_switch_on;
            UpdateSwitch();
        }
    }
    
    private void UpdateSwitch()
    {
        if (_switch_on)
        {
            _spriteRenderer.sprite = FloorSwitchOn;
        }
        else
        {
            _spriteRenderer.sprite = FloorSwitchOff;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateSwitch();
    }

    // Update is called once per frame
}
