using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactible");
    }
}
