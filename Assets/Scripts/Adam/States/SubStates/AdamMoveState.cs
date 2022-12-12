using UnityEngine;

public class AdamMoveState : AdamGroundedState
{
    private string _horizontalAxisAnimationName;
    private string _verticalAxisAnimationName;
    private int _sprintMultiplier;
    private float _movementSpeed;


    public AdamMoveState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody, string horizontalAxisAnimationName = "SpeedX", string verticalAxisAnimationName = "SpeedY") : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
        _horizontalAxisAnimationName = horizontalAxisAnimationName;
        _verticalAxisAnimationName = verticalAxisAnimationName;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (inputHandler.SprintInput && inputHandler.NormalizedInputY > 0 && inputHandler.NormalizedInputX == 0)
        {
            _sprintMultiplier = 2;
            _movementSpeed = controller.m_runSpeed;
        }
        else
        {
            _sprintMultiplier = 1;
            _movementSpeed = controller.m_walkSpeed;
        }

        animator.SetFloat(_horizontalAxisAnimationName, inputHandler.NormalizedInputX);
        animator.SetFloat(_verticalAxisAnimationName, inputHandler.NormalizedInputY * _sprintMultiplier);

        controller.SetVelocity(inputHandler.cameraRelativeAxisInput.x * _movementSpeed, inputHandler.cameraRelativeAxisInput.y * _movementSpeed);

        if (inputHandler.RawAxisInput.x == 0 && inputHandler.RawAxisInput.y == 0)
        {
            stateMachine.SwitchState(controller.IdleState);
        }
    }
}
