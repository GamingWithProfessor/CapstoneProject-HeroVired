using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the W key is pressed and hold
        bool isAccelerating = Input.GetKey(KeyCode.W);

        // Update the "IsAccelerating" parameter in the Animator
        animator.SetBool("IsAccelerating", isAccelerating);

        // If not accelerating, play SkateOnBoard animation
        if (!isAccelerating)
        {
            animator.Play("SkateOnBoard");
        }
    }
}
