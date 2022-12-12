using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdamController : MonoBehaviour
{
    [Header("Locomotion")]
    public float m_walkSpeed = 1f;
    public float m_runSpeed = 1f;
    public float m_rotationSpeed = 2f;
    public float m_jumpSpeed = 5f;
    [Range(0, 1)]
    public float m_inAirControlMultiplier = 0.5f;

    [Header("Checker Properties")]
    [SerializeField] private float m_groundCheckRadius = 1;

    [Header("Checkers")]
    [SerializeField] private Transform m_groundChecker;

    [Header("Layers")]
    [SerializeField] private LayerMask m_groundLayer;

    private Rigidbody _rigidbody;
    private Vector3 _workspace = Vector3.zero;
    public Vector3 CurrentSpeed { get; private set; }
    private Animator _animator;
    private InputHandler _inputHandler;
    private Transform _cameraTransform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
        _animator = GetComponent<Animator>();
        _cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        InitializeStates();
    }

    private void Update()
    {
        _stateMachine?.Tick();
        CurrentSpeed = _rigidbody.velocity;
    }

    private void FixedUpdate()
    {
        _stateMachine?.PhysicsTick();
        RotatePlayerWithCamera();
    }

    private StateMachine _stateMachine;

    public AdamIdleState IdleState { get; private set; }
    public AdamMoveState MoveState { get; private set; }
    public AdamInAirState InAirState { get; private set; }
    public AdamLandedState LandedState { get; private set; }
    public AdamJumpState JumpState { get; private set; }

    private void InitializeStates()
    {
        _stateMachine = new StateMachine();

        IdleState = new AdamIdleState(_stateMachine, this, _inputHandler, _animator, _rigidbody);
        MoveState = new AdamMoveState(_stateMachine, this, _inputHandler, _animator, _rigidbody);
        InAirState = new AdamInAirState(_stateMachine, this, _inputHandler, _animator, _rigidbody);
        LandedState = new AdamLandedState(_stateMachine, this, _inputHandler, _animator, _rigidbody);
        JumpState = new AdamJumpState(_stateMachine, this, _inputHandler, _animator, _rigidbody);

        _stateMachine.Initialize(IdleState);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
        //SetVelocityY(m_jumpSpeed);
    }

    public void SetSpeedZero()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void SetVelocity(float xSpeed, float ySpeed)
    {
        _workspace = _rigidbody.velocity;
        _workspace.Set(xSpeed, _workspace.y, ySpeed);
        _rigidbody.velocity = _workspace;
    }

    public void SetVelocityY(float ySpeed)
    {
        _workspace = _rigidbody.velocity;
        _workspace.y = ySpeed;
        _rigidbody.velocity = _workspace;
    }

    private void RotatePlayerWithCamera()
    {
        var targetRotation = _cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetRotation, 0), m_rotationSpeed);
    }

    #region Checkers
    public bool CheckIfGrounded()
    {
        return Physics.CheckSphere(m_groundChecker.position, m_groundCheckRadius, m_groundLayer);
    }
    #endregion

    #region AnimationTriggers
    public void AnimationStartTrigger() => _stateMachine.AnimationStartTrigger();
    public void AnimationTrigger() => _stateMachine.AnimationTrigger();
    public void AnimationFinishTrigger()  {_stateMachine.AnimationFinishTrigger(); Debug.Log("Animation Finished: " + _stateMachine.CurrentState); }
    #endregion

    private void OnDrawGizmos()
    {
        if (m_groundChecker)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(m_groundChecker.position, m_groundCheckRadius);
        }
    }
}
