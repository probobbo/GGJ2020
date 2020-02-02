using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private ParticleSystem _system;

    private void Start()
    {
        _system = GetComponent<ParticleSystem>();
        _system.Play();
        StartCoroutine(WaitAndKill());
    }

    IEnumerator WaitAndKill()
    {
        yield return new WaitWhile(()=>_system.isPlaying);
        Destroy(gameObject);
    }
}
