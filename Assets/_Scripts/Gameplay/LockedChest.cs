using UnityEngine;

public class LockedChest : MonoBehaviour
{
    [SerializeField] private ChestLockingMechanism[] mechanisms;
    [SerializeField] private string chestCode = "0,0,0,0";

    [SerializeField] private int mechanismSteps = 12;
    [SerializeField] private float mechanismPadding = 30f;

    public int Steps => mechanismSteps;
    public float Padding => mechanismPadding;

    private void Awake()
    {
        RandomizeMechanisms();
    }

    /// <summary>
    /// Generate a random code and set each mechanism's index in the array to a random value
    /// </summary>
    private void RandomizeMechanisms()
    {
        string randomCode = GenerateRandomCode();

        //Restart this function to generate another code
        if (randomCode == chestCode)
        {
            RandomizeMechanisms();
            return;
        }
        
        //Set index for each mechanism
        for (int i = 0; i < mechanisms.Length; i++)
        {
            int m_Index = randomCode.ToCharArray()[i];
            mechanisms[i].RotateMechanism(m_Index);
        }
    }

    /// <summary>
    /// Generate a random code which fits chest's mechanisms amount
    /// </summary>
    private string GenerateRandomCode()
    {
        string generatedCode = "";
        for (int i = 0; i < mechanisms.Length; i++)
        {
            Debug.Log("here");
            int randomIndex = Random.Range(0, Steps);
            generatedCode += randomIndex;
        }

        Debug.Log("Generated code is : " + generatedCode);
        return generatedCode;
    }

    /// <summary>
    /// Verify if the code is correct or wrong
    /// </summary>
    public void CheckCodeValidity()
    {
        bool correctCode = VerifyCurrentCode();

        //Wrong code
        if (!correctCode)
        {
            Debug.Log("Wrong code entered");
            return;
        }

        //Correct code
        Debug.Log("Correct code entered");
    }

    /// <summary>
    /// Return a boolean which represents current code validity
    /// </summary>
    private bool VerifyCurrentCode()
    {
        string code = "";

        foreach (ChestLockingMechanism item in mechanisms)
            code += item.CurrentPosition.ToString();

        return code == chestCode;
    }
}
