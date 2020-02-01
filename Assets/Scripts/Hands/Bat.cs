using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : RoboHand
{
    [SerializeField] private float batStrength;
    private bool _isActive;
    
    public override void ActivateHand()
    {
        base.ActivateHand();
        _isActive = true;
        Rb.mass = strength * batStrength;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
