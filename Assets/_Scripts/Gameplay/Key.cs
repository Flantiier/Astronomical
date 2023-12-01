using UnityEngine;

public class Key : PickableItem
{
    public enum KeyType { Silver, Gold }

    [SerializeField] private KeyType type;
    public KeyType GetKeyType() => type;
}