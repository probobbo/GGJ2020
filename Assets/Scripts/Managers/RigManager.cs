using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigManager : MonoBehaviour
{
    private Transform _player;
    private Transform _camera;

    private void Start()
    {
        _player = transform;
        _camera = Camera.main.transform;
    }

    private void Update()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        var thumbstickAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        Vector3 rotated = Quaternion.Inverse(Quaternion.AngleAxis(_player.eulerAngles.y, Vector3.up)) *
                          (Quaternion.AngleAxis(_camera.eulerAngles.y, Vector3.up) *
                           new Vector3(thumbstickAxis.x, 0, thumbstickAxis.y));

        _player.Translate(rotated * Time.deltaTime);
#endif
    }
}