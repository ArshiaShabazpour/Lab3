using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Elements")]
    public Slider healthBar;
    public GameObject winPanel;
    public GameObject losePanel;

    public override void Awake()
    {
        base.Awake();

        winPanel?.SetActive(false);
        losePanel?.SetActive(false);

        // If Slider is null, warn
        if (healthBar == null)
            Debug.LogWarning("UIManager: HealthBar not assigned in inspector!");
    }

    void OnEnable()
    {
        Subscribe();
    }

    void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnHealthChanged += UpdateHealthUI;
        GameManager.Instance.OnWin += ShowWinPanel;
        GameManager.Instance.OnLose += ShowLosePanel;

        Debug.Log("UIManager: Subscribed to GameManager events");
        Debug.Log($"UIManager: Subscribing to GameManager instance {GameManager.Instance.GetInstanceID()}");
    }

    private void Unsubscribe()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnHealthChanged -= UpdateHealthUI;
        GameManager.Instance.OnWin -= ShowWinPanel;
        GameManager.Instance.OnLose -= ShowLosePanel;
    }

    public void UpdateHealthUI(int currentHP, int maxHP)
    {
        if (healthBar == null) return;

        healthBar.value = (float)currentHP / maxHP;
        Debug.Log($"UIManager: Updating health UI → {currentHP}/{maxHP}");
        Debug.Log($"UIManager: UpdateHealthUI called → {currentHP}/{maxHP} | Slider value before={healthBar.value}");
    }

    public void ShowWinPanel()
    {
        winPanel?.SetActive(true);
        Debug.Log("UIManager: ShowWinPanel called");
    }

    public void ShowLosePanel()
    {
        losePanel?.SetActive(true);
        Debug.Log("UIManager: ShowLosePanel called");
    }
}
