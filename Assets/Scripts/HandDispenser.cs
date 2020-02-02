using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMOD.Studio;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class HandDispenser : MonoBehaviour
{
    private bool _changingHand;
    private Coroutine _handSwitchCoroutine;
    private Coroutine _unfillCoroutine;

    [SerializeField] private Image circle;
    [SerializeField] private float fillTime;
    [SerializeField] private Ease easeType;

    [SerializeField] private EventInstance _eventInstance;
    [SerializeField] private ParticleSystem sparks;

    private Tweener routing;

    private void Start()
    {
        circle.fillAmount = 0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        _eventInstance = AudioManager.PlayAudio("event:/SOUNDFX/SFX_DispencerFiller", gameObject);
        
        var hand = other.transform.parent.GetComponentInParent<RoboHandController>();
        if (hand == null) return;
        if (hand.HasHand) return;

        _handSwitchCoroutine = StartCoroutine(Fill(hand));
        sparks.Play();
    }


    private IEnumerator Fill(RoboHandController hand)
    {
        if (_unfillCoroutine != null)
            StopCoroutine(_unfillCoroutine);
        yield return circle.DOFillAmount(1, fillTime).SetEase(easeType)
            .OnUpdate(()=> AudioManager.SetParameterToInstance(_eventInstance	,"FillAmount",circle.fillAmount))
            .WaitForCompletion();
        hand.ChangeRoboHand();
        //TODO Particellare
        circle.fillAmount = 0f;
        AudioManager.StopAudio(_eventInstance);
        sparks.Stop();
        AudioManager.PlayOneShotAudio("event:/SOUNDFX/SFX_DispencerComplete", gameObject);

    }

    private IEnumerator UnFill()
    {
        yield return circle.DOFillAmount(0, fillTime / 2)
            .OnUpdate(()=> AudioManager.SetParameterToInstance(_eventInstance	,"FillAmount",circle.fillAmount))
            .WaitForCompletion();
    }


    private void OnTriggerExit(Collider other)
    {
        AudioManager.StopAudio(_eventInstance);
        if (_handSwitchCoroutine != null)
            StopCoroutine(_handSwitchCoroutine);
        sparks.Stop();
        _unfillCoroutine = StartCoroutine(UnFill());
    }
}
