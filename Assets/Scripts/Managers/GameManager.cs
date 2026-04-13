using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerInputActions playerInputs;

    [HideInInspector] public PlayerMovement player1Movement, player2Movement;

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

    public void ActivatePlayerInputs(int playerNum, PlayerMovement playerMovement)
    {
        switch (playerNum)
        {
            case 1:
                playerInputs.Player1.Enable();
                player1Movement = playerMovement;
                playerInputs.Player1.Move.performed += ctx =>
                {
                    player1Movement.direction = ctx.ReadValue<float>();
                };
                playerInputs.Player1.Move.canceled += ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        player1Movement.direction = 0;
                    }
                };
                playerInputs.Player1.KickGoal.performed += ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
                };
                break;

            case 2:
                playerInputs.Player2.Enable();
                player2Movement = playerMovement;
                playerInputs.Player2.Move.performed += ctx =>
                {
                    player2Movement.direction = ctx.ReadValue<float>();
                };
                playerInputs.Player2.Move.canceled += ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        player2Movement.direction = 0;
                    }
                };
                playerInputs.Player2.KickGoal.performed += ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
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
                    player1Movement.direction = ctx.ReadValue<float>();
                };
                playerInputs.Player1.Move.canceled -= ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        player1Movement.direction = 0;
                    }
                };
                playerInputs.Player1.KickGoal.performed -= ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
                };
                break;

            case 2:
                playerInputs.Player2.Disable();
                playerInputs.Player2.Move.performed -= ctx =>
                {
                    player2Movement.direction = ctx.ReadValue<float>();
                };
                playerInputs.Player1.Move.canceled -= ctx =>
                {
                    if (ctx.ReadValue<float>() == 0)
                    {
                        player1Movement.direction = 0;
                    }
                };
                playerInputs.Player2.KickGoal.performed -= ctx =>
                {
                    LvlManager.Instance.ChangePlayerState(playerNum);
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