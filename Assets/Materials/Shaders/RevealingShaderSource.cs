using UnityEngine;

[ExecuteInEditMode]
public class RevealingShaderSource : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Light spotlight;

    private void Update()
    {
        if (!spotlight || !material)
            return;

        material.SetVector("_LPosition", spotlight.transform.position);
        material.SetVector("_LDirection", -spotlight.transform.forward);
        material.SetFloat("_LAngle", spotlight.spotAngle);
        material.SetFloat("_LRange", spotlight.range);
    }
}
