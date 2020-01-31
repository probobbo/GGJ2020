using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoboHandController : MonoBehaviour
{
    private RoboHand[] _roboHands;
    private RoboHand _activeHand;
    
    private IEnumerator Start()
    {
        _roboHands = GetComponentsInChildren<RoboHand>(true);
        yield return null;
        foreach (var roboHand in _roboHands)
        {
            roboHand.gameObject.SetActive(false);
        }
        
        _activeHand = _roboHands.First(hand => hand.roboHandType == RoboHandType.Default);
        if (_activeHand == null)
        {
            Debug.LogWarning("default hand not present");
            yield break;
        }
        _activeHand.gameObject.SetActive(true);
    }

    public void ChangeRoboHand()
    {
        _activeHand.gameObject.SetActive(false);
        int randomu;
        var nextRoboHand = _activeHand;
        while (nextRoboHand.roboHandType == _activeHand.roboHandType)
        {
            randomu = Random.Range(0, _roboHands.Length);
            nextRoboHand = _roboHands[randomu];
        }

        _activeHand = nextRoboHand;
        _activeHand.gameObject.SetActive(true);
        Debug.Log($"new hand type {_activeHand.roboHandType}");
    }
}
