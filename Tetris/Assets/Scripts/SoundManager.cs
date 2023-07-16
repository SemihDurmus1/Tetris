using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public IconManager musicIcon;
    public IconManager fxIcon;

    #region Music Variables
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;
    private AudioClip randomMusicClip;
    public bool musicPlaying = true;
    #endregion

    #region FX Variables
    [SerializeField] private AudioSource[] soundFX;
    public bool effectPlaying = true;
    #endregion

    [SerializeField] private AudioSource[] vocalClips;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        randomMusicClip = RandomClipChoose(musicClips);
        BackgroundPlayMusic(randomMusicClip);
    }

    public void DoVocalSound(int whichLocal)
    {
        if (!effectPlaying) { return; }

        AudioSource source = vocalClips[whichLocal];

        source.Stop();
        source.Play();

    }

    public void DoSoundFX(int whichFX)
    {
        if (effectPlaying && whichFX < soundFX.Length ) 
        {
            soundFX[whichFX].Stop();
            soundFX[whichFX].Play();
        }
    }


    AudioClip RandomClipChoose(AudioClip[] clips)
    {
        AudioClip rastgeleClip = clips[Random.Range(0, clips.Length)];
        return rastgeleClip;
    }

    public void BackgroundPlayMusic(AudioClip musicClip)
    {
        if (!musicClip || !musicSource || !musicPlaying) { return; }

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void MusicUpdate()
    {
        if(musicSource.isPlaying != musicPlaying) 
        {
            if (musicPlaying)
            {
                randomMusicClip = RandomClipChoose(musicClips);
                BackgroundPlayMusic(randomMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void MusicOpenClose()
    {
        musicPlaying = !musicPlaying;
        MusicUpdate();

        musicIcon.IconOpenClose(musicPlaying);
    }

    public void FXOpenClose()
    {
        effectPlaying = !effectPlaying;

        fxIcon.IconOpenClose(effectPlaying);
    }
}
