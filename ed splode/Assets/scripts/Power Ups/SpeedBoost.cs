using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public GameObject pickUpEffect;
    public float duration = 5f;
    public float speedIncrease = 5f;
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    public IEnumerator PickUp(Collider player) 
    {
        Debug.Log("Speed Boost Picked Up");

        Instantiate(pickUpEffect, transform.position, transform.rotation);
        //Spawn effect


        //Gives PLayer effect
        player.GetComponent<Movement>().walkspeed += speedIncrease;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //Wait time to reverse effects
        yield return new WaitForSeconds(duration);
        player.GetComponent<Movement>().walkspeed -= speedIncrease;
        //Destroys the object
        Destroy(gameObject);
    }

}
