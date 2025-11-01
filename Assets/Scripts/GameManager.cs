using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public int playerMaxHP = 3;

    public event Action OnWin;
    public event Action OnLose;
    public event Action<int, int> OnHealthChanged; 


    public int PlayerHP { get; private set; }
    public bool IsGameOver { get; private set; }

    void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        IsGameOver = false;
        PlayerHP = playerMaxHP;

        OnHealthChanged?.Invoke(PlayerHP, playerMaxHP);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealthUI(PlayerHP, playerMaxHP);
    }

    public void PlayerJumped()
    {
        AudioManager.Instance?.PlayPlayerJump();
    }

    public void PlayerShot()
    {
        AudioManager.Instance?.PlayPlayerShoot();
    }

    public void PlayerDamaged(int damage)
    {
        AudioManager.Instance?.PlayPlayerHit();
        DamagePlayer(damage);
    }

    public void PlayerDied()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        AudioManager.Instance?.PlayPlayerHit();
        OnLose?.Invoke();
    }

    public void DamagePlayer(int dmg)
    {
        if (IsGameOver) return;

        PlayerHP -= dmg;
        PlayerHP = Mathf.Max(0, PlayerHP);
        OnHealthChanged?.Invoke(PlayerHP, playerMaxHP);
        if (PlayerHP <= 0)
            PlayerDied();
    }


    public void EnemyStomped()
    {
        AudioManager.Instance?.PlayEnemyStomp();
    }

    public void EnemyHit()
    {
        AudioManager.Instance?.PlayEnemyHit();
    }


    public void PlayerHitSpike()
    {
        AudioManager.Instance?.PlaySpikeHit();
        DamagePlayer(1);
    }

    public void PlayerHitFriendly()
    {
        AudioManager.Instance?.PlayFriendlyHit();
        DamagePlayer(1);
    }

    public void PlayerReachedGoal()
    {
        AudioManager.Instance?.PlayGoalReached();
        int remainingEnemies = CountEnemiesInScene();
        if (remainingEnemies <= 0)
            Win();
        else
            Debug.Log("Can't win yet — enemies remain.");
    }

    void Win()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        OnWin?.Invoke();
    }

    int CountEnemiesInScene()
    {
        var enemies = FindObjectsOfType<Enemy>();
        int alive = 0;
        foreach (var e in enemies)
            if (e != null && e.IsAlive) alive++;
        return alive;
    }
    public void HealPlayer(int amount)
    {
        if (IsGameOver) return;

        PlayerHP += amount;
        PlayerHP = Mathf.Min(PlayerHP, playerMaxHP); // cap to max HP
        OnHealthChanged?.Invoke(PlayerHP, playerMaxHP);

    }

}
