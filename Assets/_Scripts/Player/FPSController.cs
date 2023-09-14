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
    private Interactor _interactor;

    #region Motion
    [Header("Motion & View")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField, Range(0f, 0.5f)] private float smoothInputs = 0.05f;
    [SerializeField, Range(0f, 0.5f)] private float smoothSpeed = 0.05f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [HideInInspector] private Vector2 _rawInputs;
    [HideInInspector] private Vector2 _inputsRef;
    [HideInInspector] private float _xRotation = 0f;
    private Vector2 _lookInputs;
    private Vector2 _currentInputs;
    private Vector3 _movement;
    private float _currentSpeed;
    #endregion

    #region Physics
    [Header("Physics")]
    [SerializeField] private float gravity = 3f;
    [SerializeField] private float maxHorizontalVel = 20f;
    [HideInInspector] private float _airTime = 1;
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
        _interactor = GetComponent<Interactor>();
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
        _inputs.currentActionMap.FindAction("Jump").started += OnJump;
        _inputs.currentActionMap.FindAction("Interact").started += _interactor.OnInteract;
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
        _inputs.currentActionMap.FindAction("Jump").started -= OnJump;
        _inputs.currentActionMap.FindAction("Interact").started -= _interactor.OnInteract;
    }

    private void OnMove(InputAction.CallbackContext ctx) => _rawInputs = ctx.ReadValue<Vector2>();
    private void OnLook(InputAction.CallbackContext ctx) => _lookInputs = ctx.ReadValue<Vector2>();
    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (!IsGrounded)
            return;

        _movement.y = jumpForce;
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

        //Get movement vector
        Vector3 direction = (transform.forward * _currentInputs.y + transform.right * _currentInputs.x) * _currentSpeed;
        Vector3 movement = new Vector3(direction.x, _movement.y, direction.z);
        _movement = Vector3.Lerp(_movement, movement, smoothInputs);

        //Apply gravity
        HandleGravity();

        _cc.Move(_movement * Time.deltaTime);
    }

    /// <summary>
    /// Handle applied gravity on player
    /// </summary>
    private void HandleGravity()
    {
        //Not grounded
        if (!IsGrounded)
        {
            _airTime += Time.deltaTime;
            _movement.y -= (gravity * _airTime) * Time.deltaTime;
            _movement.y = Mathf.Clamp(_movement.y, -maxHorizontalVel, maxHorizontalVel);
        }
        //Grounded
        else
        {
            if (_movement.y <= 0.01f)
                _movement.y = -gravity;

            //Reset air time
            if (_airTime > 1)
                _airTime = 1;
        }
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
        if (_rawInputs.magnitude >= 0.5f && _inputs.currentActionMap.FindAction("Run").IsPressed())
            return runSpeed;
        else if (_rawInputs.magnitude >= 0.01f)
            return walkSpeed;

        return 0f;
    }
    #endregion
}