using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void FixedUpdate()
    {
        if (!Player.Instance || !Player.Instance.Interactor)
            return;

        if (Player.Instance.Interactor.GetInteractableObject(out IInteractable interactable) != null)
            Show(interactable);
        else
            Hide();
    }

    private void Show(IInteractable interactable)
    {
        container.SetActive(true);
        textMesh.text = interactable.GetInteractText();
    }

    private void Hide()
    {
        container.SetActive(false);
    }
}
