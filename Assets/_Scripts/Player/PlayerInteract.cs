using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactMask;

    [SerializeField] private Transform handTransform;
    private IPickable _carriedObject;

    private void OnEnable()
    {
        inputReader.InteractEvent += Interact;
    }

    private void OnDisable()
    {
        inputReader.InteractEvent -= Interact;
    }

    private void Interact()
    {
        if (GetInteractableObject(out IInteractable interactable) == null)
            return;

        interactable.Interact(this);
    }

    public IInteractable GetInteractableObject(out IInteractable interactable)
    {
        Transform camTransform = Camera.main.transform;
        Vector3 origin = camTransform.position;
        Vector3 direction = camTransform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, interactRange, interactMask))
        {
            if (hitInfo.collider.TryGetComponent(out interactable) && interactable.IsInteractable)
                return interactable;
            else
                return null;
        }

        return interactable = null;
    }

    public void PickUpObject(IPickable obj)
    {
        //Already carrying an object
        if (_carriedObject != null)
            DropObjectWorld(obj.GetObjectTransform().position);

        _carriedObject = obj;
        obj.SetObjectParent(handTransform);
    }

    /// <summary>
    /// Drop the object and set his parent to world
    /// </summary>
    public void DropObjectWorld(Vector3 position)
    {
        _carriedObject.ResetObjectParent(position);
        _carriedObject = null;
    }

    /// <summary>
    /// Drop the object and set his parent to a new parent
    /// </summary>
    public void DropObjectParent(Transform newParent)
    {
        _carriedObject.SetObjectParent(newParent);
        _carriedObject = null;
    }

    /// <summary>
    /// Get the current carried object on player
    /// </summary>
    public IPickable GetPickableItem()
    {
        return _carriedObject;
    }
}