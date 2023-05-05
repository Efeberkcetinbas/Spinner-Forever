using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip SpinSound,SelectedSound,GameOverSound,AreaOpenSound,EnemyDeadSound;

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
        EventManager.AddHandler(GameEvent.OnAreaOpen,OnAreaOpen);
        EventManager.AddHandler(GameEvent.OnBossDie,OnBossDie);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnSelectedSpin,OnSelect);
        EventManager.RemoveHandler(GameEvent.OnTargetSpin,OnSpin);
        EventManager.RemoveHandler(GameEvent.OnAreaOpen,OnAreaOpen);
        EventManager.RemoveHandler(GameEvent.OnBossDie,OnBossDie);
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

    void OnAreaOpen()
    {
        effectSource.PlayOneShot(AreaOpenSound);
    }

    void OnBossDie()
    {
        effectSource.PlayOneShot(EnemyDeadSound);
    }
}
