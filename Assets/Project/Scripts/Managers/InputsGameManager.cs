using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsGameManager : MonoBehaviour
{
    public static InputsGameManager Instance;

    [SerializeField] GameEvent onPauseRequested, onBackPressed;

    PlayerInputActions playerInputs;

    [HideInInspector] public List<PlayerMovement> playersMovement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerInputs = new PlayerInputActions();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /* Métodos */

    public void InGameOnly()
    {
        DeactivateUIInput();
        ActivatePlayerInputs(1);
        ActivatePlayerInputs(2);
    }

    public void UIOnly()
    {
        DeactivatePlayerInputs(1);
        DeactivatePlayerInputs(1);
        ActivateUIInput();
    }


    /* Player 1 Inputs */
    void OnPlayer1PausePerformed(InputAction.CallbackContext ctx)
    {
        onPauseRequested.Raise();
    }

    void OnPlayer1MovePerformed(InputAction.CallbackContext ctx)
    {
        playersMovement[0].direction = ctx.ReadValue<float>();
    }

    void OnPlayer1MoveCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            playersMovement[0].direction = 0;
        }
    }

    void OnPlayer1KickGoalPerformed(InputAction.CallbackContext ctx)
    {
        LvlManager.Instance.ChangePlayerState(1);
        playersMovement[0].GoalKick();
    }

    /* Player 2 Inputs */
    void OnPlayer2MovePerformed(InputAction.CallbackContext ctx)
    {
        playersMovement[1].direction = ctx.ReadValue<float>();
    }

    void OnPlayer2MoveCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0)
        {
            playersMovement[1].direction = 0;
        }
    }

    void OnPlayer2KickGoalPerformed(InputAction.CallbackContext ctx)
    {
        LvlManager.Instance.ChangePlayerState(2);
        playersMovement[1].GoalKick();
    }

    /* UI Inputs */
    void OnBackPerformed(InputAction.CallbackContext ctx)
    {
        onBackPressed.Raise();
    }

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

    public void DeactivatePlayerInputs(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                playerInputs.Player1.Move.performed -= OnPlayer1MovePerformed;

                playerInputs.Player1.Move.canceled -= OnPlayer1MoveCanceled;

                playerInputs.Player1.KickGoal.performed -= OnPlayer1KickGoalPerformed;

                playerInputs.Player1.Pause.performed -= OnPlayer1PausePerformed;
                playerInputs.Player1.Disable();
                break;

            case 2:
                playerInputs.Player2.Move.performed -= OnPlayer2MovePerformed;

                playerInputs.Player2.Move.canceled -= OnPlayer2MoveCanceled;

                playerInputs.Player2.KickGoal.performed -= OnPlayer2KickGoalPerformed;

                playerInputs.Player2.Disable();
                break;
        }
    }

    void ActivateUIInput()
    {
        playerInputs.UI.Enable();
        playerInputs.UI.Back.performed += OnBackPerformed;
        MouseCursorController.UnlockCursor();
    }

    void DeactivateUIInput()
    {
        playerInputs.UI.Back.performed -= OnBackPerformed;
        playerInputs.UI.Disable();
        MouseCursorController.LockCursor();
    }
}