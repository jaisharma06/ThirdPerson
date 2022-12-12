using UnityEngine;

public class AdamInAirState : BaseState
{
    private string _inAirAnimationName;
    protected bool isGrounded;

    private const float gravity = 9.81f;
    private const float mass = 0.3f;

    public AdamInAirState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody, string inAirAnimationName = "IsGrounded") : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
        _inAirAnimationName = inAirAnimationName;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = controller.CheckIfGrounded();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
         
        animator.SetBool(_inAirAnimationName, isGrounded);
        animator.SetFloat("SpeedY", rigidbody.velocity.y);

        rigidbody.velocity -= (Vector3.up * gravity * Time.deltaTime);

        if(Mathf.Approximately(rigidbody.velocity.y, 0f))
        {
            controller.SetVelocityY(0);
        }

        if (isGrounded)
        {
            rigidbody.velocity -= (Vector3.up * gravity * mass);
        }

        if (isGrounded && controller.CurrentSpeed.y < 0)//Switch To Landed state
        {
            stateMachine.SwitchState(controller.LandedState);
        }
    }
}
