using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

public class ProgressLights : MonoBehaviour
{
    private List<MeshRenderer> lights;
    
    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponentsInChildren<MeshRenderer>().ToList();
        EventManager.Instance.onPieceConnected.AddListener(LightLight);
    }

    private void LightLight()
    {
        lights.First(meshRenderer => meshRenderer.material.color == Color.red).material.color = Color.green;
    }
}
