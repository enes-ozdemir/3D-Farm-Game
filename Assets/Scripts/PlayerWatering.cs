using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWatering : CharacterStateBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayerManager(animator).isPlayerNotBlocked = false;
        GetPlayerManager(animator).currentItem.particleSystem.Play();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayerManager(animator).isPlayerNotBlocked  = false;

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayerAnimationManager(animator).ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Idle, 0);
        GetPlayerManager(animator).isPlayerNotBlocked  = true;
        GetPlayerManager(animator).currentParticle.Stop();

    }
}