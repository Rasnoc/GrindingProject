using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    float currentProgress;
    bool forward = true;

    bool splineWalkerActive = false;
    bool thirdPersonActive = true;

    public GameObject runningAudio;
    
    private void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<SplineWalker>().enabled == false)
        {
            if (other.gameObject.CompareTag("GrindRail"))
            {
                this.GetComponent<SplineWalker>().enabled = true;
                this.GetComponent<SplineWalker>().spline = other.GetComponent<RailCubeData>().playerActiveSpline;
                this.GetComponent<SplineWalker>().duration = other.GetComponent<RailCubeData>().speed;
                splineWalkerActive = true;
                this.GetComponent<SplineWalker>().progress = other.GetComponent<RailCubeData>().newPosition;
                this.GetComponent<ThirdPersonMovement>().enabled = false;
                thirdPersonActive = false;
                this.GetComponent<ThirdPersonMovement>().Grind();
                this.GetComponent<SplineWalker>().grindParticles.SetActive(true);
                this.GetComponent<SplineWalker>().grindSound.SetActive(true);
            }
        }
        

    }

    private void Update()
    {
        currentProgress = this.GetComponent<SplineWalker>().progress;
        forward = this.GetComponent<SplineWalker>().goingForward;
        if (this.GetComponent<SplineWalker>().goingForward == true)
        { 
            if (!thirdPersonActive && (Input.GetButtonDown("Jump") || currentProgress == 1f))
            {
                this.GetComponent<SplineWalker>().enabled = false;
                splineWalkerActive = false;
                this.GetComponent<ThirdPersonMovement>().enabled = true;
                thirdPersonActive = true;
                this.GetComponent<ThirdPersonMovement>().Jump();
                this.GetComponent<SplineWalker>().grindParticles.SetActive(false);
                this.GetComponent<SplineWalker>().grindSound.SetActive(false);
            }
        }
        if (this.GetComponent<SplineWalker>().goingForward == false)
        {
            Debug.Log(currentProgress);
            if (!thirdPersonActive && (Input.GetButtonDown("Jump") || currentProgress <= 0.005f))
            {
                this.GetComponent<SplineWalker>().enabled = false;
                splineWalkerActive = false;
                this.GetComponent<ThirdPersonMovement>().enabled = true;
                thirdPersonActive = true;
                this.GetComponent<ThirdPersonMovement>().Jump();
                this.GetComponent<SplineWalker>().grindParticles.SetActive(false);
                this.GetComponent<SplineWalker>().grindSound.SetActive(false);
            }
        }

        if (Input.GetKeyDown("s") && splineWalkerActive)
        {
            this.GetComponent<SplineWalker>().goingForward = !this.GetComponent<SplineWalker>().goingForward;
            Debug.Log("Backwards");
        }

        if (this.GetComponent<ThirdPersonMovement>().isGrounded == true && Input.GetKey("left shift") && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
        {
            runningAudio.SetActive(true);
        }
        else
        {
            runningAudio.SetActive(false);
        }

    }

}
