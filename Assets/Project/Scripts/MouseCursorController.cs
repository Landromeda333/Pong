using UnityEngine;

/* Este script se encarga de gestionar el estado del cursor */
public class MouseCursorController : MonoBehaviour
{
    /* Bloquea y oculta el cursor */
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /* Desbloquea y muestra el cursor */
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}