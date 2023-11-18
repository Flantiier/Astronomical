using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class FPSController : MonoBehaviour
{
    #region Variables
    [SerializeField] private InputReader inputReader;

    [Header("Motion & View")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField, Range(0f, 0.5f)] private float smoothInputs = 0.05f;
    [SerializeField, Range(0f, 0.5f)] private float smoothSpeed = 0.05f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 5f;

    [Header("Physics")]
    [SerializeField] private float gravity = 3f;

    private Transform _camera;
    private CharacterController _cc;

    private Vector2 _rawInputs;
    private Vector2 _lookInputs;
    private bool _isRunning;

    private Vector2 _inputsRef;
    private Vector2 _currentInputs;

    private float _xRotation = 0f;

    private Vector3 _movement;
    private float _currentSpeed;
    private float _airTime = 1;

    private bool _inputsDisabled = false;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
        inputReader.LookEvent += OnLook;

        inputReader.RunEvent += OnRun;
        inputReader.RunCancelledEvent += OnRunCancelled;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.LookEvent -= OnLook;

        inputReader.RunEvent -= OnRun;
        inputReader.RunCancelledEvent -= OnRunCancelled;
    }

    private void Update()
    {
        if (_inputsDisabled)
            return;

        HandlePlayerMovement();
        HandlePlayerFPSView();
    }
    #endregion

    #region Methods
    //INPUT LISTENERS
    private void OnMove(Vector2 inputs) => _rawInputs = inputs;
    private void OnLook(Vector2 inputs) => _lookInputs = inputs;
    private void OnRun() => _isRunning = true;
    private void OnRunCancelled() => _isRunning = false;


    /// <summary>
    /// Move the player around
    /// </summary>
    private void HandlePlayerMovement()
    {
        float targetSpeed = GetMovementSpeed();
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, smoothSpeed);
        _currentInputs = Vector2.SmoothDamp(_currentInputs, _rawInputs, ref _inputsRef, smoothInputs);

        //Get movement vector
        Vector3 direction = (transform.forward * _currentInputs.y + transform.right * _currentInputs.x) * _currentSpeed;
        _movement = new Vector3(Mathf.Lerp(_movement.x, direction.x, smoothInputs), _movement.y, Mathf.Lerp(_movement.z, direction.z, smoothInputs));

        //Apply gravity
        HandleGravity();

        //Move character
        _cc.Move(_movement * Time.deltaTime);
    }

    /// <summary>
    /// Handle applied gravity on player
    /// </summary>
    private void HandleGravity()
    {
        //Not grounded
        if (!IsGrounded())
        {
            _airTime += Time.deltaTime;
            _movement.y -= (gravity * _airTime) * Time.deltaTime;
        }
        //Grounded
        else
        {
            if (_movement.y <= 0.01f)
                _movement.y = -gravity * 0.1f;

            //Reset air time
            if (_airTime > 1)
                _airTime = 1;
        }
    }

    /// <summary>
    /// Rotate the player and its camera
    /// </summary>
    private void HandlePlayerFPSView()
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
        if (_rawInputs.magnitude >= 0.5f && _isRunning)
            return runSpeed;
        else if (_rawInputs.magnitude >= 0.01f)
            return walkSpeed;

        return 0f;
    }

    /// <summary>
    /// Return if the character controller is grounded or not
    /// </summary>
    private bool IsGrounded() => _cc.isGrounded;

    /// <summary>
    /// Reset character velocity and movement vector
    /// </summary>
    private void ResetCharacterVelocity()
    {
        _movement = Vector3.zero;
        _currentInputs = Vector3.zero;
        _currentSpeed = 0f;
    }

    /// <summary>
    /// Method to disable player movement
    /// </summary>
    public void EnablePlayerInputs(bool enabled)
    {
        _inputsDisabled = !enabled;

        if (!enabled)
            ResetCharacterVelocity();
    }
    #endregion
}