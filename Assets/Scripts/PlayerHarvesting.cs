using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerHarvesting : CharacterStateBase
{
    //private ParticleSystem particle;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayerManager(animator).isPlayerNotBlocked = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayerManager(animator).isPlayerNotBlocked = false;
       // particle = Instantiate(GetPlayerManager(animator).currentItem.particleSystem,GetPlayerManager(animator).currentItem.itemPrefab.transform);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //GetPlayerAnimationManager(animator).ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Idle, 0);
        GetPlayerManager(animator).isPlayerNotBlocked = true;
        //Destroy(particle);
    }
}