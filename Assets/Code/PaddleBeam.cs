using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBeam : MonoBehaviour
{
    public GameObject beamLineRendererPrefab;
    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;

    public Color[] Light;
    public Gradient[] Laser, GlowBox;
    public Material[] Beam;

    private LineRenderer line;

    private void Start()
    {
        line = beamLineRendererPrefab.GetComponent<LineRenderer>();
        line.useWorldSpace = true;
    }

    private void Update()
    {
        line.SetPositions(new Vector3[] { beamStartPrefab.transform.position, beamEndPrefab.transform.position });
    }

    public void SetColor(int colorNum)
    {
        var startParticleSystem = beamStartPrefab.GetComponent<ParticleSystem>().colorOverLifetime;
        var startGlowBox = beamStartPrefab.GetComponentsInChildren<ParticleSystem>()[1].colorOverLifetime;

        beamStartPrefab.GetComponent<Light>().color = Light[colorNum];
        startParticleSystem.color = Laser[colorNum];
        startGlowBox.color = GlowBox[colorNum];

        beamLineRendererPrefab.GetComponent<LineRenderer>().material = Beam[colorNum];

        var endParticleSystem = beamEndPrefab.GetComponent<ParticleSystem>().colorOverLifetime;
        var endGlowBox = beamEndPrefab.GetComponentsInChildren<ParticleSystem>()[1].colorOverLifetime;

        beamEndPrefab.GetComponent<Light>().color = Light[colorNum];
        endParticleSystem.color = Laser[colorNum];
        endGlowBox.color = GlowBox[colorNum];
    }
}
