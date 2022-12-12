using UnityEngine;

public class BaseState
{
	protected AdamController controller;
	protected InputHandler inputHandler;
	protected Animator animator;
	protected Rigidbody rigidbody;
	protected StateMachine stateMachine;

	public BaseState(StateMachine stateMachine, AdamController controller, InputHandler inputHandler, Animator animator, Rigidbody rigidbody)
	{
		this.controller = controller;
		this.inputHandler = inputHandler;
		this.rigidbody = rigidbody;
		this.animator = animator;
		this.stateMachine = stateMachine;
	}

	public virtual void Enter()
	{
		DoChecks();
	}

	public virtual void Exit()
	{

	}

	public virtual void LogicUpdate()
	{

	}

	public virtual void PhysicsUpdate()
	{
		DoChecks();
	}

	public virtual void DoChecks()
	{

	}

	public virtual void AnimationStartTrigger()
	{

	}

	public virtual void AnimationFinishTrigger()
	{

	}

	public virtual void AnimationTrigger()
	{

	}
}
