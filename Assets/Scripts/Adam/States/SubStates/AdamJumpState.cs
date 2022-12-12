using UnityEngine;

public class AdamJumpState : AdamAbilityState
{
    private string _jumpAnimationName;
    public AdamJumpState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody, string jumpAnimationName = "Jump") : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
        _jumpAnimationName = jumpAnimationName;
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(_jumpAnimationName, true);
        isAbilityDone = false;
        controller.Jump();
        inputHandler.UseJumpInput();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isAbilityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(_jumpAnimationName, false);
    }

    public override void LogicUpdate()
    {
        if (isAbilityDone)
        {
            stateMachine.SwitchState(controller.InAirState);
        }
    }
}
