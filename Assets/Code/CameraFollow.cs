using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Camera MainCamera, OrthoCamera;
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
            Vector3 delta = Players[follow].position - this.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

    public void FollowPlayer(int playerNum)
    {
        MainCamera.enabled = false;
        this.GetComponent<Camera>().enabled = true;
        follow = playerNum;
        
        OrthoCamera.orthographicSize = 1.4f;
        OrthoCamera.transform.parent = this.transform;
        
        Time.timeScale = 0.4f * GameData.Instance.Speed;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;

        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1 * GameData.Instance.Speed;
        Time.fixedDeltaTime = 0.02F;
    }
}
