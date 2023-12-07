using System.Text;
using UnityEngine;

public class LockedChest : MonoBehaviour
{
    [Header("Chest properties")]
    [SerializeField] private int mechanismSteps = 12;
    [Tooltip("Always put a coma and a space between numbers.")]
    [SerializeField] private string validCode = "1, 2, 3, 4";
    [SerializeField] private ChestLockingMechanism[] mechanisms;

    [Header("Events")]
    [SerializeField] private GameEvent chestUnlockedEvent;

    public int Steps => mechanismSteps;
    public float Padding => 360f / Steps;

    private void Awake()
    {
        RandomizeMechanisms();
    }

    /// <summary>
    /// Generate a random code and set each mechanism's index in the array to a random value
    /// </summary>
    [ContextMenu("Randomize mechanisms")]
    private void RandomizeMechanisms()
    {
        string randomCode = GenerateRandomCode();

        //Restart this function to generate another code
        if (randomCode == validCode)
        {
            RandomizeMechanisms();
            return;
        }

        Debug.Log("Chest random code : " + randomCode);

        //Set index for each mechanism
        string[] splittedCode = randomCode.Split(", ");
        for (int i = 0; i < splittedCode.Length; i++)
        {
            int value = int.Parse(splittedCode[i]);
            mechanisms[i].RotateMechanism(value);
        }
    }

    /// <summary>
    /// Generate a random index for each mechanism sets in the array
    /// </summary>
    private string GenerateRandomCode()
    {
        int length = mechanisms.Length;
        StringBuilder randomCode = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(0, Steps);
            randomCode.Append(i < length - 1 ? $"{randomIndex}, " : randomIndex);
        }

        return randomCode.ToString();
    }

    /// <summary>
    /// Disable mechanisms scripts and call an event
    /// </summary>
    private void UnlockChest()
    {
        foreach (ChestLockingMechanism item in mechanisms)
            item.IsInteractable = false;

        chestUnlockedEvent.Raise();
    }

    /// <summary>
    /// Verify if the code is correct or wrong
    /// </summary>
    public void CheckCodeValidity()
    {
        //Wrong code
        if (!VerifyCurrentCode())
            return;

        //Correct code
        UnlockChest();
    }

    /// <summary>
    /// Compare mechanisms values and chest valid code
    /// </summary>
    private bool VerifyCurrentCode()
    {
        string[] splittedCode = validCode.Split(", ");

        for (int i = 0; i < splittedCode.Length; i++)
        {
            string mechanismValue = mechanisms[i].CurrentPosition.ToString();

            //Continue if the value is correct
            if (splittedCode[i] == mechanismValue)
                continue;
            //Else, we stop and return false
            else
                return false;
        }

        //Code is correct, return true
        return true;
    }
}
