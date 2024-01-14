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
        // Check if the W key is pressed and held down
        bool isAccelerating = Input.GetKey(KeyCode.W);
        Debug.Log("IsAccelerating: " + isAccelerating);

        // Update the "IsAccelerating" parameter in the Animator
        animator.SetBool("IsAccelerating", isAccelerating);

        // If not accelerating, play the "Bored" animation
        if (!isAccelerating)
        {
            animator.Play("Bored", -1, 0f); // -1 means the default layer
        }
        else
        {
            animator.Play("SkateboardingAcceleration", -1, 0f);
        }
    }
}
