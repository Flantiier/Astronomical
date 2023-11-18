using UnityEngine;

public interface IInteractable
{
    public bool IsInteractable { get; set; }

    public void Interact(PlayerInteract interactor);

    public string GetInteractText();

}
