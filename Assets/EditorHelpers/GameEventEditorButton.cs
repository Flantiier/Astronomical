using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventEditorButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Color disabledTextColor = Color.grey;
    public Button Button => button;

    private void Start()
    {
        button.onClick.AddListener(DisableButtonOnClick);
    }

    /// <summary>
    /// Set the text of the button
    /// </summary>
    public void SetButtonTitle(string title)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = title;
    }

    /// <summary>
    /// Disable interactability on button
    /// </summary>
    private void DisableButtonOnClick()
    {
        button.interactable = false;
        TextMeshProUGUI meshPro = GetComponentInChildren<TextMeshProUGUI>();
        meshPro.color = disabledTextColor;
    }
}
