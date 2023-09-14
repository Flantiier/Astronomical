using UnityEngine;

public class TestInteractableInterface : InteractableObject, IInteractible
{
    public void Interact() => Debug.Log("Interacting with object!");
}