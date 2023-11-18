using System;
using UnityEngine;

public class PlanetContainer : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform dropPoint;
    private CapsuleCollider _collider;

    [SerializeField] private PlanetSO requiredPlanet;
    private Planet _planet;

    public bool IsInteractable { get; set; }

    public static event Action OnPlanetPlaced;
    public static int PlanetAmount = 0;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        IPickable pickable = Player.Instance.Interactor.GetPickableItem();
        bool hasPlanet = _planet;

        if (pickable == null && !hasPlanet)
            IsInteractable = false;
        else
            IsInteractable = hasPlanet || pickable.GetObjectTransform().TryGetComponent(out Planet planet);
    }

    public void Interact(PlayerInteract interactor)
    {
        bool playerHasObject = interactor.GetPickableItem() != null;
        bool vesselHasObject = _planet != null;

        //Case 1 : Player has object and vessel don't
        if (playerHasObject && !vesselHasObject)
        {
            PlaceObjectInVessel(interactor);
            PlanetAmount++;
        }
        //Case 2 : Vessel has object and player don't
        else if (!playerHasObject && vesselHasObject)
        {
            PickUpFromVessel(interactor);
        }
        //Case 3 : Both have an object
        else
        {
            IPickable lastItem = _planet;
            lastItem.IsInteractable = true;

            PlaceObjectInVessel(interactor);
            interactor.PickUpObject(lastItem);
        }

        OnPlanetPlaced?.Invoke();
    }

    public string GetInteractText()
    {
        bool playerHasObject = Player.Instance.Interactor.GetPickableItem() != null;
        bool vesselHasObject = _planet != null;

        if (playerHasObject && !vesselHasObject)
        {
            return "Deposit a planet in vessel.";
        }
        else if (!playerHasObject && vesselHasObject)
        {
            return "Pick up planet from vessel";
        }

        return "Change current planet.";
    }

    private void PickUpFromVessel(PlayerInteract interactor)
    {
        _planet.IsInteractable = true;
        interactor.PickUpObject(_planet);

        PlanetAmount--;
        _planet = null;

        //Reset collider size and position
        ResetColliderValues();
    }

    private void PlaceObjectInVessel(PlayerInteract interactor)
    {
        //Get obj from player and place obj in this vessel
        _planet = (Planet)interactor.GetPickableItem();
        interactor.DropObjectParent(dropPoint);
        _planet.IsInteractable = false;

        //Set collider and planet position based on mesh dimensions
        float meshHeight = _planet.GetMeshHeight();
        SetColliderValues(meshHeight);
        _planet.transform.localPosition = new Vector3(0f, meshHeight / 2f, 0f);
    }

    public bool HasRequiredPlanet()
    {
        return requiredPlanet == _planet.GetPlanetInfos();
    }

    private void SetColliderValues(float meshHeight)
    {
        float halfHeight = meshHeight / 2f;

        float radius = Mathf.Clamp(halfHeight * 1.2f, 0.1f, 10f);
        float height = meshHeight * 1.6f;
        Vector3 center = new Vector3(0f, height * 0.225f, 0f);

        _collider.height = height;
        _collider.center = center;
        _collider.radius = radius;
    }

    private void ResetColliderValues()
    {
        _collider.center = Vector3.zero;
        _collider.radius = 0.1f;
        _collider.height = 0.1f;
    }
}
