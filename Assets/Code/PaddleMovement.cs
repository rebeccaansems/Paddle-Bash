using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PaddleMovement : MonoBehaviour
{
    public float playerSpeed;
    public float rotateSpeed;

    public int playerId;
    private Player player;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void FixedUpdate()
    {
        if(player.GetAxisRaw("Horizontal") > 0.5f || player.GetAxisRaw("Horizontal") < -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetAxisRaw("Horizontal") * playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (player.GetAxisRaw("Vertical") > 0.5f || player.GetAxisRaw("Vertical") < -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, player.GetAxisRaw("Vertical") * playerSpeed);
        }

        if(player.GetAxisRaw("Horizontal") < 0.5f && player.GetAxisRaw("Horizontal") > -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (player.GetAxisRaw("Vertical") < 0.5f && player.GetAxisRaw("Vertical") > -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        }

        if (player.GetAxisRaw("Rotate") > 0.5f || player.GetAxisRaw("Rotate") < -0.5f)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * player.GetAxisRaw("Rotate") * Time.deltaTime);
        }
    }
}
