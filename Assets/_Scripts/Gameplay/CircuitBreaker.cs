using UnityEngine;

public class CircuitBreaker : InteractableObject
{
    #region Variables
    [SerializeField] private GameEvent circuitStartedEvent;
	private Animator _animator;
    #endregion

    #region Builts_In
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }
    #endregion

    #region Methods
    public override void Interact()
    {
        _animator.enabled = true;
        CanInteractWith(false);
    }

    public void CircuitStarted()
    {
        //Raise an event
        circuitStartedEvent.Raise();
        Debug.Log("Starting circuit breaker, electricity should be on!");
    }
    #endregion
}