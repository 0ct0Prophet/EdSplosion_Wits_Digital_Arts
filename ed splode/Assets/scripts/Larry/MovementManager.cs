using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public bool canMove = true; //KeyboardMovement
    public bool canLook = true; //Mouse movement

    public void EnableMovement(bool trigger)
    {
        canMove = trigger;
    }

    public void EnableLookOnly(bool trigger)
    {
        canLook = trigger;
    }
}
