using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoboHandController : MonoBehaviour
{
    public OVRInput.Controller controller;
    
    private RoboHand[] _roboHands;
    public RoboHand ActiveHand { get; private set; }
    //private RoboHand _defaultHand;
    public bool HasHand { get; private set; }
    
    private IEnumerator Start()
    {
        _roboHands = GetComponentsInChildren<RoboHand>(true);
        yield return null;
        foreach (var roboHand in _roboHands)
        {
            roboHand.gameObject.SetActive(false);
        }

        HasHand = false;
        /*
        ActiveHand = _roboHands.First(hand => hand.roboHandType == RoboHandType.Default);
        if (ActiveHand == null)
        {
            Debug.LogWarning("default hand not present");
            yield break;
        }

        _defaultHand = ActiveHand;*/
        //ActiveHand.gameObject.SetActive(true);
    }

    public IEnumerator ChangeRoboHand()
    {
        yield return new WaitForSeconds(GameManager.Instance.TimeToSwitchHand);
        if (ActiveHand == null)
        {
            var randomu = Random.Range(0, _roboHands.Length);
            ActiveHand = _roboHands[randomu];
        }
        else
        {
            ActiveHand.gameObject.SetActive(false);

            var nextRoboHand = ActiveHand;
            while (nextRoboHand.roboHandType == ActiveHand.roboHandType)
            {
                var randomu = Random.Range(0, _roboHands.Length);
                nextRoboHand = _roboHands[randomu];
            }

            ActiveHand = nextRoboHand;
        }

        HasHand = true;
        ActiveHand.gameObject.SetActive(true);
        Debug.Log($"new hand type {ActiveHand.roboHandType}");
    }

    public void SetDefaultHand()
    {
        ActiveHand.gameObject.SetActive(false);
        HasHand = false;
        /*
        ActiveHand = _defaultHand;
        ActiveHand.gameObject.SetActive(true);*/
    }
}
