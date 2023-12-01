using System;
using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    [SerializeField] private Key key;
    [SerializeField] private Transform keyPoint;

    private Animator _animator;
    private Rigidbody _rb;

    public event Action PadlockUnlocked;

    public bool IsInteractable { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _rb = GetComponent<Rigidbody>();
        EnablePhysic(false);
    }

    private void FixedUpdate()
    {
        IPickable pickable = Player.Instance.Interactor.GetPickableItem();

        //No obj holded
        if (pickable == null)
            IsInteractable = false;
        else
            IsInteractable = pickable.GetTransform().TryGetComponent(out Key m_key) && m_key == key;
    }

    public void Interact(PlayerInteract interactor)
    {
        //Drop key inside padlock
        interactor.DropObjectParent(keyPoint);
        EnablePhysic(true);
        _animator.enabled = true;

        //Send an event to indicate that the padlock is unlocked
        PadlockUnlocked?.Invoke();

        //Disable key and padlock scripts
        key.IsInteractable = false;
        this.IsInteractable = false;
        enabled = false;
    }

    public string GetInteractText()
    {
        return "Use key to open.";
    }

    private void EnablePhysic(bool enablePhysic)
    {
        _rb.useGravity = enablePhysic;
        _rb.isKinematic = !enablePhysic;
    }
}
