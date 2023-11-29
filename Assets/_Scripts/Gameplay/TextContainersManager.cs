using UnityEngine;

public class TextContainersManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private TextContainersGUI GUI;

    private void Start()
    {
        TextContainerObject.SendTextRequest += ShowContentFromRequest;
    }

    /// <summary>
    /// Show content and enable dialogues inputs
    /// </summary>
    private void ShowContentFromRequest(TextContainerSO content)
    {
        GUI.ShowContent(content);
        EnableDialogueInputs();
    }

    /// <summary>
    /// Hide content and disable dialogues inputs
    /// </summary>
    private void DisableContent()
    {
        GUI.HideContent();
        DisableDialogueInputs();
    }

    /// <summary>
    /// Enable inputs and subscribe listeners to events
    /// </summary>
    private void EnableDialogueInputs()
    {
        inputReader.EnableDialogue();
        inputReader.ExitDialogue += DisableContent;
        inputReader.NextDialogue += GUI.GetNextSentence;
        inputReader.PreviousDialogue += GUI.GetPreviousSentence;
    }

    /// <summary>
    /// Disable inputs and unsubscribe listeners to events
    /// </summary>
    private void DisableDialogueInputs()
    {
        inputReader.EnableGameplay();
        inputReader.ExitDialogue -= DisableContent;
        inputReader.NextDialogue -= GUI.GetNextSentence;
        inputReader.PreviousDialogue -= GUI.GetPreviousSentence;
    }
}