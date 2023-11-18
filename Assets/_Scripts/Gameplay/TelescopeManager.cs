using UnityEngine;
using UnityEngine.Events;

public class TelescopeManager : MonoBehaviour, IInteractable
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private TelescopeController telescopeController;

    [SerializeField] private UnityEvent onTelescopeControlled;
    [SerializeField] private UnityEvent onTelescopeExit;

    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        ControlTelescope();
    }

    public string GetInteractText()
    {
        return "Take control of the telescope";
    }

    /// <summary>
    /// Switch control from character controller and telescope controller
    /// </summary>
    private void EnableTelescopeController(bool control)
    {
        Player.Instance.Controller.EnablePlayerInputs(!control);
        telescopeController.enabled = control;
        IsInteractable = !control;
    }

    private void ControlTelescope()
    {
        EnableTelescopeController(true);
        onTelescopeControlled?.Invoke();

        Invoke("SubscribeExitEvent", 0.5f);
    }

    private void ExitTelescope()
    {
        EnableTelescopeController(false);
        onTelescopeExit?.Invoke();

        UnsubscribeExitEvent();
    }

    private void SubscribeExitEvent()
    {
        inputReader.EscapeEvent += ExitTelescope;
    }

    private void UnsubscribeExitEvent()
    {
        inputReader.EscapeEvent -= ExitTelescope;
    }
}