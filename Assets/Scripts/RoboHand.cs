using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Statue;
using UnityEngine;

public class RoboHand : MonoBehaviour
{
    public RoboHandType roboHandType;
    public float strength = 1f;
    public float usages = 1f;
    protected float remainingUsages = 1f;

    protected Rigidbody Rb;
    protected RoboHandController _roboHandController;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        remainingUsages = usages;
        _roboHandController = GetComponentInParent<RoboHandController>();
        
        EventManager.Instance.OnHandActivation.AddListener(hand =>
        {
            if (_roboHandController.controller == hand)
                ActivateHand();
        });
    }

    public virtual void ActivateHand()
    {
        if (remainingUsages <= 0)
        {
            DetachHand();
            return;
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;

        if (!statuePiece.IsOnTheFloor)
        {
            var impulse = new Vector3(other.impulse.x, 1f, other.impulse.z);
            statuePiece.ApplyForce(impulse * strength);
        }
        else
        {
            statuePiece.ApplyForce(other.impulse * strength);
        }
    }

    public void DetachHand()
    {
        _roboHandController.SetDefaultHand();
    }

    public virtual void ResetHand()
    {
        //SE NECESSARIO QUI RIMETTI POSIZIONE
        remainingUsages = usages;
    }
}