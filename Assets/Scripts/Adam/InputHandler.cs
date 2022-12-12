using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float m_movementSmoothTime = 0.3f;
    public Vector2 RawAxisInput { get; private set; }
    [HideInInspector]
    public Vector2 cameraRelativeAxisInput;
    public float NormalizedInputX { get; private set; }
    public float NormalizedInputY { get; private set; }

    public bool SprintInput { get; private set; }
    public bool JumpInput { get; private set; }

    private Transform _mainCameraTransform;
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private Vector3 _cameraRelatedMovementInput;
    private float _normalizedInputXTarget;
    private float _normalizedInputYTarget;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        CalculateCameraRelativeAxisInput();
    }

    public void OnMovementInput(CallbackContext context)
    {
        
        RawAxisInput = context.ReadValue<Vector2>();
    }

    public void OnSprintInput(CallbackContext context)
    {
        if (context.started)
        {
            SprintInput = true;
        }else if (context.canceled)
        {
            SprintInput = false;
        }
    }

    public void OnJumpInput(CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CalculateCameraRelativeAxisInput()
    {
        _cameraForward = _mainCameraTransform.forward;
        _cameraRight = _mainCameraTransform.right;
        _cameraForward.y = 0;
        _cameraRight.y = 0;
        _cameraForward.Normalize();
        _cameraRight.Normalize();

        _cameraRelatedMovementInput = _cameraForward * RawAxisInput.y + _cameraRight * RawAxisInput.x;

        cameraRelativeAxisInput.x = Mathf.MoveTowards(cameraRelativeAxisInput.x,_cameraRelatedMovementInput.x, m_movementSmoothTime);
        cameraRelativeAxisInput.y = Mathf.MoveTowards(cameraRelativeAxisInput.y, _cameraRelatedMovementInput.z, m_movementSmoothTime);

        _normalizedInputXTarget = Mathf.RoundToInt(RawAxisInput.x);
        _normalizedInputYTarget = Mathf.RoundToInt(RawAxisInput.y);

        NormalizedInputX = Mathf.MoveTowards(NormalizedInputX, _normalizedInputXTarget, m_movementSmoothTime);
        NormalizedInputY = Mathf.MoveTowards(NormalizedInputY, _normalizedInputYTarget, m_movementSmoothTime);
    }
}
