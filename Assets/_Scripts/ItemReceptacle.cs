using UnityEngine;

public class ItemReceptacle : MonoBehaviour, IInteractable
{
    [SerializeField] protected Transform dropPoint;
    protected IPickable _currentItem;

    public bool IsInteractable { get; set; } = true;

    public virtual void Interact(PlayerInteract interactor)
    {
        bool playerHasObject = interactor.GetPickableItem() != null;
        bool vesselHasObject = _currentItem != null;

        //Case 1 : Player has object and vessel don't
        if (playerHasObject && !vesselHasObject)
        {
            PlaceObjectInVessel(interactor);
            //PlanetAmount++;
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

            PlaceObjectInVessel(interactor);
            interactor.PickUpObject(lastItem);
        }

        //OnPlanetPlaced?.Invoke();
    }

    public virtual string GetInteractText()
    {
        bool playerHasObject = Player.Instance.Interactor.GetPickableItem() != null;
        bool vesselHasObject = _currentItem != null;

        if (playerHasObject && !vesselHasObject)
        {
            return "Drop item.";
        }
        else if (!playerHasObject && vesselHasObject)
        {
            return "Pick up item.";
        }

        return "Switch items.";
    }

    protected virtual void PickUpFromVessel(PlayerInteract interactor)
    {
        _currentItem.IsInteractable = true;
        interactor.PickUpObject(_currentItem);

        _currentItem = null;
    }

    protected virtual void PlaceObjectInVessel(PlayerInteract interactor)
    {
        //Get obj from player and place obj in this vessel
        _currentItem = interactor.GetPickableItem();
        interactor.DropObjectParent(dropPoint);
        _currentItem.IsInteractable = false;
    }
}