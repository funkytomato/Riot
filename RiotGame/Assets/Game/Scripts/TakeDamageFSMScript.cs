using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using Eliot.AgentComponents;

public class TakeDamageFSMScript : StateMachineBehaviour
{
    protected Agent agent;
    private Eliot.AgentComponents.Resources resources;
    public GameObject bloodTrail;

    protected GameObject clone;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        agent = animator.gameObject.GetComponent<Eliot.AgentComponents.Agent>();
        if (agent != null)
        {
            resources = agent.GetComponent<Eliot.AgentComponents.Resources>();
            resources = agent.Resources;
        }

        clone = Instantiate(bloodTrail, animator.rootPosition, Quaternion.identity) as GameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Create blood trail if health is low
        if (resources.HealthPoints < 30.0)
        {
            //Set the blood node position relative to the parent
           


            //Create the blood fx



            //Make it delete itself over time


        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(clone);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
