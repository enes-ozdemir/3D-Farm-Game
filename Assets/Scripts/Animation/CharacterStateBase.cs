using UnityEngine;

public class CharacterStateBase : StateMachineBehaviour
{
    private PlayerAnimationManager _playerAnimationManager;
    private PlayerMovement _playerMovement;
    private PlayerManager _playerManager;

    public PlayerAnimationManager GetPlayerAnimationManager(Animator animator)
    {
        if (_playerAnimationManager == null)
        {
            _playerAnimationManager = animator.GetComponentInParent<PlayerAnimationManager>();
        }

        return _playerAnimationManager;
    }

    public PlayerMovement GetPlayerMovement(Animator animator)
    {
        if (_playerMovement == null)
        {
            _playerMovement = animator.GetComponentInParent<PlayerMovement>();
        }

        return _playerMovement;
    }

    public PlayerManager GetPlayerManager(Animator animator)
    {
        if (_playerManager == null)
        {
            _playerManager = animator.GetComponentInParent<PlayerManager>();
        }

        return _playerManager;
    }
}