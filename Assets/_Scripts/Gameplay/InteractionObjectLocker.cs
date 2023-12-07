using UnityEngine;

public class InteractionObjectLocker : MonoBehaviour
{
    [SerializeField] private Padlock padlock;
    private IInteractable _interactableObject;

    private void Awake()
    {
        if(!TryGetComponent(out IInteractable interactable))
            return;

        //Turn off object interactability
        _interactableObject = interactable;
        _interactableObject.IsInteractable = false;
    }

    private void Start() => padlock.PadlockUnlocked += EnableObjectInteractivity;

    /// <summary>
    /// Turn on object interactability
    /// </summary>
    private void EnableObjectInteractivity()
    {
        if (_interactableObject == null)
            return;

        _interactableObject.IsInteractable = true;
    }
}