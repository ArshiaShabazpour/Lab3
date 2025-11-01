using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip playerJumpClip;
    public AudioClip playerShootClip;
    public AudioClip playerHitClip;
    public AudioClip enemyHitClip;
    public AudioClip enemyStompClip;
    public AudioClip spikeHitClip;
    public AudioClip friendlyHitClip;
    public AudioClip goalReachedClip;

    public float volume = 1f;

    private AudioSource audioSource;

    public override void Awake()
    {
        base.Awake();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void Play(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayPlayerJump() => Play(playerJumpClip);
    public void PlayPlayerShoot() => Play(playerShootClip);
    public void PlayPlayerHit() => Play(playerHitClip);

    public void PlayEnemyHit() => Play(enemyHitClip);
    public void PlayEnemyStomp() => Play(enemyStompClip);

    public void PlaySpikeHit() => Play(spikeHitClip);
    public void PlayFriendlyHit() => Play(friendlyHitClip);
    public void PlayGoalReached() => Play(goalReachedClip);
}
