using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.PlayDelayed(1); 
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play(); 
    }
}
