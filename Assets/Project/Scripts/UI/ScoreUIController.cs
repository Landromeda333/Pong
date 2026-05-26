using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//# Este script se encarga de getionar la UI de los marcadores #//
public class ScoreUIController : MonoBehaviour
{
    /* Labels */
    Label _scoreLeft, _scoreRight;                               // Marcadores

    /* Dictionaries */
    Dictionary<int, Label> scores = new();// Asiganación de cada marcador a un jugador

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _scoreLeft = root.Q<Label>("score-left");
        _scoreRight = root.Q<Label>("score-right");
    }

    // Asignación de los marcadores
    private void Start()
    {
        scores.Add(1, _scoreRight);
        scores.Add(2, _scoreLeft);
    }

    /* Método para el SO Event PlayerScored */
    public void UpdateScore(int playerNumber, int score)
    {
        if (scores.TryGetValue(playerNumber, out Label scoreText))
        {
            scoreText.text = score.ToString();
        }
    }
}