using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isFallingHash;

    void Start()
    {
        animator = GetComponent<Animator>();

        //This lets us type isWalkingHash instead of "isWalking" in order to increase performance. It's for optimization.
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool movementPressed = Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("s") | Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        //bool oppositeDirections = (Input.GetKey("w") & Input.GetKey("s")) | (Input.GetKey("a") && Input.GetKey("d"));

        if (!isWalking && movementPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if ((isWalking && !movementPressed))
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (movementPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!movementPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
