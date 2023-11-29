using UnityEngine;

[CreateAssetMenu(fileName = "new TextContainer SO")]
public class TextContainerSO : ScriptableObject
{
    public string title = "Title";
    [TextArea(18, 18)] public string[] content = new string[1];
}
