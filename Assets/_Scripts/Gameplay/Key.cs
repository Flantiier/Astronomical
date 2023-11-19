using UnityEngine;

public class Key : PickableObject
{
    public enum KeyType { Silver, Gold }

    [SerializeField] private KeyType type;
    public KeyType GetKeyType() => type;
}