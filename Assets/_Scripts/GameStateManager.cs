using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        HidePauseMenu();
    }

    private void Start()
    {
        inputReader.PauseEvent += ShowPauseMenu;
        inputReader.ResumeEvent += HidePauseMenu;
    }

    /// <summary>
    /// Enable pause menu gameObject
    /// </summary>
    private void ShowPauseMenu()
    {
        if (!pauseMenu)
            return;

        GameManager.Instance.EnableCursor(true);
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Disable pause menu gameObject
    /// </summary>
    private void HidePauseMenu()
    {
        if (!pauseMenu)
            return;

        GameManager.Instance.EnableCursor(false);
        pauseMenu.SetActive(false);
    }
}
