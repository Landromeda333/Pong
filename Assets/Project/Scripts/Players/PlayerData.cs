using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int playerNum;        // Número del jugador
    public string playerName;    // Nombre del jugador
    public enum KickDirection
    {
        Right = 1,
        Left = -1
    }

    public KickDirection kickDir;// Dirección del saque

    private void Start()
    {
        PlayersDataManager.Instance.SaveName(playerNum, playerName);
    }
}