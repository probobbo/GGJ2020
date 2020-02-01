using System;
using System.Collections;
using System.Collections.Generic;
using Statue;
using UnityEngine;

public class RoboHand : MonoBehaviour
{
    public RoboHandType roboHandType;
    public float strength = 1f;
    public float usages = 1f;

    protected Rigidbody Rb;
    private RoboHandController _roboHandController;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        _roboHandController = GetComponentInParent<RoboHandController>();
        
        EventManager.Instance.OnHandActivation.AddListener(hand =>
        {
            if (_roboHandController.controller == hand)
                ActivateHand();
        });
    }

    public virtual void ActivateHand()
    {
        if (usages <= 0)
        {
            DetachHand();
            return;
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;

        if (!statuePiece.IsOnTheFloor) return;
        var impulse = new Vector3(other.impulse.x, -other.impulse.y, other.impulse.z);
        statuePiece.ApplyForce(impulse * strength);
    }

    public void DetachHand()
    {
        _roboHandController.SetDefaultHand();
    }
}