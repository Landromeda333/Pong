using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerInputActions playerInputs;
    [HideInInspector] public List<PlayerMovement> playersMovement;

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ActivatePlayerInputs(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                playerInputs.Player1.Enable();
                playerInputs.Player1.Move.performed += ctx =>
                {
                    playersMovement[playerNum - 1].direction = ctx.ReadValue<float>();
                };
                playerInputs.Player1.Move.canceled += ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        playersMovement[playerNum - 1].direction = 0;
                    }
                };
                playerInputs.Player1.KickGoal.performed += ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
                    playersMovement[playerNum - 1].GoalKick();
                };
                playerInputs.Player1.Pause.performed += ctx =>
                {
                    LvlManager.Instance.SetGameState(LvlManager.GameState.Pause);
                };
                break;

            case 2:
                playerInputs.Player2.Enable();
                playerInputs.Player2.Move.performed += ctx =>
                {
                    playersMovement[playerNum - 1].direction = ctx.ReadValue<float>();
                };
                playerInputs.Player2.Move.canceled += ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        playersMovement[playerNum - 1].direction = 0;
                    }
                };
                playerInputs.Player2.KickGoal.performed += ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
                    playersMovement[playerNum - 1].GoalKick();
                };
                break;
        }
    }

    public void DeactivatePlayerInputs(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                playerInputs.Player1.Disable();
                playerInputs.Player1.Move.performed -= ctx =>
                {
                    playersMovement[playerNum - 1].direction = ctx.ReadValue<float>();
                };
                playerInputs.Player1.Move.canceled -= ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        playersMovement[playerNum - 1].direction = 0;
                    }
                };
                playerInputs.Player1.KickGoal.performed -= ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
                    playersMovement[playerNum - 1].GoalKick();
                };
                playerInputs.Player1.Pause.performed -= ctx =>
                {
                    LvlManager.Instance.SetGameState(LvlManager.GameState.Pause);
                };
                break;

            case 2:
                playerInputs.Player2.Disable();
                playerInputs.Player2.Move.performed -= ctx =>
                {
                    playersMovement[playerNum - 1].direction = ctx.ReadValue<float>();
                };
                playerInputs.Player2.Move.canceled -= ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        playersMovement[playerNum - 1].direction = 0;
                    }
                };
                playerInputs.Player2.KickGoal.performed -= ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
                    playersMovement[playerNum - 1].GoalKick();
                };
                break;
        }
    }

    public void ActivateUIInput()
    {
        playerInputs.UI.Enable();
        MouseCursorController.UnlockCursor();
    }

    public void DeactivateUIInput()
    {
        playerInputs.UI.Disable();
        MouseCursorController.LockCursor();
    }
}