using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int Damage = 10;
    [SerializeField] private float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAi enemy = collision.gameObject.GetComponent<EnemyAi>();

            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
                Debug.Log("Enemy took " + Damage + " damage! Health remaining: " + enemy.health);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
}
    }
