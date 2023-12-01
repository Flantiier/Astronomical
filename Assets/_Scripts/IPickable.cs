using UnityEngine;

public interface IPickable : IInteractable
{
    public void SetObjectParent(Transform parent);

    public void ResetObjectParent(Vector3 position);

    public T GetComponent<T>();

    public Transform GetTransform();
}