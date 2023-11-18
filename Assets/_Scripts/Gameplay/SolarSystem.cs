using UnityEngine;
using UnityEngine.Events;

public class SolarSystem : MonoBehaviour
{
    [SerializeField] private PlanetContainer[] containers;

    [SerializeField] private UnityEvent SolarSystemValid;

    private void OnEnable()
    {
        PlanetContainer.OnPlanetPlaced += CheckSolarSystemOrder;
    }

    private void OnDisable()
    {
        PlanetContainer.OnPlanetPlaced -= CheckSolarSystemOrder;
    }

    private void CheckSolarSystemOrder()
    {
        if (PlanetContainer.PlanetAmount < containers.Length)
        {
            Debug.Log("Missing planets...");
            return;
        }

        for (int i = 0; i < containers.Length; i++)
        {
            PlanetContainer targetVessel = containers[i];

            if (!targetVessel.HasRequiredPlanet())
            {
                Debug.Log("Solar System incorrect !");
                return;
            }
        }

        Debug.Log("Solar System correct !");
        DisableVessels();

        SolarSystemValid?.Invoke();
    }

    private void DisableVessels()
    {
        foreach (PlanetContainer vessel in containers)
        {
            vessel.enabled = false;
            vessel.IsInteractable = false;
        }
    }
}
