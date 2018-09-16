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
        if (SessionData.Instance.Players[PlayerNum] == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            PlayerData = SessionData.Instance.Players[PlayerNum];
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

#if UNITY_EDITOR || UNITY_WINDOWS
        moveVector.y = moveVector.y * -1;
#endif

        rotateLeft = player.GetButton("Rotate Left");
        rotateRight = player.GetButton("Rotate Right");
    }

    private void ProcessInput()
    {
        GetComponent<Rigidbody2D>().velocity = moveVector * PlayerSpeed * Time.fixedDeltaTime;

        if (rotateLeft && !rotateRight)
        {
            GetComponent<Rigidbody2D>().freezeRotation = false;
            GetComponent<Rigidbody2D>().AddTorque(RotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else if (!rotateLeft && rotateRight)
        {
            GetComponent<Rigidbody2D>().freezeRotation = false;
            GetComponent<Rigidbody2D>().AddTorque(-RotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
}
