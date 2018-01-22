using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public int RewiredPlayerId = 0;

    public float PlayerSpeed;
    public float RotateSpeed;

    private Player player;
    private Vector3 moveVector;

    private bool rotateLeft, rotateRight;
    private bool canMove;


    private void Awake()
    {
        player = ReInput.players.GetPlayer(RewiredPlayerId);
        player.controllers.maps.SetMapsEnabled(false, 0);
        player.controllers.maps.SetMapsEnabled(true, 1);
    }

    private void FixedUpdate()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        moveVector.x = player.GetAxis("Horizontal");
        moveVector.y = player.GetAxis("Vertical");

        rotateLeft = player.GetButton("Rotate Left");
        rotateRight = player.GetButton("Rotate Right");
    }

    private void ProcessInput()
    {
        GetComponent<Rigidbody2D>().velocity = moveVector * PlayerSpeed;

        if (rotateLeft && !rotateRight)
        {
            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
        }
        else if (!rotateLeft && rotateRight)
        {
            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime * -1);
        }
    }
}
