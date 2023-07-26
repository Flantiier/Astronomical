using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class FPSController : MonoBehaviour
{
    #region Variables
    private Transform _camera;
    private CharacterController _cc;
    private PlayerInput _inputs;

    #region Motion
    [Header("Motion & View")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField, Range(0f, 0.1f)] private float smoothInputs = 0.05f;
    [SerializeField, Range(0f, 0.1f)] private float smoothSpeed = 0.05f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 5f;

    private Vector2 _lookInputs;
    private Vector2 _rawInputs;
    private Vector2 _currentInputs;
    [HideInInspector] private float _xRotation = 0f;
    [HideInInspector] private Vector2 _inputsRef;
    private Vector3 _movement;
    private float _currentSpeed;
    #endregion

    #region Physics
    [Header("Physics")]
    [SerializeField] private float gravity = 3f;
    [SerializeField] private float maxHorizontalVel = 20f;
    private float _appliedGravity;
    #endregion

    #endregion

    #region Properties
    public bool IsGrounded => _cc.isGrounded;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _cc = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        EnableInputs(true);
        SubscribeToInputs();
    }

    private void OnDisable()
    {
        EnableInputs(false);
        UnsubscribeToInputs();
    }

    private void Update()
    {
        HandleView();
        HandleGravity();
        HandleMovement();
    }
    #endregion

    #region Inputs Methods
    /// <summary>
    /// Enable or disable player inputs
    /// </summary>
    private void EnableInputs(bool enabled)
    {
        if (enabled)
            _inputs.ActivateInput();
        else
            _inputs.DeactivateInput();
    }

    /// <summary>
    /// Subscribe methods to player actions
    /// </summary>
    private void SubscribeToInputs()
    {
        _inputs.currentActionMap.FindAction("Move").performed += OnMove;
        _inputs.currentActionMap.FindAction("Move").canceled += OnMove;
        _inputs.currentActionMap.FindAction("Look").performed += OnLook;
        _inputs.currentActionMap.FindAction("Look").canceled += OnLook;
    }

    /// <summary>
    /// Unsubscribe methods to player actions
    /// </summary>
    private void UnsubscribeToInputs()
    {
        _inputs.currentActionMap.FindAction("Move").performed -= OnMove;
        _inputs.currentActionMap.FindAction("Move").canceled -= OnMove;
        _inputs.currentActionMap.FindAction("Look").performed -= OnLook;
        _inputs.currentActionMap.FindAction("Look").canceled -= OnLook;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        _rawInputs = ctx.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        _lookInputs = ctx.ReadValue<Vector2>();
    }
    #endregion

    #region Movement Methods
    /// <summary>
    /// Move the player around
    /// </summary>
    private void HandleMovement()
    {
        float targetSpeed = GetMovementSpeed();
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, smoothSpeed);
        _currentInputs = Vector2.SmoothDamp(_currentInputs, _rawInputs, ref _inputsRef, smoothInputs);

        if (IsGrounded)
            _movement = (transform.forward * _currentInputs.y + transform.right * _currentInputs.x) * _currentSpeed;

        _movement.y = _appliedGravity;
        _cc.Move(_movement * Time.deltaTime);
    }

    /// <summary>
    /// Rotate the player and its camera
    /// </summary>
    private void HandleView()
    {
        float mouseX = _lookInputs.x * mouseSensitivity * Time.deltaTime;
        float mouseY = _lookInputs.y * mouseSensitivity * Time.deltaTime;

        //Vertical
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// Return the corresponding movement speed based on pressed inputs
    /// </summary>
    private float GetMovementSpeed()
    {
        if (_cc.isGrounded)
        {
            if (_rawInputs.magnitude >= 0.5f && _inputs.currentActionMap.FindAction("Run").IsPressed())
                return runSpeed;
            else if (_rawInputs.magnitude >= 0.01f)
                return walkSpeed;

            Debug.Log("Grounded");
        }

        Debug.Log("Not grounded");
        return 0f;
    }
    #endregion

    #region Physics Methods
    /// <summary>
    /// Handle gravity value applied on player
    /// </summary>
    private void HandleGravity()
    {
        //In air gravity
        if (!_cc.isGrounded)
        {
            if (_appliedGravity > -maxHorizontalVel)
                _appliedGravity -= gravity * Time.deltaTime;

            return;
        }

        _appliedGravity = -gravity * 0.5f;
    }
    #endregion
}