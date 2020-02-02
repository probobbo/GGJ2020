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

    public void ChangeRoboHand()
    {
        EventManager.Instance.OnHandActivation.RemoveListener(ActivateHand);
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
        EventManager.Instance.OnHandActivation.AddListener(ActivateHand);

        var eventRef = "";
        switch (ActiveHand.roboHandType)
        {
            case RoboHandType.Claw:
                eventRef = "event:/SOUNDFX/Spawns/SFX_Spawn_Banana";
                break;
            case RoboHandType.Bat:
                eventRef = "event:/SOUNDFX/Spawns/SFX_Spawn_BaseballBat";
                break;
            case RoboHandType.Godzilla:
                eventRef = "event:/SOUNDFX/Spawns/SFX_Spawn_Chicken";
                break;
            case RoboHandType.Gun:
                eventRef = "event:/SOUNDFX/Spawns/SFX_Spawn_Gun";
                break;
            case RoboHandType.Cannon:
                eventRef = "event:/SOUNDFX/Spawns/SFX_Spawn_Cannon";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        AudioManager.PlayOneShotAudio(eventRef, gameObject);

    }

    private void ActivateHand(OVRInput.Controller hand)
    {
        if (ActiveHand != null && controller == hand)
            ActiveHand.ActivateHand();
    }


    private void Update()
    {
        if (controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One) ||
            controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.Three) ||
            Input.GetKeyDown(KeyCode.Z))
            SetDefaultHand();
    }

    public void SetDefaultHand()
    {
        if (ActiveHand != null)
        {
            EventManager.Instance.OnHandActivation.RemoveListener(ActivateHand);
            ActiveHand.gameObject.SetActive(false);
            ActiveHand.ResetHand();
        }

        HasHand = false;
        /*
        ActiveHand = _defaultHand;
        ActiveHand.gameObject.SetActive(true);*/
    }
}