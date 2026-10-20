
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject exitTrigger;

    private int enemyCount;
    private bool levelComplete;

    void Start()
    {
        // Hide the exit until all enemies are dead.
        if (exitTrigger != null)
            exitTrigger.SetActive(false);

        // Count enemies in the scene.
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Debug.Log("Enemies at start: " + enemyCount);

        if (enemyCount == 0)
            CompleteLevel();
    }

    public void EnemyRemoved()
    {
        if (levelComplete)
            return;

        enemyCount = Mathf.Max(0, enemyCount - 1);

        Debug.Log("Enemies remaining: " + enemyCount);

        if (enemyCount == 0)
            CompleteLevel();
    }

    private void CompleteLevel()
    {
        if (levelComplete)
            return;

        levelComplete = true;

        if (exitTrigger != null)
        {
            exitTrigger.SetActive(true);
            Debug.Log("ALL ENEMIES DEAD - EXIT ACTIVATED");
        }
        else
        {
            Debug.LogError("Assign the Exit Trigger in the Inspector!");
        }
    }
}
