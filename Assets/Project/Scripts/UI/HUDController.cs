using UnityEngine;
using UnityEngine.UIElements;

//# Este script controla la HUD del jugador #//
public class HUDController : MonoBehaviour
{
    /* Labels */
    Label _gameState;       // Texto informativo sobre el estado del juego
    Label[] _playerStates;  // Texto informativo sobre el estado de los jugadores

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _playerStates = new[] { root.Q<Label>("player1-state"), root.Q<Label>("player2-state") };

        _gameState = root.Q<Label>("gameState-text");
    }

    /* Método para SO Event UpdateGameStateText */
    // Actualiza el título informativo
    public void UpdateGameState(string stateName)
    {
        if (stateName == "")
        {
            _gameState.style.display = DisplayStyle.None;
        }
        else
        {
            _gameState.text = stateName;
            _gameState.style.display = DisplayStyle.Flex;
        }
    }

    /* Métodos para SO Event OnPlayerReady */
    public void UpdatePlayerState(int playerNum)
    {
        _playerStates[playerNum-1].text = "¡Preparado!";
    }

    /* Métodos para SO Event StartToPlay */
    public void StartGame()
    {
        foreach (Label playerState in _playerStates)
        {
            playerState.style.display = DisplayStyle.None;
        }
    }
}