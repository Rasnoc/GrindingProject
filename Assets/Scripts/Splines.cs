using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splines : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform pointAB;
    [SerializeField] private Transform pointBC;
    [SerializeField] private Transform pointAB_BC;

    public GameObject player;
    public Transform playerTrans;
    ThirdPersonMovement playerScript;
    bool onRail = false;
    public bool reverse = false;

    private float interpolateAmount;

    private void Start()
    {
        ThirdPersonMovement playerScript = player.GetComponent<ThirdPersonMovement>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onRail = true;
        }


    }

    private void Update()
    {
        if(!reverse)
        {
            interpolateAmount = (interpolateAmount + .0015f) % 1f;
        }
        else
        {
            interpolateAmount = 1f - ((interpolateAmount + .0015f) % 1f);
        }
        
        
        pointAB.position = Vector3.Lerp(pointA.position, pointB.position, interpolateAmount);
        pointBC.position = Vector3.Lerp(pointB.position, pointC.position, interpolateAmount);
        pointAB_BC.position = Vector3.Lerp(pointAB.position, pointBC.position, interpolateAmount);

        if (onRail == true && !Input.GetButtonDown("Jump"))
        {
            playerScript.isGrinding = true;
            playerTrans.position = Vector3.Lerp(pointAB.position, pointBC.position, interpolateAmount);
            Debug.Log("Is detecting");
        }
        else
        {
            playerScript.isGrinding = false;
        }


    }
}
