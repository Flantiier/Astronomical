using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    #region Variables
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
        feedbackText.text = item ? item.Message : "";
    }
    #endregion
}
