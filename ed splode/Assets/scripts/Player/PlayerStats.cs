using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public HealthBar healthBar;
    public HealthBar face; 
    public HealthBar healthBar2;

    private float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        face.SetSprite(0);
        healthBar.SetSliderMax(maxHealth);
        healthBar2.SetSprite(0);
    }
    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        //change face sprite based on current health
        if (currentHealth <= maxHealth * 0.25f)
        {
            face.SetSprite(2);
        }
        else if (currentHealth <= maxHealth * 0.5f)
        {
            face.SetSprite(1);
        }
        else if (currentHealth >= maxHealth * 0.6f)
        {
            face.SetSprite(0);
        }

        //change health bar sprite based on current health
        if (currentHealth <= maxHealth * 1.0f)
        {
            healthBar2.SetSprite(0);
            Debug.Log("Health bar 2 sprite set to 0");
        }
        if (currentHealth <= maxHealth * 0.9f)
        {
            healthBar2.SetSprite(1);
            Debug.Log("Health bar 2 sprite set to 1");
        }
        if (currentHealth <= maxHealth * 0.77f)
        {
            healthBar2.SetSprite(2);
            Debug.Log("Health bar 2 sprite set to 2");
        }
        if (currentHealth <= maxHealth * 0.65f)
        {
            healthBar2.SetSprite(3);
            Debug.Log("Health bar 2 sprite set to 3");  
        }
        if (currentHealth <= maxHealth * 0.55f)
        {
            healthBar2.SetSprite(4);
            Debug.Log("Health bar 2 sprite set to 4");
        }
        if(currentHealth <= maxHealth * 0.45f)
        {
            healthBar2.SetSprite(5);
            Debug.Log("Health bar 2 sprite set to 5");
        }
        if (currentHealth <= maxHealth * 0.25f)
        {
            healthBar2.SetSprite(6);
            Debug.Log("Health bar 2 sprite set to 6");
        }
        if (currentHealth <= maxHealth * 0.15f)
        {
            healthBar2.SetSprite(7);
            Debug.Log("Health bar 2 sprite set to 7");
        }

        
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            FindFirstObjectByType<GameOverManager>().GameOver();
        }
    }

    private void Die()
    {
        Debug.Log("You died");

        Cursor.visible = true;
    }
}