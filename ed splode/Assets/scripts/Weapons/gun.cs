using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class gun : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //Recoil
    public Rigidbody playerRB;
    public float recoilForce;

    //animator
    public Animator animator;
    private string currentAnimation;

    //sound 
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioSource sounds;

    //bools
    public bool shooting, readyToShoot, reloading;

    //References
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic, reloadSmoke;
    public TextMeshProUGUI ammunitionDisplay;

    //bug fixing 
    public bool allowInvoke = true;

    private void Awake()
    {
        //makes sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        sounds = GetComponent<AudioSource>();

    }

    private void Update()
    {
        MyInput();
        
        //Set ammo display if it exists
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft/bulletsPerTap + "/" + magazineSize/bulletsPerTap);
        }
    }

    private void ChangeAnimation(string animationName)
    {

            currentAnimation = animationName;
            animator.Play(animationName);
    }
    
 

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding action
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && !reloading && bulletsLeft < magazineSize)
        {
            Reload();
        }
        //Reload automatically when shooting
        if (readyToShoot && shooting && bulletsLeft <=0 && !reloading)
        {
            Reload();
        }


        //shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;
            sounds.clip = shootSound;
            sounds.Play();
            ChangeAnimation("shoot");
            Shoot();
            

        }
    }

       private void Shoot()
        {
        readyToShoot = false;

        //find the exact position bieng raycasted
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));// sets ray to center of screen
        RaycastHit hit;

        //check if Ray cast hit something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //just a point far away from the player
        }

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread,spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;
        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke  resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //add recoil to player
            playerRB.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        ChangeAnimation("reload");
        sounds.time = 0.1f;
        sounds.clip = reloadSound;
        sounds.Play();
        if (reloadSmoke != null)
        {
            Instantiate(reloadSmoke, attackPoint.position, Quaternion.identity);
        }
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        ChangeAnimation("idle");
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
