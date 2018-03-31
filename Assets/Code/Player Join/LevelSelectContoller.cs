using Rewired;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectContoller : MonoBehaviour
{
    public int[] GameplayLevels;
    
    public Animator LevelSelectAnimator, PlayerPanelsAnimator;
    public Animator[] SinglePlayerPanels;

    public void Update()
    {
        foreach (PlayerData player in GameData.GetNonNullPlayers().Where(x => x.PanelData.PlayerLocked == true))
        {
            if (ReInput.players.GetPlayer(player.RewiredPlayerId).GetButtonDown("Enter"))
            {
                GameObject.FindGameObjectWithTag("Overall Controller").GetComponent<LevelLoader>().LoadLevel(GameplayLevels[0]);
            }
            else if (LevelSelectAnimator.GetBool("isOnLevelSelectScreen") == true &&
                ReInput.players.GetPlayer(player.GamePlayerId).GetButtonDown("Back"))
            {
                this.GetComponent<LevelSelectContoller>().enabled = false;

                LevelSelectAnimator.SetBool("isOnLevelSelectScreen", false);
                PlayerPanelsAnimator.SetBool("IsOnPlayerScreen", true);
                
                foreach (Animator anim in SinglePlayerPanels)
                {
                    anim.SetBool("IsOnPlayerScreen", true);
                }
            }
        }
    }
}
