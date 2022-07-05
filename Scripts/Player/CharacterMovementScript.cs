
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
 
public class CharacterMovementScript : MonoBehaviour
{
    //NavMeshAgent variable control player movement
    public NavMeshAgent playerNavMeshAgent;

    //A Camera that follow player movement
    public Camera playerCamera;

    //Control the animation clips of player object 
    public Animator playerAnimator;

    //check player is running(moving) or not
    public bool isRunning;

    private GameObject player;

    private void Update()
    {
        // rotate the player character
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.transform.Rotate(0f, -5f, 0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.transform.Rotate(0f, +5f, 0f);
            }
        }
   
    }

    public void movTheCharacter()
    { //if the left button of is clicked
        if (Input.GetMouseButton(0))
        {

            //Unity cast a ray from the position of mouse cursor on-screen toward the 3D scene.
            Ray myRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {

                //Assign ray hit point as Destination of Navemesh Agent (Player)
                playerNavMeshAgent.SetDestination(myRaycastHit.point);
            }
        }

        //Compare the value of the remaining distance and the stopping distance(Destination point)

        if (playerNavMeshAgent.remainingDistance <= playerNavMeshAgent.stoppingDistance)
        {
            //The remaining distance are less or equal than the stopping distance it means character stop moving and reached destination
            isRunning = false;
        }
        else
        {
            //If remaining distance are greater than the stopping distance than character still moving toward Destination
            isRunning = true;
        }

        playerAnimator.SetBool("IsRun", isRunning);
    }
}