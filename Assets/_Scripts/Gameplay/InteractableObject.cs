using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected string interactMessage = $"Interact with this object";
    public string Message => interactMessage;

    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactible");
    }
}
