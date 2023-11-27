using TMPro;
using UnityEngine;

public class TextContainersGUI : MonoBehaviour
{
    [Header("GUI elements")]
    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject leftArrow, rightArrow;

    private string[] _textContent;
    private int _currentIndex = 0;

    /// <summary>
    /// Initialize text content and enable GUI
    /// </summary
    public void ShowContent(string[] content)
    {
        _textContent = content;
        _currentIndex = 0;

        UpdateTextContent();
        EnableContentPanel();
    }

    /// <summary>
    /// Hide GUI elements
    /// </summary>
    public void HideContent()
    {
        content.SetActive(false);
    }

    /// <summary>
    /// Set the text in the TextMesh component
    /// </summary>
    /// <param name="text"> Text content </param>
    private void UpdateTextContent()
    {
        textMesh.text = GetSentence();
        EnableNavigationArrows();
    }

    /// <summary>
    /// Get the string in the sentence array at current index
    /// </summary>
    private string GetSentence()
    {
        int index = Mathf.Clamp(_currentIndex, 0, _textContent.Length - 1);
        return _textContent[index];
    }

    /// <summary>
    /// Enable navigation arrows when the text content is shown
    /// </summary>
    private void EnableNavigationArrows()
    {
        if (!rightArrow || !leftArrow)
            return;

        int maxIndex = _textContent.Length;
        bool canNext = _currentIndex < maxIndex - 1;
        bool canPrevious = _currentIndex > 0 && _currentIndex < maxIndex;

        rightArrow.SetActive(canNext);
        leftArrow.SetActive(canPrevious);
    }

    /// <summary>
    /// Get the previous sentence in text content and update UI
    /// </summary>
    public void GetPreviousSentence()
    {
        if (_currentIndex <= 0)
            return;

        _currentIndex--;
        UpdateTextContent();
    }

    /// <summary>
    /// Get the next sentence in text content and update UI
    /// </summary>
    public void GetNextSentence()
    {
        if (_currentIndex >= _textContent.Length - 1)
            return;

        _currentIndex++;
        UpdateTextContent();
    }

    /// <summary>
    /// Enable the content panel
    /// </summary>
    private void EnableContentPanel()
    {
        content.SetActive(true);
    }
}