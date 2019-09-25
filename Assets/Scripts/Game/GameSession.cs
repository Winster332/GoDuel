namespace Game
{
  public class GameSession
  {
    private static GameSession _instance;
    public bool IsShotRival;
    public bool IsShotPlayer;
    public bool IsPlayerWin;
    public bool IsRivalWin;

    public static GameSession Get()
    {
      return _instance ?? (_instance = new GameSession());
    }

    private GameSession()
    {
    }

    public void Begin()
    {
      IsShotRival = false;
      IsShotPlayer = false;
      IsPlayerWin = false;
      IsRivalWin = false;
    }

    public bool IsWin()
    {
      if (IsRivalWin)
        return false;
      
      return IsPlayerWin;
    }

    public bool IsGameEnd()
    {
      if (IsShotPlayer && IsShotRival)
        return true;
      if (IsPlayerWin)
        return true;
      if (IsRivalWin)
        return true;

      return false;
    }
  }
}