using UnityEngine;

public class BlackFlashlight : PickableItem
{
    [SerializeField] private RevealingShaderSource revealingScript;

    private void Start()
    {
        EnableRevealingScript(false);
    }

    public override void SetObjectParent(Transform parent)
    {
        base.SetObjectParent(parent);
        EnableRevealingScript(true);
    }

    public override void ResetObjectParent(Vector3 position)
    {
        base.ResetObjectParent(position);
        EnableRevealingScript(false);
    }

    /// <summary>
    /// Enable revealing script on this object
    /// </summary>
    private void EnableRevealingScript(bool enabled)
    {
        if (!revealingScript)
            return;

        revealingScript.enabled = enabled;
    }
}
