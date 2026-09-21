using UnityEngine;
public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        // Destroy the bullet on collision
        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            // You can add logic here to damage the enemy if needed
            print("hit " + objectWeHit.gameObject.name + "!");
            CreatBulletImpactEffect(objectWeHit);
            Destroy(gameObject); // Destroy the enemy on collision
        }

        // Destroy the bullet on collision
        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            // You can add logic here to damage the enemy if needed
            print("hit a wall " + objectWeHit.gameObject.name + "!");
            CreatBulletImpactEffect(objectWeHit);
            Destroy(gameObject); // Destroy the enemy on collision
        }

        // Destroy the bullet on collision
        if (objectWeHit.gameObject.CompareTag("Floor"))
        {
            // You can add logic here to damage the enemy if needed
            print("hit the floor " + objectWeHit.gameObject.name + "!");
            CreatBulletImpactEffect(objectWeHit);
            Destroy(gameObject); // Destroy the enemy on collision
        }
    }

    void CreatBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
} 