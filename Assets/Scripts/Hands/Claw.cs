using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio;

public class Claw : RoboHand
{
    private Coroutine _consumeCoroutine;

    public override void ActivateHand()
    {
        _consumeCoroutine = StartCoroutine(Consume());
    }
    
    public void DeactivateHand()
    {
        StopCoroutine(_consumeCoroutine);
    }

    private IEnumerator Consume()
    {
        while (remainingUsages >= 0f)
        {
            yield return new WaitForEndOfFrame();
            remainingUsages -= Time.deltaTime;
        }
        _roboHandController.SetDefaultHand();
    }
}
