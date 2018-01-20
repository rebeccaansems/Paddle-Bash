using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public int PlayerId = 0;

    public float maxSpeed = 200f;

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Finish")
        {
            Scores.k_CurrentScores[PlayerId]++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
