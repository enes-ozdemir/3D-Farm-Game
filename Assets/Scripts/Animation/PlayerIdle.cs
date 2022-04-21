using UnityEngine;

public class PlayerIdle : CharacterStateBase
{
    [SerializeField] private float fadeDuration = 0.05f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetPlayerManager(animator).isPlayerNotBlocked = true;
        if (GetPlayerMovement(animator).PlayerRun())
        {
            GetPlayerAnimationManager(animator)
                .ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Run, fadeDuration);
        }
        else if (GetPlayerMovement(animator).SetMovementAnimation())
        {
            GetPlayerAnimationManager(animator)
                .ChangeAnimationState(PlayerAnimationManager.AnimState.Player_Walk, fadeDuration);
        }
        else if (GetPlayerManager(animator).CheckIfInteract() != PlayerAnimationManager.AnimState.Player_Idle)
        {
            GetPlayerAnimationManager(animator).ChangeAnimationState(GetPlayerManager(animator).CheckIfInteract(), 0f);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}