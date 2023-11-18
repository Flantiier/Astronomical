using UnityEngine;
using UnityEngine.Events;

public class CircuitBreaker : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent powerOnEvent;
    private Animator _animator;

    public bool IsInteractable { get; set; } = true;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Interact(PlayerInteract interactor)
    {
        if (!IsInteractable)
            return;

        Debug.Log("Interact with Circuit Breaker ");

        PlayPowerOnFeedbacks();
        RaisePowerOnEvent();

        IsInteractable = false;
    }

    /// <summary>
    /// Play animation, sfx and particules
    /// </summary>
    private void PlayPowerOnFeedbacks()
    {
        _animator.enabled = true;
        //Play SFX
        //Play particules
    }

    private void RaisePowerOnEvent()
    {
        powerOnEvent?.Invoke();
    }

    public void PowerEventTest()
    {
        Debug.Log("Power has been activated !");
    }

    public string GetInteractText()
    {
        return "Turn on the power.";
    }
}
