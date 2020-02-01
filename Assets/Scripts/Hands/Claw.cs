using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio;

public class Claw : RoboHand
{
    private Coroutine _consumeCoroutine;

    private ClawGrabber _grabber;

    private bool running = false;


    protected override void Start()
    {
        base.Start();
        _grabber = GetComponent<ClawGrabber>();
    }

    public override void ActivateHand()
    {
        running = true;
        _consumeCoroutine = StartCoroutine(Consume());
    }
    
    public void DeactivateHand()
    {
        StopCoroutine(_consumeCoroutine);
        running = false;
    }

    private IEnumerator Consume()
    {
        while (remainingUsages > 0f && running)
        {
            yield return new WaitForEndOfFrame();
            remainingUsages -= Time.deltaTime;
        }

        if (remainingUsages <= 0f)
        {
            if(_grabber.grabbedObject != null)
                _grabber.ForceRelease(_grabber.grabbedObject);
            _roboHandController.SetDefaultHand();
        }
    }
}
