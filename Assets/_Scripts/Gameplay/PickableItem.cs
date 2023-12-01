using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [SerializeField] private ItemSO itemDatas;

    public bool IsInteractable { get; set; } = true;
    public ItemSO Item => itemDatas;

    public void Interact(PlayerInteract interactor)
    {
        interactor.PickUpObject(this);
    }

    public virtual string GetInteractText() => "Pick up item.";

    public GameObject GetGameObject() => gameObject;
    public Transform GetTransform() => transform;

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