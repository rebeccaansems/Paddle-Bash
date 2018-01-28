public class PlayerData
{
    public int RewiredPlayerId, GamePlayerId;

    public int PlayerColor = -1;

    public int Score = 0;

    public PlayerPanelData PanelData;

    public PlayerData(int rewiredPlayerId, int gamePlayerId)
    {
        this.RewiredPlayerId = rewiredPlayerId;
        this.GamePlayerId = gamePlayerId;
    }
}