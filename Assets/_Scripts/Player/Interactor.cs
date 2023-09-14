using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform playerHand;
    [SerializeField] private float interactRange = 1f;
    [SerializeField] private LayerMask interactable;
    private Transform _cam;

    private bool _canInteract = false;
    private RaycastHit _hitInfo;
    private IPickable _carriedItem;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>().transform;
    }

    private void FixedUpdate()
    {
        _canInteract = ShootingRaycast();
        Debug.Log(_canInteract);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Handle player interactions with other objects
    /// </summary>
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!_canInteract)
            return;

        GameObject hitObj = _hitInfo.collider.gameObject;

        //Interact with pickable items
        if (hitObj.TryGetComponent(out IPickable pickableItem))
            PickUpItem(pickableItem);
        //Interact with other objects
        else if(hitObj.TryGetComponent(out IInteractible interactibleItem))
            interactibleItem.Interact();
    }

    /// <summary>
    /// Shoot a raycast to interact with other elements
    /// </summary>
    private bool ShootingRaycast()
    {
        Ray ray = new Ray(_cam.position, _cam.forward * interactRange);
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);
        return Physics.Raycast(ray, out _hitInfo, interactRange, interactable, QueryTriggerInteraction.Collide);
    }

    /// <summary>
    /// Drop the carried item to pick up another one
    /// </summary>
    private void PickUpItem(IPickable item)
    {
        if (_carriedItem != null)
            _carriedItem.Drop(item.Transform);

        _carriedItem = item;
        item.PickUp(playerHand);
    }
    #endregion
}
