using TMPro;
using UnityEngine;

public class TextContainersGUI : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI textMesh;

    private string[] _textContent;
    private int _currentIndex = 0;

    private void Start()
    {
        TextContainerObject.ShowTextRequest += ShowContent;
    }

    /// <summary>
    /// Initialize text content and enable GUI
    /// </summary
    private void ShowContent(string[] content)
    {
        _textContent = content;
        _currentIndex = 0;

        string text = GetSentenceFromContent();
        SetText(text);
        EnableContentPanel();
    }

    /// <summary>
    /// Get the string in the sentence array at current index
    /// </summary>
    private string GetSentenceFromContent()
    {
        int index = Mathf.Clamp(_currentIndex, 0, _textContent.Length - 1);
        return _textContent[index];
    }

    /// <summary>
    /// Set the text in the TextMesh component
    /// </summary>
    /// <param name="text"> Text content </param>
    private void SetText(string text)
    {
        textMesh.text = text;
    }

    /// <summary>
    /// Enable the content panel
    /// </summary>
    private void EnableContentPanel()
    {
        content.SetActive(true);
    }

    /// <summary>
    /// Disable the content panel
    /// </summary>
    private void DisableContentPanel()
    {
        content.SetActive(false);
    }
}