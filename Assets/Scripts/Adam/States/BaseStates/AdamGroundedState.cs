using UnityEngine;

public class AdamGroundedState : BaseState
{
    protected bool isGrounded;
    public AdamGroundedState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody) : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("IsGrounded", true);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = controller.CheckIfGrounded();
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool("IsGrounded", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isGrounded)
        {
            stateMachine.SwitchState(controller.InAirState);
        }
        else
        {
            if (inputHandler.JumpInput)
            {
                stateMachine.SwitchState(controller.JumpState);
            }
        }
    }
}
