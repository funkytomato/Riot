using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using Pathfinding.RVO;

/*
 * Sets the animator properties Speed and isWalking.
 * isWalking is to check for Idle and Walking velocity using RVOController.velocity.
 */
public class MovementAnimationScript : MonoBehaviour
{
    public GameObject cloneObject;
    private AIPath aiPath;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (cloneObject != null) 
        {
            aiPath = GetComponent<AIPath>();
            
        }
        else
        {
            Debug.Log("CivilianAnimationComponent CloneObject not set");
        }


        animator = cloneObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        //animator = cloneObject.GetComponent<Animator>();
        animator = gameObject.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            Vector3 velocity;
            velocity = GetComponent<RVOController>().velocity;

            Debug.Log(velocity);
            animator.SetBool("isWalking", velocity.magnitude > 0.2f);

            if (aiPath != null)
            {
                animator.SetFloat("Speed", aiPath.maxSpeed);

            }
            else
            {
                Debug.Log("CivilianAnimationComponent aiPath not found");
            }
        }
        else
        {
            Debug.Log("CivilianAnimationComponent animator not found");
        }
    }
}
