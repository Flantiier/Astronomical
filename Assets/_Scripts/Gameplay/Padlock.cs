using System;
using UnityEngine;

public class Padlock : ItemReceptacle, IInteractable
{
    [SerializeField] private ItemSO requiredKey;

    private Animator _animator;
    private Rigidbody _rb;

    public event Action PadlockUnlocked;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _rb = GetComponent<Rigidbody>();
        EnablePhysic(false);
    }

    private void FixedUpdate()
    {
        IPickable playerObject = Player.Instance.Interactor.GetPickableItem();

        //No obj holded
        if (playerObject == null)
            IsInteractable = false;
        else
            IsInteractable = HasRequiredKey(playerObject);
    }

    public override void Interact(PlayerInteract interactor)
    {
        //Drop key inside padlock
        PlaceObjectInReceptacle(interactor);
        EnablePhysic(true);
        _animator.enabled = true;

        //Send an event to indicate that the padlock is unlocked
        PadlockUnlocked?.Invoke();

        //Disable key and padlock scripts
        _currentItem.IsInteractable = false;
        this.IsInteractable = false;
        enabled = false;
    }

    public override string GetInteractText()
    {
        return "Open padlock.";
    }
    
    /// <summary>
    /// Indicate if the player is holdinhg the required object 
    /// </summary>
    /// <param name="playerObject"> Object holded by player </param>
    private bool HasRequiredKey(IPickable playerObject)
    {
        return playerObject.GetGameObject().GetComponent<PickableItem>().Item == requiredKey;
    }

    /// <summary>
    /// Enable rb physics on this object
    /// </summary>
    /// <param name="enablePhysic"> Should use gravity </param>
    private void EnablePhysic(bool enablePhysic)
    {
        _rb.useGravity = enablePhysic;
        _rb.isKinematic = !enablePhysic;
    }
}
