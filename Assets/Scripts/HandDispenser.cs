﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HandDispenser : MonoBehaviour
{
    private bool _changingHand;
    private Coroutine _handSwitchCoroutine;

    [SerializeField] private Image circle;
    [SerializeField] private float fillTime;
    [SerializeField] private Ease easeType;

    private Tweener routing;

    private void Start()
    {
        circle.fillAmount = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        var hand = other.transform.parent.GetComponentInParent<RoboHandController>();
        if (hand == null) return;
        if (hand.HasHand) return;

        _handSwitchCoroutine = StartCoroutine(Fill(hand));
    }


    private IEnumerator Fill(RoboHandController hand)
    {
        yield return circle.DOFillAmount(1, fillTime).SetEase(easeType).WaitForCompletion();
        hand.ChangeRoboHand();
        //TODO Particellare
        circle.fillAmount = 0f;


    }

    private IEnumerator UnFill()
    {
        yield return circle.DOFillAmount(0, fillTime/2).WaitForCompletion();
    }

    
    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_handSwitchCoroutine);
        StartCoroutine(UnFill());
    }
}
