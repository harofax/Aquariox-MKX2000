using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinSFX;

    private AudioSource audioSource;

    private static AudioManager _instance;

    public static AudioManager Instance => _instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCoinSFX()
    {
        
        audioSource.clip = coinSFX;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Stop();
        audioSource.Play();
    }
}
