using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BeginButton : MonoBehaviour
{
    //[SerializeField] [EventRef] private string sfx;
    private bool _alreadyTriggered = false;
    private CanvasGroup _canvasGroup;

    
    private void Start()
    {
        _alreadyTriggered = false;
        _canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hand")) return;
        if (_alreadyTriggered) return;
        _alreadyTriggered = true;
        
        //AudioManager.PlayOneShotAudio(sfx, gameObject);
        _canvasGroup.DOFade(0, 1f);
    }
}
