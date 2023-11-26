using UnityEngine;

public class TextContainersGUI : MonoBehaviour
{
    [SerializeField] private GameObject textGUI;

    private void Start()
    {
        TextContainerObject.ShowTextRequest += ShowContent;
    }

    private void ShowContent(string[] content)
    {
        foreach (string sentence in content) 
        {
            Debug.Log(sentence);
        }
    }
}