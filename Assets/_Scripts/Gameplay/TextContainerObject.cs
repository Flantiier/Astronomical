using System;
using UnityEngine;

public class TextContainerObject : MonoBehaviour, IInteractable
{
    [SerializeField] private TextContainerSO content;
    public static event Action<TextContainerSO> SendTextRequest;

    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        SendTextRequest?.Invoke(content);
    }

    public string GetInteractText()
    {
        return "Read content.";
    }
}
