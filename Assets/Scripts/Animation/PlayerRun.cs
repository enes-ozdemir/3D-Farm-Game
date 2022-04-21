using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : CharacterStateBase
{
    [SerializeField] private float fadeDuration = 0.05f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!GetPlayerMovement(animator).PlayerRun())
        {
            if (GetPlayerMovement(animator).SetMovementAnimation())
            {
                GetPlayerAnimationManager(animator)
                    .ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Walk, fadeDuration);
            }
            else
            {
                GetPlayerAnimationManager(animator)
                    .ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Idle, fadeDuration);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}