using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using Managers;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    [SerializeField] [EventRef] private string sfx;

    [SerializeField] private int sceneToLoad;
    private bool _alreadyTriggered = false;
    private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        _alreadyTriggered = false;
        _canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Wrist"))
            return;
        if (_alreadyTriggered) return;
        _alreadyTriggered = true;
        
        AudioManager.PlayOneShotAudio(sfx, gameObject);
        GameManager.Instance.LoadScene(sceneToLoad);
        _canvasGroup.DOFade(0, 1f);
        
    }
}
