using UnityEngine;

public class AdamAbilityState : BaseState
{
    protected bool isAbilityDone;
    public AdamAbilityState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody) : base(stateMachine, controller, inputHandler, animator, rigidbody)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAbilityDone)
        {
            stateMachine.SwitchState(controller.IdleState);
        }
    }
}
