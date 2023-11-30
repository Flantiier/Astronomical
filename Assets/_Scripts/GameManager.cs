using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
