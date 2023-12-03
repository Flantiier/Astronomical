using System;
using UnityEngine;

public class PlanetContainer : ItemReceptacle, IInteractable
{
    [SerializeField] private ItemSO requiredItem;

    private CapsuleCollider _collider;

    public static event Action OnPlanetPlaced;
    public static int PlanetAmount = 0;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        IPickable playerObject = Player.Instance.Interactor.GetPickableItem();

        //If player doesn't hold an object, interaction is possible only if there is a planet on this receptacle
        if (playerObject == null)
        {
            bool hasPlanet = _currentItem != null && GetPlanet();
            IsInteractable = hasPlanet;
            return;
        }

        //Player can interact if he's carrying a planet
        IsInteractable = playerObject.GetGameObject().TryGetComponent(out Planet planet);
    }

    public override void Interact(PlayerInteract interactor)
    {
        bool playerHasObject = interactor.GetPickableItem() != null;
        bool vesselHasObject = _currentItem != null;

        //Case 1 : Player has object and vessel don't
        if (playerHasObject && !vesselHasObject)
        {
            PlaceObjectInReceptacle(interactor);
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
            IPickable lastItem = _currentItem;
            lastItem.IsInteractable = true;

            PlaceObjectInReceptacle(interactor);
            interactor.PickUpObject(lastItem);
        }

        OnPlanetPlaced?.Invoke();
    }

    public override string GetInteractText()
    {
        bool playerHasObject = Player.Instance.Interactor.GetPickableItem() != null;
        bool vesselHasObject = _currentItem != null;

        if (playerHasObject && !vesselHasObject)
        {
            return "Drop planet.";
        }
        else if (!playerHasObject && vesselHasObject)
        {
            return "Pick up planet";
        }

        return "Switch planets.";
    }

    protected override void PickUpFromVessel(PlayerInteract interactor)
    {
        base.PickUpFromVessel(interactor);
        PlanetAmount--;

        //Reset collider size and position
        ResetColliderValues();
    }

    protected override void PlaceObjectInReceptacle(PlayerInteract interactor)
    {
        base.PlaceObjectInReceptacle(interactor);

        //Set collider and planet position based on mesh dimensions
        float meshHeight = GetPlanet().GetMeshHeight();
        SetColliderValues(meshHeight);
        _currentItem.GetTransform().localPosition = new Vector3(0f, meshHeight / 2f, 0f);
    }


    private Planet GetPlanet()
    {
        return _currentItem.GetGameObject().GetComponent<Planet>();
    }

    public bool HasRequiredPlanet() => requiredItem == GetPlanet().GetPlanetDatas();


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
