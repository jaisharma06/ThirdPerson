using UnityEngine;

public class AdamLandedState : AdamAbilityState
{
    public AdamLandedState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody) : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
}
