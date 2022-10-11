using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool _switch_on = false;
    
    public Sprite SwitchOn;
    public Sprite SwitchOff;
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

    private void UpdateSwitch()
    {
        if (_switch_on)
        {
            _spriteRenderer.sprite = SwitchOn;
        }
        else
        {
            _spriteRenderer.sprite = SwitchOff;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateSwitch();
    }

    // Update is called once per frame
}
