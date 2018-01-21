using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float RotateSpeed = 1.2f;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed, Space.Self);
    }
}
