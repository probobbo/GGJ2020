﻿using System;
using System.Collections;
using System.Collections.Generic;
using Hands;
using Managers;
using Statue;
using UnityEngine;

public class RoboHand : MonoBehaviour
{

    public GameObject remains;
    public RoboHandType roboHandType;
    public float strength = 1f;
    public float usages = 1f;
    protected float remainingUsages = 1f;

    protected Rigidbody Rb;
    protected RoboHandController _roboHandController;
    protected VelocityEstimator _velocityEstimator;

    protected virtual void Start()
    {
        _velocityEstimator = GetComponentInParent<VelocityEstimator>();
        _velocityEstimator.BeginEstimatingVelocity();
        Rb = GetComponent<Rigidbody>();
        remainingUsages = usages;
        _roboHandController = GetComponentInParent<RoboHandController>();
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

        var vel = _velocityEstimator.GetVelocityEstimate();
        if (!statuePiece.IsOnTheFloor)
        {
            var impulse = new Vector3(vel.x, 1f, vel.z);
            statuePiece.ApplyForce(impulse * strength);
        }
        else
        {
            Debug.Log(vel * strength);
            statuePiece.ApplyForce(vel * strength);
        }
    }

    public void DetachHand()
    {
        //Istanzia prefab mano a caso
        _roboHandController.SetDefaultHand();
    }

    public virtual void ResetHand()
    {
        //SE NECESSARIO QUI RIMETTI POSIZIONE
        remainingUsages = usages;
    }
}