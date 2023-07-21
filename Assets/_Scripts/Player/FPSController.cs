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
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 5f;

    private Vector2 _lookInputs;
    private float _xRotation = 0f;
    private Vector2 _rawInputs;
    private Vector2 _currentInputs;
    private Vector2 _inputsRef;
    private Vector3 _movement;
    #endregion

    #region Physics
    [Header("Physics")]
    [SerializeField] private float gravity = 3f;
    #endregion

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
        HandleMovement();
        HandleView();
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

    #region Methods
    /// <summary>
    /// Move the player around
    /// </summary>
    private void HandleMovement()
    {
        _currentInputs = Vector2.SmoothDamp(_currentInputs, _rawInputs, ref _inputsRef, smoothInputs);

        if (!_cc.isGrounded)
            _movement.y -= gravity * Time.deltaTime;
        else
        {
            _movement = transform.forward * _currentInputs.y + transform.right * _currentInputs.x;
            _movement.y = gravity * 0.05f * Time.deltaTime;

        }

        _cc.Move(_movement * walkSpeed * Time.deltaTime);
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
    #endregion
}
