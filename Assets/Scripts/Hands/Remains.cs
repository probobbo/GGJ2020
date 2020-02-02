using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remains : MonoBehaviour
{
    public float deathTimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delet());
    }

    // Update is called once per frame
    private IEnumerator Delet()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }
}
