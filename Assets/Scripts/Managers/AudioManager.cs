using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip SpinSound,SelectedSound,GameOverSound;

    AudioSource musicSource,effectSource;


    private void Start() {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=0.4f;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.AddHandler(GameEvent.OnSelectedSpin,OnSelect);
        EventManager.AddHandler(GameEvent.OnTargetSpin,OnSpin);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnSelectedSpin,OnSelect);
        EventManager.RemoveHandler(GameEvent.OnTargetSpin,OnSpin);
    }

    void OnSpin()
    {
        effectSource.PlayOneShot(SpinSound);
    }

    void OnSelect()
    {
        effectSource.PlayOneShot(SelectedSound);
    }

    void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }
}
