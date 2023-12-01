using UnityEngine;

public class Planet : PickableItem, IPickable
{
    public ItemSO GetPlanetDatas() => Item;

    public float GetMeshHeight()
    {
        return transform.GetChild(0).transform.localScale.y;
    }
}