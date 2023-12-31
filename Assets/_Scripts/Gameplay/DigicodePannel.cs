using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DigicodePannel : MonoBehaviour
{
    [Header("Code properties")]
    [SerializeField] private DigicodeButton[] digicodeButtons;

    [Header("Code text properties")]
    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private DigicodeScreenText screenTextProperties;

    [Header("Events")]
    [SerializeField] private UnityEvent ValidCodeEntered;

    private string _validCode;
    private string _currentCode = "";

    private void Start()
    {
        _validCode = GameManager.Instance.GameCode;
        UpdateScreenText();
    }

    public void NewDigicodeInputPressed(DigicodeButton.ButtonType buttonType, int value)
    {
        switch (buttonType)
        {
            case DigicodeButton.ButtonType.Remove:
                RemoveButtonPressed();
                break;

            case DigicodeButton.ButtonType.Validate:
                ValidateButtonPressed();
                break;

            default:
                NumberButtonPressed(value);
                break;
        }

        UpdateScreenText();
    }

    /// <summary>
    /// Add a digit number into the current code
    /// </summary>
    /// <param name="value"> Digit value </param>
    private void NumberButtonPressed(int value)
    {
        if (IsCurrentCodeFull())
            return;

        //Insert the new button value
        int insertIndex = _currentCode.Length;
        string textValue = value.ToString();
        _currentCode = _currentCode.Insert(insertIndex, textValue);
    }

    /// <summary>
    /// Check if the current code is correct
    /// </summary>
    private void ValidateButtonPressed()
    {
        if (!IsCurrentCodeFull())
            return;

        bool hasValidCode = _currentCode == _validCode;

        if (hasValidCode)
        {
            Debug.Log("Valid code entered !");

            EnableDigicodeButtons(false);
            ValidCodeEntered?.Invoke();
        }
        else
        {
            Debug.Log("Wrong code entered");

            _currentCode = "";
        }
    }

    /// <summary>
    /// Remove the last digit number of the current code
    /// </summary>
    private void RemoveButtonPressed()
    {
        if (_currentCode.Length <= 0)
            return;

        int startIndex = _currentCode.Length - 1;
        _currentCode = _currentCode.Remove(startIndex);
    }

    private bool IsCurrentCodeFull()
    {
        return _currentCode.Length >= _validCode.Length;
    }


    /// <summary>
    /// Enable or disable all digicode buttons in the array
    /// </summary>
    /// <param name="enabled"> Enabled or disabled </param>
    private void EnableDigicodeButtons(bool enabled)
    {
        if (digicodeButtons.Length <= 0)
            return;

        foreach (DigicodeButton button in digicodeButtons)
        {
            button.EnableButtonInteractability(false);
        }
    }

    /// <summary>
    /// Filling buttons array with DigicodeButton components on children
    /// </summary>
    [ContextMenu("Get Digicode Buttons")]
    private void GetDigicodeButtons()
    {
        digicodeButtons = GetComponentsInChildren<DigicodeButton>();
    }


    private void UpdateScreenText()
    {
        string text = _currentCode;
        int masLength = _validCode.Length;
        screenText.text = screenTextProperties.GenerateScreenTextString(text, masLength);
    }
}


[System.Serializable]
public class DigicodeScreenText
{
    private enum EmptyCharacterType
    {
        Underscore,
        Dash,
        Asterisk
    }

    [SerializeField] private EmptyCharacterType emptyCharacterType = EmptyCharacterType.Underscore;

    public string GenerateScreenTextString(string text, int maxLength)
    {
        string screenText = text;
        char symbol = GetCharacterFromType();

        if (text.Length >= maxLength)
            return screenText;
        else
        {
            for (int i = text.Length; i < maxLength; i++)
            {
                screenText += symbol;
            }
        }

        return screenText;
    }

    private char GetCharacterFromType()
    {
        switch (emptyCharacterType)
        {
            case EmptyCharacterType.Asterisk:
                return '*';
            case EmptyCharacterType.Dash:
                return '-';
            default:
                return '_';
        }

    }
}
