using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Bullet : MonoBehaviour
{
    [FormerlySerializedAs("TimeToLive")] public float timeToLive = 10f;
    public float strength = 1f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    protected void OnCollisionEnter(Collision other)
    {
        CollideWithThings(other);
    }

    protected virtual void CollideWithThings(Collision other)
    {
        Destroy(gameObject);
    }
}
