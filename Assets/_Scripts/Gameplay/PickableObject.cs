using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    public bool IsInteractable { get; set; } = true;

    public void Interact(PlayerInteract interactor)
    {
        interactor.PickUpObject(this);
    }

    public virtual string GetInteractText() => "Pick up item.";

    public Transform GetObjectTransform() => transform;

    public virtual void SetObjectParent(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void ResetObjectParent(Vector3 position)
    {
        transform.parent = null;
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }
}