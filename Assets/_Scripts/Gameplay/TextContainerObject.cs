using System;
using UnityEngine;

public class TextContainerObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] content;
    public static event Action<string[]> SendTextRequest;

    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        Debug.Log("Start reading content");
        SendTextRequest?.Invoke(content);
    }

    public string GetInteractText()
    {
        return "Read content.";
    }
}
