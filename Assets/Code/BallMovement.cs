using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public int PlayerNum = 0;
    public float MaxSpeed = 200f, MinSpeed = 10f;

    private Vector3 startLocation;
    private Player player;

    private void Start()
    {
        if (GameData.k_Players[PlayerNum] == null)
        {
            Destroy(this.gameObject);
        }

        startLocation = this.transform.position;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = MinSpeed * (GetComponent<Rigidbody2D>().velocity.normalized);

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
