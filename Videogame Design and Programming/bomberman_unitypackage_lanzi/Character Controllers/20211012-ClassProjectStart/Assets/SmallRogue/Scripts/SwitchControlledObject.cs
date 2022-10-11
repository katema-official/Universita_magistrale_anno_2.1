using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchControlledObject : MonoBehaviour
{
    public bool _switch_on = false;
    
    public Sprite OnStatusSprite;
    public Sprite OffStatusSprite;
    private SpriteRenderer _spriteRenderer;
    
    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Switch()
    {
        _switch_on = !_switch_on;
        UpdateSwitch();
    }
    
    protected virtual void UpdateSwitch()
    {
        if (_switch_on)
        {
            if (OnStatusSprite == null)
            {
                print("STATUS SHOULD BE ON");
                _spriteRenderer.enabled = false;
            }
            else
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = OnStatusSprite;
            }

        }
        else
        {
            if (OffStatusSprite == null)
            {
                print("STATUS SHOULD BE OFF");
                _spriteRenderer.enabled = false;
            }
            else
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = OffStatusSprite;

            }
        }
    }

    void OnEnable()
    {
        UpdateSwitch();
    }
}
