using UnityEngine;

public class ReloadSoundRelay : MonoBehaviour
{
    public gun playerGun;

    public void PlayShellLoadSound()
    {
        if (playerGun != null)
        {
            playerGun.PlayShellLoadSound();
        }
    }
}