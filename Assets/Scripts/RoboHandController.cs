using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoboHandController : MonoBehaviour
{
    public OVRInput.Controller controller;
    
    private RoboHand[] _roboHands;
    public RoboHand ActiveHand { get; private set; }
    private RoboHand _defaultHand;
    
    private IEnumerator Start()
    {
        _roboHands = GetComponentsInChildren<RoboHand>(true);
        yield return null;
        foreach (var roboHand in _roboHands)
        {
            roboHand.gameObject.SetActive(false);
        }
        
        ActiveHand = _roboHands.First(hand => hand.roboHandType == RoboHandType.Default);
        if (ActiveHand == null)
        {
            Debug.LogWarning("default hand not present");
            yield break;
        }

        _defaultHand = ActiveHand;
        ActiveHand.gameObject.SetActive(true);
    }

    public void ChangeRoboHand()
    {
        ActiveHand.gameObject.SetActive(false);
        int randomu;
        var nextRoboHand = ActiveHand;
        while (nextRoboHand.roboHandType == ActiveHand.roboHandType)
        {
            randomu = Random.Range(0, _roboHands.Length);
            nextRoboHand = _roboHands[randomu];
        }

        ActiveHand = nextRoboHand;
        ActiveHand.gameObject.SetActive(true);
        Debug.Log($"new hand type {ActiveHand.roboHandType}");
    }

    public void SetDefaultHand()
    {
        ActiveHand.gameObject.SetActive(false);
        ActiveHand = _defaultHand;
        ActiveHand.gameObject.SetActive(true);
    }
}
