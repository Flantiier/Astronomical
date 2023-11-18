using UnityEngine;

public class TextureOverrider : MonoBehaviour
{
    [SerializeField] private Texture2D texture;

    private void Awake()
    {
        LoadTexture();
    }

    private void LoadTexture()
    {
        if (!texture)
            return;

        Renderer renderer = GetComponent<Renderer>();
        Material instance = new Material(renderer.sharedMaterial);

        instance.mainTexture = texture;
        renderer.sharedMaterial = instance;
    }
}
