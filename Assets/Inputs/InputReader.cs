using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, GameInputs.IGameplayActions, GameInputs.IUIActions
{
    private GameInputs _gameInputs;

    private void OnEnable()
    {
        if(_gameInputs == null)
        {
            _gameInputs = new GameInputs();

            _gameInputs.Gameplay.SetCallbacks(this);
            _gameInputs.UI.SetCallbacks(this);

            EnableGameplay();
        }
    }

    public void EnableGameplay()
    {
        _gameInputs.Gameplay.Enable();
        _gameInputs.UI.Disable();
    }

    public void EnableUI()
    {
        _gameInputs.Gameplay.Disable();
        _gameInputs.UI.Enable();
    }



    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;

    public event Action RunEvent;
    public event Action RunCancelledEvent;

    public event Action InteractEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    public event Action EscapeEvent;

    //GAMEPLAY
    public void OnMove(InputAction.CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());

    public void OnLook(InputAction.CallbackContext context) => LookEvent?.Invoke(context.ReadValue<Vector2>());

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            RunEvent?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            RunCancelledEvent?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            EnableUI();
        }
    }

    //UI
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            EnableGameplay();
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            EscapeEvent?.Invoke();
        }
    }
}
