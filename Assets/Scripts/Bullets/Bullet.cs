using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float strength = 1f;
    protected void OnCollisionEnter(Collision other)
    {
        CollideWithThings(other);
    }

    protected virtual void CollideWithThings(Collision other)
    {
        Destroy(gameObject);
    }
}
