using Rewired;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public int PlayerNum = 0;
    public float MaxSpeed = 200f, MinSpeed = 10f;

    public GameObject LinkedPlayer;
    public CameraFollow Camera;
    
    private Player player;

    private void Start()
    {
        if (SessionData.Instance.Players[PlayerNum] == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.GetComponent<BallBeam>().SetColor(SessionData.Instance.Players[PlayerNum].PlayerColor);
        }
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
            LinkedPlayer.GetComponent<PaddleMovement>().PlayerData.Score += 1;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Camera.FollowPlayer(PlayerNum);
        }
    }
}
