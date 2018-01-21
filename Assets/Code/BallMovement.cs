using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public int PlayerId = 0;
    public float MaxSpeed = 200f, MinSpeed = 10f;

    private Vector3 startLocation;
    private Player player;

    private void Start()
    {
        startLocation = this.transform.position;
        player = ReInput.players.GetPlayer(PlayerId);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = MinSpeed * (GetComponent<Rigidbody2D>().velocity.normalized);

        if (player.GetButton("Reset Ball") && PlayerData.k_BallResets[PlayerId] > 0 && transform.position != startLocation)
        {
            this.transform.position = startLocation;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            PlayerData.k_BallResets[PlayerId]--;
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude > MaxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * MaxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerData.k_CurrentScores[PlayerId]++;
        }
    }
}
