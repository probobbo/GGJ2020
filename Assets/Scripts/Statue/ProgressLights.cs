using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

public class ProgressLights : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> lights = new List<MeshRenderer>(6);

    private static readonly int EMISSION_COLOR = Shader.PropertyToID("_EmissionColor");

    // Start is called before the first frame update
    void Start()
    {
        lights.ForEach(meshRenderer => meshRenderer.material.color = Color.red);
        EventManager.Instance.onPieceConnected.AddListener(LightLight);
    }

    private void LightLight()
    {
        var currentLight = lights.First(meshRenderer => meshRenderer.material.color == Color.red);
            currentLight.material.color = Color.green;
            currentLight.material.SetColor(EMISSION_COLOR, Color.green);
            currentLight.material.EnableKeyword("_EMISSION");
        AudioManager.PlayOneShotAudio("event:/SOUNDFX/SFX_CompleteTask",gameObject);
    }
}
