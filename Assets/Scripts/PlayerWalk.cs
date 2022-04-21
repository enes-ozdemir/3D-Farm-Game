using UnityEngine;

public class PlayerWalk : CharacterStateBase
{
    [SerializeField] private float fadeDuration = 0.05f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetPlayerMovement(animator).PlayerRun())
        {
            GetPlayerAnimationManager(animator)
                .ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Run, fadeDuration);
        }
        else if (!GetPlayerMovement(animator).SetMovementAnimation())
        {
            GetPlayerAnimationManager(animator)
                .ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Idle, fadeDuration);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}