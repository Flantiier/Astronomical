using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Utils;
using System.Collections;

public class InteractText : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI feedbackText;
    #endregion

    #region Builts_In
    private void Awake()
    {
        ShowInteractaText(null);
    }

    private void OnEnable()
    {
        Interactor.OnInteractableItemChanged += ShowInteractaText;
    }

    private void OnDisable()
    {
        Interactor.OnInteractableItemChanged -= ShowInteractaText;
    }
    #endregion

    #region Methods
    private void ShowInteractaText(InteractableObject item)
    {
        background.enabled = item;
        Utilities.Inputs.GetCurrentInputForAction(playerInput, "Interact", out string input);
        feedbackText.text = item ? $"Press <b>{input}</b> to <b>{item.Message}</b>" : "";
    }
    #endregion
}
