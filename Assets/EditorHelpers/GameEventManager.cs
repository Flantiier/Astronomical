using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private GameEvent[] events;
    [SerializeField] private GameEventEditorButton buttonPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Transform grid;

    private void Awake()
    {
        CreateButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.Instance.EnableCursor(true);

            if (!content.gameObject.activeSelf)
                ShowContentPanel();
            else
                HideContentPanel();
        }
    }

    //Create buttons for each event in gameEvent array
    private void CreateButtons()
    {
        foreach (GameEvent item in events)
        {
            GameEventEditorButton instance = Instantiate(buttonPrefab, grid);
            instance.SetButtonTitle(item.eventName);
            instance.Button.onClick.AddListener(item.Raise);
            instance.Button.onClick.AddListener(HideContentPanel);
        }
    }

    /// <summary>
    /// Enable content panel
    /// </summary>
    private void ShowContentPanel() => content.gameObject.SetActive(true);

    /// <summary>
    /// Disable content panel
    /// </summary>
    private void HideContentPanel()
    {
        content.gameObject.SetActive(false);
        GameManager.Instance.EnableCursor(false);
    }
}
