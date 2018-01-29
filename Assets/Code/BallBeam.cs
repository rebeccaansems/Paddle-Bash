using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBeam : MonoBehaviour
{
    public GameObject TrailGameObject;
    public GameObject BigGlowGameObject;
    public GameObject PointLightGameObject;

    public Color[] Glow, PointLight;
    public Gradient[] Trail;
    public Material[] TrailMat;

    public void Awake()
    {
        var bigGlowEmission = BigGlowGameObject.GetComponent<ParticleSystem>().emission;
        bigGlowEmission.enabled = false;
        PointLightGameObject.GetComponent<Light>().enabled = false;
    }

    public void SetColor(int colorNum)
    {
        var bigGlowEmission = BigGlowGameObject.GetComponent<ParticleSystem>().emission;
        var trailColorLifetime = TrailGameObject.GetComponent<ParticleSystem>().colorOverLifetime;
        var bigGlowStartColor = BigGlowGameObject.GetComponent<ParticleSystem>().main;
        var pointLightStart = PointLightGameObject.GetComponent<Light>();

        trailColorLifetime.color = Trail[colorNum];
        TrailGameObject.GetComponent<TrailRenderer>().material = TrailMat[colorNum];

        bigGlowStartColor.startColor = Glow[colorNum];

        pointLightStart.color = PointLight[colorNum];
        
        PointLightGameObject.GetComponent<Light>().enabled = true;
        bigGlowEmission.enabled = true;
    }
}
