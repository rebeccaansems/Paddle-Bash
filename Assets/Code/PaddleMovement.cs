using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public int PlayerNum;

    public float PlayerSpeed;
    public float RotateSpeed;

    private Player player;
    private Vector3 moveVector;

    private int rewiredPlayerId = 0;

    private bool rotateLeft, rotateRight;


    private void Awake()
    {
        if (GameData.k_Players[PlayerNum] == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            rewiredPlayerId = GameData.k_Players[PlayerNum].RewiredPlayerId;
            player = ReInput.players.GetPlayer(rewiredPlayerId);
            player.controllers.maps.SetMapsEnabled(true, "Game");
            player.controllers.maps.SetMapsEnabled(false, "Menu");
            this.GetComponent<PaddleBeam>().SetColor(GameData.k_Players[PlayerNum].PlayerColor);
        }
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
            transform.Rotate(Vector3.back * RotateSpeed * Time.deltaTime);
        }
    }
}
