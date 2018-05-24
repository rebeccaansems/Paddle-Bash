using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    public MeshRenderer[] AllWalls;
    public LevelData LevelData;

    void Start()
    {
        foreach (MeshRenderer mesh in AllWalls)
        {
            mesh.materials[0].SetColor("_EmissionColor", LevelData.LevelColor);
        }
    }
}
