using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JukeBox : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] songs;

    [SerializeField]
    private AudioSource musicPlayer;

    private int songIndex = 0;

    private bool playing = true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartBGM;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartBGM;
    }

    private void StartBGM(Scene scene, LoadSceneMode mode)
    {
        musicPlayer.clip = songs[songIndex];
        StartCoroutine(StartFade(musicPlayer, 4, musicPlayer.volume));
        musicPlayer.Play();
        StartCoroutine(QueueNextSong(musicPlayer.clip));
    }
    
    public void NextSong()
    {
        songIndex++;
        if (songIndex > songs.Length - 1)
        {
            songIndex = 0;
        }
        musicPlayer.clip = songs[songIndex];
        musicPlayer.Play();
    }

    private IEnumerator QueueNextSong(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        musicPlayer.Stop();
        NextSong();
        StartCoroutine(QueueNextSong(musicPlayer.clip));
    }

    private IEnumerator StartFade(AudioSource audioSource, float fadeTime, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / fadeTime);
            yield return null;
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && playing)
        {
            StopAllCoroutines();
            musicPlayer.Stop();
            playing = false;
        } else if (Input.GetKeyDown(KeyCode.M) && !playing)
        {
            NextSong();
            StartCoroutine(QueueNextSong(musicPlayer.clip));
            playing = true;
        }
    }
}
