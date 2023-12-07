using System;
using UnityEngine;
using UnityEngine.Events;

public class SimpleInteractionObject : MonoBehaviour, IInteractable
{
    private enum InteractionType { Single, Multiple };

    [SerializeField] private string interactText = "Interact with object.";
    [SerializeField] private InteractionType interactType = InteractionType.Multiple;
    [SerializeField] private UnityEvent interactEvent;

    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        Debug.Log("Interact");

        interactEvent?.Invoke();
        IsInteractable = Convert.ToBoolean((int)interactType);
    }

    public string GetInteractText() => interactText;
}
