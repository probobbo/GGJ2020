using System.Collections;
using System.Collections.Generic;
using Statue;
using UnityEngine;

public class Bat : RoboHand
{
    [SerializeField] private float batStrength;
    private bool _isActive;

    protected override void Start()
    {
        base.Start();
        Rb.mass = strength;
    }

    public override void ActivateHand()
    {
        if (_isActive) return;
        base.ActivateHand();
        remainingUsages--;
        _isActive = true;
        Rb.mass *= batStrength;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;
        
        _isActive = false;
        Rb.mass = strength;
    }
}
