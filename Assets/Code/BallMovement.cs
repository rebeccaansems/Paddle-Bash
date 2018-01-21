using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public int RewiredPlayerId = 0;
    public float MaxSpeed = 200f;

    private Vector2 startLocation;
    private Player player;

    private void Start()
    {
        startLocation = this.transform.position;
        player = ReInput.players.GetPlayer(RewiredPlayerId);
    }

    private void FixedUpdate()
    {
        if (player.GetButton("Reset Ball"))
        {
            this.transform.position = startLocation;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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
        }
    }
}
