using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractible
{
    [SerializeField] protected string interactMessage = $"Interact with this object";

    public string Message => interactMessage;
    public bool IsInteractable { get; set; } = true;

    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactible");
    }

    public virtual void Interact() => Debug.Log("Interact with object");
    protected void CanInteractWith(bool interactable) => IsInteractable = interactable;
}
