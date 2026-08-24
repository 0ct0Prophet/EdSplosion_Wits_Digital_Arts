using UnityEngine;

public class CursorController : MonoBehaviour
{
    //hides the cursor and locks it to the center of the screen
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
