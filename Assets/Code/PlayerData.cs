public class PlayerData
{
    public int RewiredPlayerId, GamePlayerId;

    public int PlayerColor = -1;

    public PlayerData(int rewiredPlayerId, int gamePlayerId)
    {
        this.RewiredPlayerId = rewiredPlayerId;
        this.GamePlayerId = gamePlayerId;
    }
}