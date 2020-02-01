using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawGrabber : OVRGrabber
{
    private Claw _claw;

    protected override void Start()
    {
        base.Start();
        _claw = GetComponent<Claw>();
    }

    protected override void GrabBegin()
    {
        base.GrabBegin();
        _claw.ActivateHand();
    }

    protected override void GrabEnd()
    {
        base.GrabEnd();
        _claw.DeactivateHand();
    }
}
