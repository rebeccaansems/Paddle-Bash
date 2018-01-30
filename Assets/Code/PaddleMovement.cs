using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public int PlayerNum;

    public float PlayerSpeed;
    public float RotateSpeed;

    public PlayerData PlayerData;

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
            PlayerData = GameData.k_Players[PlayerNum];
            rewiredPlayerId = PlayerData.RewiredPlayerId;
            player = ReInput.players.GetPlayer(rewiredPlayerId);
            player.controllers.maps.SetMapsEnabled(true, "Game");
            player.controllers.maps.SetMapsEnabled(false, "Menu");
            this.GetComponent<PaddleBeam>().SetColor(PlayerData.PlayerColor);
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
        GetComponent<Rigidbody2D>().velocity = moveVector * PlayerSpeed * Time.fixedDeltaTime;

        if (rotateLeft && !rotateRight)
        {
            transform.Rotate(Vector3.forward * RotateSpeed * Time.fixedDeltaTime);
        }
        else if (!rotateLeft && rotateRight)
        {
            transform.Rotate(Vector3.back * RotateSpeed * Time.fixedDeltaTime);
        }
    }
}
