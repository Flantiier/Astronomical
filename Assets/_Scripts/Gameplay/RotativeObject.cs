using UnityEngine;

public class RotativeObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector2[] rotations;
    private int _index = 0;

    public bool IsInteractable { get; set; } = true;

    private void Start()
    {
        SetObjectRotation();
    }

    /// <summary>
    /// Set rotation of this object
    /// </summary>
    private void SetObjectRotation()
    {
        Vector2 eulers = rotations[_index];
        transform.rotation = Quaternion.Euler(eulers.x, eulers.y, 0f);
    }

    /// <summary>
    /// Increase index and rotate object
    /// </summary>
    [ContextMenu("Rotate")]
    private void RotateObject()
    {
        _index = _index >= rotations.Length - 1 ? 0 : _index;
        SetObjectRotation();
    }

    public void Interact(PlayerInteract interactor)
    {
        RotateObject();
    }

    public string GetInteractText()
    {
        return "Rotate.";
    }
}
