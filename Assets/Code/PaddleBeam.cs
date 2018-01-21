using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBeam : MonoBehaviour
{
    public GameObject beamLineRendererPrefab;
    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
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
}
