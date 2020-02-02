using System.Collections;
using System.Collections.Generic;
using Hands;
using Statue;
using UnityEngine;

public class Bat : RoboHand
{
    [SerializeField] private float batStrength;
    private bool _isActive;
    [SerializeField] private MeshRenderer mesh;
    private Material _mat;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private Color col;
    private static readonly int mainColor = Shader.PropertyToID("_Color");

    protected override void Start()
    {
        base.Start();
        _mat = mesh.material;
        col = _mat.GetColor(mainColor);

    }

    public override void ActivateHand()
    {
        if (_isActive) return;
        base.ActivateHand();
        remainingUsages--;
        _isActive = true;
        strength *= batStrength;
        _mat.EnableKeyword("_EMISSION");
        _mat.SetColor(EmissionColor, Color.magenta);
        _mat.SetColor(mainColor, Color.magenta);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;
        
        var vel = _velocityEstimator.GetVelocityEstimate();
        statuePiece.ApplyForce(vel * strength);
        if (_isActive)
        {
            _mat.DisableKeyword("_EMISSION");
            _mat.SetColor(EmissionColor, Color.white);
            _mat.SetColor(mainColor, col);
            strength /= batStrength;
        }

        _isActive = false;
    }
}
