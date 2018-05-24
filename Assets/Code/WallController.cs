using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject ParentWall;
    public LevelData LevelData;

    void Start()
    {
        foreach (MeshRenderer mesh in ParentWall.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.materials[0].SetColor("_EmissionColor", LevelData.LevelColor);
        }
    }
}
