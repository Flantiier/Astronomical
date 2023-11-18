using UnityEngine;

public class Planet : PickableObject, IPickable
{
    [SerializeField] private PlanetSO planetSO;

    public PlanetSO GetPlanetInfos()
    {
        return planetSO;
    }

    public float GetMeshHeight()
    {
        return transform.GetChild(0).transform.localScale.y;
    }
}