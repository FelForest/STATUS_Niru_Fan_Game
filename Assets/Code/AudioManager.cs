using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;
    private AudioSource jumpAudioSource;
    private AudioSource ObstructionAudioSource;
    
    [SerializeField] private AudioClip countdownClip;
    private AudioSource countdownAudioSource;
    
    [SerializeField] private AudioClip getTargetClip;
    private AudioSource getTargetAudioSource;
    
    [SerializeField] private AudioClip failClip;
    private AudioSource failAudioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        jumpAudioSource = SpawnAudioSource(jumpClip);
        ObstructionAudioSource = SpawnAudioSource(null);
        countdownAudioSource = SpawnAudioSource(countdownClip);
        getTargetAudioSource =  SpawnAudioSource(getTargetClip);
        failAudioSource = SpawnAudioSource(failClip);
    }
    
    
    public void PlayBGM(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(clip);
    }

    public void PlayJumpSound()
    {
        if (jumpClip)
        {
            jumpAudioSource.Play();
        }
    }

    AudioSource SpawnAudioSource(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
        return source;
    }

    public void PlayObstruction(AudioClip clip)
    {
        if (clip)
        {
            ObstructionAudioSource.clip = clip;
            ObstructionAudioSource.Play();
        }
    }

    public void PlayCountDown()
    {
        if (countdownClip)
        {
            ObstructionAudioSource.clip = countdownClip;
            ObstructionAudioSource.Play();
        }
    }

    public void PlayGetTarget()
    {
        if (getTargetClip)
        {
            ObstructionAudioSource.clip = getTargetClip;
            ObstructionAudioSource.Play();
        }
    }

    public void PlayFail()
    {
        if (failClip)
        {
            failAudioSource.clip = failClip;
            failAudioSource.Play();
        }
    }
}
