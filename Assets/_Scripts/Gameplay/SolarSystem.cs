using UnityEngine;
using UnityEngine.Events;

public class SolarSystem : MonoBehaviour
{
    [SerializeField] private PlanetReceptacle[] containers;

    [SerializeField] private UnityEvent SolarSystemValid;

    private void OnEnable()
    {
        PlanetReceptacle.OnPlanetPlaced += CheckSolarSystemOrder;
    }

    private void OnDisable()
    {
        PlanetReceptacle.OnPlanetPlaced -= CheckSolarSystemOrder;
    }

    private void CheckSolarSystemOrder()
    {
        if (PlanetReceptacle.PlanetAmount < containers.Length)
        {
            Debug.Log("Missing planets...");
            return;
        }

        for (int i = 0; i < containers.Length; i++)
        {
            PlanetReceptacle targetVessel = containers[i];

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
        foreach (PlanetReceptacle vessel in containers)
        {
            vessel.enabled = false;
            vessel.IsInteractable = false;
        }
    }
}
