using Rewired;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectContoller : MonoBehaviour
{
    public int[] GameplayLevels;

    public void Update()
    {
        foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter"))
            {
                GameObject.FindGameObjectWithTag("Overall Controller").GetComponent<LevelLoader>().LoadLevel(GameplayLevels[0]);
            }
        }
    }
}
