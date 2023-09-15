using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform playerHand;
    [SerializeField] private float interactRange = 1f;
    [SerializeField] private LayerMask interactable;
    private Transform _cam;

    private RaycastHit _hitInfo;
    private InteractableObject _inRangeObject;
    public static event Action<InteractableObject> OnInteractableItemChanged;
    #endregion

    #region Properties
    public bool CanInteract { get; private set; }
    public IPickable CarriedItem { get; private set; }
    public InteractableObject InRangeObject
    {
        get => _inRangeObject;
        set
        {
            _inRangeObject = value;
            OnInteractableItemChanged.Invoke(_inRangeObject);
        }
    }
    #endregion

    #region Builts_In
    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>().transform;
    }

    private void FixedUpdate()
    {
        CanInteract = ShootingRaycast();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Handle player interactions with other objects
    /// </summary>
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!CanInteract)
            return;

        GameObject hitObj = _hitInfo.collider.gameObject;

        //Interact with pickable items
        if (hitObj.TryGetComponent(out IPickable pickableItem))
            PickUpItem(pickableItem);
        //Interact with other objects
        else if (hitObj.TryGetComponent(out IInteractible interactibleItem) && interactibleItem.IsInteractable)
            interactibleItem.Interact();
    }

    /// <summary>
    /// Shoot a raycast to interact with other elements
    /// </summary>
    private bool ShootingRaycast()
    {
        Ray ray = new Ray(_cam.position, _cam.forward * interactRange);
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);
        bool raycastHit = Physics.Raycast(ray, out _hitInfo, interactRange, interactable, QueryTriggerInteraction.Collide);

        InteractableObject hitObj = raycastHit ? _hitInfo.collider.gameObject.GetComponent<InteractableObject>() : null;
        InRangeObject = hitObj && hitObj.IsInteractable ? hitObj : null;

        return raycastHit;
    }

    /// <summary>
    /// Drop the carried item to pick up another one
    /// </summary>
    private void PickUpItem(IPickable item)
    {
        if (CarriedItem != null)
            CarriedItem.Drop(item.Transform);

        CarriedItem = item;
        item.PickUp(playerHand);
    }
    #endregion
}
