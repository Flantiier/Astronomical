using UnityEngine;

public interface IInteractible
{
    public bool IsInteractable { get; set; }
    public void Interact();
}
