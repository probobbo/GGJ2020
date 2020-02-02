using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

public class ProgressLights : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> lights = new List<MeshRenderer>(6);
    
    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponentsInChildren<MeshRenderer>().ToList();
        lights.ForEach(meshRenderer => meshRenderer.material.color = Color.red);
        EventManager.Instance.onPieceConnected.AddListener(LightLight);
    }

    private void LightLight()
    {
        lights.First(meshRenderer => meshRenderer.material.color == Color.red).material.color = Color.green;
    }
}
