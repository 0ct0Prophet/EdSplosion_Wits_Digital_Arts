using UnityEngine;

public class CursorController : MonoBehaviour
{
    //hides the cursor and locks it to the center of the screen
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
