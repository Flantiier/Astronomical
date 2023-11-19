using UnityEngine;

public class LockedCloset : SimpleInteractionObject
{
    [SerializeField] private Padlock padlock;

    private void Awake() => IsInteractable = false;

    private void OnEnable() => padlock.PadlockUnlocked += UnlockShelf;

    private void OnDisable() => padlock.PadlockUnlocked -= UnlockShelf;

    private void UnlockShelf()
    {
        IsInteractable = true;
        PlayInteractAnimation();
    }
}