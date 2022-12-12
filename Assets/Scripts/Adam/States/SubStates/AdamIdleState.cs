using UnityEngine;

public class AdamIdleState : AdamGroundedState
{
    private string _horizontalAxisAnimationName;
    private string _verticalAxisAnimationName;
    public AdamIdleState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody, string horizontalAxisAnimationName = "SpeedX", string verticalAxisAnimationName = "SpeedY") : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
        _horizontalAxisAnimationName = horizontalAxisAnimationName;
        _verticalAxisAnimationName = verticalAxisAnimationName;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        animator.SetFloat(_horizontalAxisAnimationName, 0);
        animator.SetFloat(_verticalAxisAnimationName, 0);

        controller.SetSpeedZero();

        if (inputHandler.RawAxisInput.x != 0 || inputHandler.RawAxisInput.y != 0)
        {
            stateMachine.SwitchState(controller.MoveState);
        }
    }

}
