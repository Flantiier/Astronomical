using UnityEngine;

public class DigicodeButton : MonoBehaviour, IInteractable
{
    public enum ButtonType
    {
        Number,
        Remove,
        Validate
    }

    [SerializeField] private DigicodePannel digicodePannel;
    [SerializeField] private ButtonType buttonType = ButtonType.Number;
    [SerializeField, Range(0, 9)] private int buttonValue = 0;

    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        if (!IsInteractable)
            return;

        digicodePannel.NewDigicodeInputPressed(buttonType, buttonValue);
    }

    /// <summary>
    /// Modify button interactability state
    /// </summary>
    public void EnableButtonInteractability(bool interactable)
    {
        IsInteractable = interactable;
    }

    public string GetInteractText()
    {
        return "Press the button.";
    }
}