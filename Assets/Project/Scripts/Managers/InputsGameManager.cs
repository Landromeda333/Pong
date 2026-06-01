using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//# Este script se encarga de gestionar los controles de la partida #//
public class InputsGameManager : MonoBehaviour
{
    /* SO Events */
    [SerializeField] GameEvent onPauseRequested, onBackPressed; // Solicita el menú de pausa, Pulsación de Esc
    [SerializeField] IntGameEvent onPlayerReady;                // Avisa de qué jugador está preparado

    PlayerInputActions playerInputs;                            // Inputs

    [HideInInspector] public Dictionary<int,PlayerBehaviour> players = new ();  // Diccionario con el número y scripts de los jugadores

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
    }

    /* Método para SO Event InGameOnly */
    // Configura los controles en solo el juego
    public void InGameOnly()
    {
        DeactivateUIInput();
        for (int i = 1; i <= players.Count; i++)
        {
            ActivatePlayerInputs(i);
        }
    }

    /* Método para SO Event UIOnly */
    // Configura los controles para solo UI
    public void UIOnly()
    {
        for (int i = 1; i <= players.Count; i++)
        {
            DeactivatePlayerInputs(i);
        }
        ActivateUIInput();
    }

    /* Métodos para evitar fugas de referencias en las suscripciones de los controles */
    /* Player 1 Inputs */
    void OnPlayer1PausePerformed(InputAction.CallbackContext ctx)
    {
        onPauseRequested.Raise();
    }

    void OnPlayer1MovePerformed(InputAction.CallbackContext ctx)
    {
        if (players.TryGetValue(1, out PlayerBehaviour player))
        {
            player.movement.direction = ctx.ReadValue<float>();
        }
    }

    void OnPlayer1MoveCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            if (players.TryGetValue(1, out PlayerBehaviour player))
            {
                player.movement.direction = 0;
            }
        }
    }

    void OnPlayer1KickGoalPerformed(InputAction.CallbackContext ctx)
    {
        if (players.TryGetValue(1, out PlayerBehaviour player))
        {
            onPlayerReady.Raise(1);
            player.GoalKick();
        }
    }

    /* Player 2 Inputs */
    void OnPlayer2MovePerformed(InputAction.CallbackContext ctx)
    {
        if (players.TryGetValue(2, out PlayerBehaviour player))
        {
            player.movement.direction = ctx.ReadValue<float>();
        }
    }

    void OnPlayer2MoveCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            if (players.TryGetValue(2, out PlayerBehaviour player))
            {
                player.movement.direction = 0;
            }
        }
    }

    void OnPlayer2KickGoalPerformed(InputAction.CallbackContext ctx)
    {
        if (players.TryGetValue(2, out PlayerBehaviour player))
        {
            onPlayerReady.Raise(2);
            player.GoalKick();
        }
    }

    /* UI Inputs */
    void OnBackPerformed(InputAction.CallbackContext ctx)
    {
        onBackPressed.Raise();
    }

    // Activación de los controles de un jugador
    public void ActivatePlayerInputs(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                playerInputs.Player1.Enable();
                playerInputs.Player1.Move.performed += OnPlayer1MovePerformed;

                playerInputs.Player1.Move.canceled += OnPlayer1MoveCanceled;

                playerInputs.Player1.KickGoal.performed += OnPlayer1KickGoalPerformed;

                playerInputs.Player1.Pause.performed += OnPlayer1PausePerformed;
                break;

            case 2:
                playerInputs.Player2.Enable();

                playerInputs.Player2.Move.performed += OnPlayer2MovePerformed;

                playerInputs.Player2.Move.canceled += OnPlayer2MoveCanceled;

                playerInputs.Player2.KickGoal.performed += OnPlayer2KickGoalPerformed;
                break;
        }
    }

    // Desactivación de los controles de un jugador
    public void DeactivatePlayerInputs(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                playerInputs.Player1.Move.performed -= OnPlayer1MovePerformed;

                playerInputs.Player1.Move.canceled -= OnPlayer1MoveCanceled;

                playerInputs.Player1.KickGoal.performed -= OnPlayer1KickGoalPerformed;

                playerInputs.Player1.Pause.performed -= OnPlayer1PausePerformed;
                break;

            case 2:
                playerInputs.Player2.Move.performed -= OnPlayer2MovePerformed;

                playerInputs.Player2.Move.canceled -= OnPlayer2MoveCanceled;

                playerInputs.Player2.KickGoal.performed -= OnPlayer2KickGoalPerformed;

                playerInputs.Player2.Disable();
                break;
        }
    }

    // Activación de los controles de la UI
    void ActivateUIInput()
    {
        playerInputs.UI.Enable();
        playerInputs.UI.Back.performed += OnBackPerformed;
        MouseCursorController.UnlockCursor();
    }

    // Desactivación de los controles de la UI
    void DeactivateUIInput()
    {
        playerInputs.UI.Back.performed -= OnBackPerformed;
        playerInputs.UI.Disable();
        MouseCursorController.LockCursor();
    }

    /* Métodos para Game Event Listener y String Game Event Listener*/
    public void ClearPlayerMovement()
    {
        players.Clear();
    }

    /* Métodos para Int-PlayerBehaviour Game Event Listener */
    public void RegisterPlayer(int playerNum, PlayerBehaviour player)
    {
        players.Add(playerNum, player);
    }
}