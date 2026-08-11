using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // You can add logic here to damage the enemy if needed
            print("hit " + collision.gameObject.name + "!");
            Destroy(gameObject); // Destroy the enemy on collision
        }

        // Destroy the bullet on collision
        if (collision.gameObject.CompareTag("Wall"))
        {
            // You can add logic here to damage the enemy if needed
            print("hit a wall " + collision.gameObject.name + "!");
            Destroy(gameObject); // Destroy the enemy on collision
        }

        // Destroy the bullet on collision
        if (collision.gameObject.CompareTag("Floor"))
        {
            // You can add logic here to damage the enemy if needed
            print("hit the floor " + collision.gameObject.name + "!");
            Destroy(gameObject); // Destroy the enemy on collision
        }
    }
} 