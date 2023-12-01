using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game properties")]
    [SerializeField] private string gameCode = "25947";

    public static GameManager Instance { get; private set; }
    public string GameCode => gameCode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        EnableCursor(false);
    }

    public void EnableCursor(bool enabled)
    {
        Cursor.visible = enabled;
    }
}
