using System.Collections;
using System.Collections.Generic;
using Hands;
using Statue;
using UnityEngine;

public class Bat : RoboHand
{
    [SerializeField] private float batStrength;
    private bool _isActive;

    public override void ActivateHand()
    {
        if (_isActive) return;
        base.ActivateHand();
        remainingUsages--;
        _isActive = true;
        strength *= batStrength;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;
        
        var vel = _velocityEstimator.GetVelocityEstimate();
        Debug.Log("nigga collide " + vel);
        statuePiece.ApplyForce(vel * strength);
        if(_isActive)
            strength /= batStrength;
        _isActive = false;
    }
}
