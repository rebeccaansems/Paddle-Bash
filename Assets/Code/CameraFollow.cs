﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Camera MainCamera;
    public Transform[] Players;

    private int follow;
    private float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        follow = -1;
    }

    void FixedUpdate()
    {
        if (follow != -1)
        {
            Vector3 point = this.GetComponent<Camera>().WorldToViewportPoint(Players[follow].position);
            Vector3 delta = Players[follow].position - this.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

    public void FollowPlayer(int playerNum)
    {
        MainCamera.enabled = false;
        this.GetComponent<Camera>().enabled = true;
        follow = playerNum;
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }
}
