using UnityEngine;

public class TransitionController : MonoBehaviour
{

    public Animator transitionAnimator;
    void Start()
    {
        transitionAnimator = GetComponent<Animator>();
        transitionAnimator.Play("Transition");
    }

    
}
