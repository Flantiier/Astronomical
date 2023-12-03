using UnityEngine;
using UnityEngine.Events;

public class LaserPadlock : MonoBehaviour
{
    [SerializeField] private UnityEvent padlockUnlocked;

    public void UnlockPadlock() => padlockUnlocked?.Invoke();
    public void UnlockTest() => Debug.Log($"Laser padlock unlocked !");
}