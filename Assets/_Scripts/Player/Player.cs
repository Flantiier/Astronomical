using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }
    public FPSController Controller { get; private set; }
    public PlayerInteract Interactor { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Controller = GetComponent<FPSController>();
        Interactor = GetComponent<PlayerInteract>();
    }
}