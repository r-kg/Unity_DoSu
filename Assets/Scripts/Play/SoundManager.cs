using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource effectPlayer;
    public AudioSource bgmPlayer;

    public AudioClip click, slide, scratch;
    public AudioClip count, ding;
    public AudioClip ui, swipe;
    public AudioClip doMeow0, doMeow1;
    public AudioClip bgmMain, bgmPlay;


    void Awake()
    {
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.AddComponent<AudioSource>();
        Component[] players = this.GetComponents(typeof(AudioSource));
        effectPlayer = (AudioSource)players[0];
        bgmPlayer = (AudioSource)players[1];
        DontDestroyOnLoad(this);
    }

    public void Click()
    {
        effectPlayer.clip = click;
        playSound();
    }

    public void Slide()
    {
        effectPlayer.clip = slide;
        playSound();
    }

    public void Count()
    {
        effectPlayer.clip = count;
        playSound();
    }
    public void UI()
    {
        effectPlayer.clip = ui;
        effectPlayer.time = 0.27f;
        playSound();
    }
    public void Ding()
    {
        effectPlayer.Stop();
        effectPlayer.clip = ding;
        effectPlayer.time = 2;
        playSound();
    }

    public void DoMeow()
    {
        switch(Random.Range(0,2))
        {
            case 0:
                effectPlayer.clip = doMeow0;
            break;
            case 1:
                effectPlayer.clip = doMeow1;
            break;
        }
        
        playSound();
    }
    public void Swipe()
    {
        effectPlayer.Stop();
        effectPlayer.clip = swipe;
        effectPlayer.time = 0;
        playSound();
    }
    public void Scratch()
    {
        effectPlayer.Stop();
        effectPlayer.clip = scratch;
        effectPlayer.time = 0;
        playSound();
    }

    public void playSound()
    {
        effectPlayer.Stop();
        effectPlayer.time = 0;
        effectPlayer.Play();
    }

    public void playBGM(AudioClip bgm)
    {
        if(!bgmPlayer.isPlaying)
        {
            bgmPlayer.Stop();
            bgmPlayer.clip = bgm;
            bgmPlayer.time = 1.3f;
            bgmPlayer.Play();
            bgmPlayer.loop = true;
            
        }
        else
        {
            bgmPlayer.Stop();
            bgmPlayer.clip = bgm;
            bgmPlayer.time = 0;
            bgmPlayer.Play();
            bgmPlayer.loop = true;
        }
    }
}
