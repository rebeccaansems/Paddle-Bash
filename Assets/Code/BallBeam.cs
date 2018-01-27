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

    public void SetColor(int colorNum)
    {
        var trailColorLifetime = TrailGameObject.GetComponent<ParticleSystem>().colorOverLifetime;
        var bigGlowStartColor = BigGlowGameObject.GetComponent<ParticleSystem>().main;
        var pointLightStart = PointLightGameObject.GetComponent<Light>();

        trailColorLifetime.color = Trail[colorNum];
        TrailGameObject.GetComponent<TrailRenderer>().material = TrailMat[colorNum];

        bigGlowStartColor.startColor = Glow[colorNum];

        pointLightStart.color = PointLight[colorNum];
    }
}
