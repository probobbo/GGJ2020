using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoboHand : MonoBehaviour
{
    public RoboHandType roboHandType;

    public virtual void ActivateHand()
    {
        Debug.Log($"hand type: {roboHandType}");
    }
}