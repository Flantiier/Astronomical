using UnityEngine;

public class EmptyBook : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        Debug.Log("Interact with empty book");

        //Animation Close/Open
    }

    public string GetInteractText()
    {
        return "Open/Close the book.";
    }
}
