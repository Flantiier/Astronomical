using System;
using UnityEngine;

public class TextContainerObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] content;
    public static event Action<string[]> ShowTextRequest;

    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        Debug.Log("Start reading content");
        ShowTextRequest?.Invoke(content);
    }

    public string GetInteractText()
    {
        return "Read content.";
    }
}
