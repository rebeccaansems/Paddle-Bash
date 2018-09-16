using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Text CurrentScore;
    public Transform BallParent;
    public GameObject Ball;
    public CameraFollow FollowCamera;

    public GameObject[] Players;
    public Vector2[] BallSpawnLocations;

    void Start()
    {
        string currScore = "";
        foreach (PlayerData players in SessionData.Instance.Players.Where(x => x != null))
        {
            currScore += players.Score + " | ";
        }
        CurrentScore.text = currScore.TrimEnd('|', ' ');

        for (int i = 0; i < SessionData.Instance.Players.Where(x => x != null).ToArray().Length; i++)
        {
            var newBall = Instantiate(Ball);
            newBall.transform.position = BallSpawnLocations[i];
            newBall.transform.parent = BallParent;
            newBall.GetComponent<BallMovement>().Camera = FollowCamera;
            newBall.GetComponent<BallMovement>().PlayerNum = i;
            newBall.GetComponent<BallBeam>().SetColor(SessionData.Instance.Players[i].PlayerColor);
            newBall.GetComponent<BallMovement>().LinkedPlayer = Players[i];
        }
    }
}
