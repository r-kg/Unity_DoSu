using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource bgmPlayer, effectPlayer, effectSubPlayer;
    public AudioClip bgm_Main, bgm_Play;
    public AudioClip effect_click, effect_slide, effect_scratch, effect_ui, effect_swipe, effect_count, effect_doMeow0, effect_ding;
    public AudioClip genSound;


    void Awake()
    {
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.AddComponent<AudioSource>();
        Component[] players = this.GetComponents(typeof(AudioSource));
        effectPlayer = (AudioSource)players[0];
        bgmPlayer = (AudioSource)players[1];
        effectSubPlayer = (AudioSource)players[2];

        PlayBGM(SoundManager.Instance.bgm_Main);
        bgmPlayer.volume = 0.4f;
        effectPlayer.volume = 0.5f;
        effectSubPlayer.volume = 0.3f;
        DontDestroyOnLoad(this);
    }

    public void PlayEffect(AudioClip effect)
    {
        effectPlayer.Stop();
        effectPlayer.clip = effect;
        effectPlayer.time = 0;
        effectPlayer.Play();
    }

    public void GenClick()
    {
        
        effectSubPlayer.Stop();
        effectSubPlayer.time = 0;
        effectSubPlayer.clip = genSound;
        effectSubPlayer.Play();
    }

    public void PlayBGM(AudioClip bgm)
    {
        if(!bgmPlayer.isPlaying)
        {
            bgmPlayer.time = 1.3f;
            
        }
        else
        {
            bgmPlayer.time = 0;
        }

        bgmPlayer.Stop();
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
        bgmPlayer.loop = true;
    }
}
