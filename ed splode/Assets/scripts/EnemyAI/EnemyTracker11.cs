
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    private bool counted;

    void Start()
    {
        counted = true;
    }

    void OnDestroy()
    {
        if (!counted)
            return;

        EnemyManager manager = FindObjectOfType<EnemyManager>();

        if (manager != null)
        {
            manager.EnemyRemoved();
        }
    }
}
