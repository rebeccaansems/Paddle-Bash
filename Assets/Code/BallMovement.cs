﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float maxSpeed = 200f;
    
    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }
}