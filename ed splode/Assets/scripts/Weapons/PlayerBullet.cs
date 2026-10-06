using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAi enemy = collision.gameObject.GetComponent<EnemyAi>();

            if (enemy != null)
            {
                enemy.TakeDamage(10);
                Debug.Log("Enemy took 10 damage! Health remaining: " + enemy.health);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
}
    }
