using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDispenser : MonoBehaviour
{
    private bool _changingHand;
    private Coroutine _handSwitchCoroutine;
    
    private void OnTriggerEnter(Collider other)
    {
        var hand = other.transform.parent.GetComponentInParent<RoboHandController>();
        if (hand == null) return;
        if (hand.HasHand) return;

        _changingHand = true;
        _handSwitchCoroutine = StartCoroutine(hand.ChangeRoboHand());
    }

    private void OnTriggerExit(Collider other)
    {
        if (_handSwitchCoroutine != null)
        {
            StopCoroutine(_handSwitchCoroutine);
        }

        _changingHand = false;
    }
}
