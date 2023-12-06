using UnityEngine;

public class ChestLockingMechanism : MonoBehaviour, IInteractable
{
    [SerializeField] private LockedChest chest;
    public int CurrentPosition { get; private set; } = 0;

    public bool IsInteractable { get; set; } = true;
    public string GetInteractText() => "Interact";

    public void Interact(PlayerInteract interactor)        
    {
        if (!chest)
            return;

        int index = CurrentPosition >= chest.Steps - 1 ? 0 : CurrentPosition + 1;
        RotateMechanism(index);

        //Check entered code validity
        chest.CheckCodeValidity();
    }

    /// <summary>
    /// Set the index and rotate the mesh
    /// </summary>
    /// <param name="index"> New mechanism index </param>
    public void RotateMechanism(int index)
    {
        CurrentPosition = index;
        float angle = Mathf.Repeat(index * chest.Padding, 359);
        transform.localEulerAngles = new Vector3(angle, 0f, 0f);
    }
}
