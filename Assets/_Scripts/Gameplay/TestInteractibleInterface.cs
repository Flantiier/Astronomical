using UnityEngine;

public class TestInteractibleInterface : InteractibleObject, IInteractible
{
    public void Interact() => Debug.Log("Interacting with object!");
}