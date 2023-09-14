using UnityEngine;

public interface IPickable
{
    public Transform Transform { get; }

    public void PickUp(Transform parent);
    public void Drop(Transform transform);
}
