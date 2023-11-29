using UnityEngine;

[CreateAssetMenu(fileName = "new TextContainer SO")]
public class TextContainerSO : ScriptableObject
{
    public string title = "Title";
    [TextArea(5, 5)] public string[] content = new string[1];
}
