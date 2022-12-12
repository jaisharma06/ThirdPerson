using UnityEngine;

public class StateMachine
{
    public BaseState CurrentState { get; set; }
    
    public void Tick()
    {
        CurrentState?.LogicUpdate();
    }

    public void PhysicsTick()
    {
        CurrentState?.PhysicsUpdate();
    }

    public void SwitchState(BaseState newState)
    {
        if(newState != null && newState != CurrentState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();

            Debug.Log(CurrentState.GetType());
        }
    }

    public void Initialize(BaseState initialState)
    {
        CurrentState = initialState;
        CurrentState?.Enter();
    }

    public void AnimationStartTrigger() => CurrentState?.AnimationStartTrigger();
    public void AnimationTrigger() => CurrentState?.AnimationTrigger();
    public void AnimationFinishTrigger() => CurrentState?.AnimationFinishTrigger();
}
