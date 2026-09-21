using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public Camera playerCamara;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float bulletPrefabLifetime = 3f;

    //Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int burstPerBullet = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //Muzzle
    public GameObject muzzleEffect;
    public Animator animator;

    //Reloading
    public float reloadTime;
    public float magazineSize, bulletsLeft;
    public bool isReloading;

    


    //Different shooting modes
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    //Ready to shoot from game start
    public void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = burstPerBullet;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMagazineSound.Play();
        }   

        if (currentShootingMode == ShootingMode.Auto)
        {
            //Holding down the left mouse button to fire the weapon
            isShooting = Mouse.current != null && Mouse.current.leftButton.isPressed;
        }

        else if (currentShootingMode == ShootingMode.Burst || currentShootingMode == ShootingMode.Single)
        {
            isShooting = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        }

        //Manual Reload
        if (Keyboard.current.rKey.wasPressedThisFrame && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        //Automatic Reload
        if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0)
        {
            Reload();
        }


        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = burstPerBullet;
            FireWeaponMethod();
        }

        //Left mouse button click to fire the weapon
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            FireWeaponMethod();
        }

        if(AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/burstPerBullet} / {magazineSize/burstPerBullet}";
        }



    }

    private void FireWeaponMethod()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        SoundManager.Instance.shootingSound.Play();

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //Instantiates the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //Shooting the bullet in the direction of the camera with spread
        bullet.transform.forward = shootingDirection;

        //Shoots the bullet forward with a force    
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        //Destry the bullet after a certain time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        //Check if the shooting is done
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Burst mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeaponMethod", shootingDelay);
        }

    }

    private void Reload()
    {
        isReloading = true;
        SoundManager.Instance.reloadingSound.Play();
        animator.SetTrigger("RELOAD");
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100); // Arbitrary far point if nothing is hit
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float spreadX = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float spreadY = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(spreadX, spreadY, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}