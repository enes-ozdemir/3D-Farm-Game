using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public enum AnimState
    {
        Player_Idle,
        Player_Run,
        Player_Walk,
        Player_Watering,
        Player_Harvesting,
        Player_Holding,
        Player_Digging,
    }

    [SerializeField] private Animator animator;
    [SerializeField] private AnimState currentState;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(AnimState newAnimState, float fadeDuration)
    {
        //stop if its the same animation
        if (currentState == newAnimState) return;

        animator.CrossFade(newAnimState.ToString(), fadeDuration);
        // animator.Play(newAnimState.ToString());

        currentState = newAnimState;
    }
}