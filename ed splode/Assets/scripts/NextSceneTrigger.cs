
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string nextSceneName;
    public Animator animator;
    private string currentAnimation;
    private void ChangeAnimation(string animationName)
    {

        currentAnimation = animationName;
        animator.Play(animationName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player entered exit trigger. Loading " + nextSceneName);

        SceneManager.LoadScene(nextSceneName);
    }
}
