using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDispenser : MonoBehaviour
{
    private bool _alreadyChanged;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("GameController") || _alreadyChanged) return;

        _alreadyChanged = true;
        var hand = other.transform.parent.GetComponentInParent<RoboHandController>();
        hand.ChangeRoboHand();
    }

    private void OnTriggerExit(Collider other)
    {
        _alreadyChanged = false;
    }
}
