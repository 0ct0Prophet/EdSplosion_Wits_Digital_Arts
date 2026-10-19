using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CursorController : MonoBehaviour
{
    float xRotation = 0f;
    float yRotation = 0f;

    private float mouseSensitivityX = 2f;
    private float mouseSensitivityY = 1f;


    //hides the cursor and locks it to the center of the screen
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!MovementManager.Instance.canLook)  return;
        {

            Vector2 mouseDelta;
            if (Mouse.current != null)
            {
                mouseDelta = Mouse.current.delta.ReadValue();
            }
            else if(UnityEngine.Input.mousePresent)
            {
                mouseDelta = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
            }

            else
            {
                return;
            }

            float mouseX = mouseDelta.x * mouseSensitivityX * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivityY * Time.deltaTime;

            //controls x axis rotation (left and right)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //controls y axis rotation (up and down)
            yRotation += mouseX;

            //apllys both rotations
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

    }
}
