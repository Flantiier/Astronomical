using UnityEngine;

[ExecuteInEditMode]
public class TextureOverrider : MonoBehaviour
{
    [SerializeField] private Texture2D texture;

    private void Awake()
    {
        LoadTexture();
    }

    private void LoadTexture()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        if (!texture || !renderer || !renderer.sharedMaterial)
            return;

        Material instance = new Material(renderer.sharedMaterial);

        instance.mainTexture = texture;
        renderer.sharedMaterial = instance;
    }
}
