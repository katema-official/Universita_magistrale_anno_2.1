using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : SwitchControlledObject
{
    private BoxCollider2D _boxCollider;
    
    protected override void Awake()
    {
        base.Awake();
        
        _boxCollider = GetComponent<BoxCollider2D>();
        
        UpdateCollider();
    }
    protected override void UpdateSwitch()
    {
        base.UpdateSwitch();
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        if (_switch_on)
        {
            _boxCollider.isTrigger = true;
        }
        else
        {
            _boxCollider.isTrigger = false;
        }

    }
    
}
