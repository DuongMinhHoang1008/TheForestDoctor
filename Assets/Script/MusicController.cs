using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance {get; private set;}
    AudioSource audioSource;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            instance.audioSource = GetComponent<AudioSource>();
            instance.audioSource.Play();
            instance.audioSource.loop = true;
            DontDestroyOnLoad(instance.audioSource);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AudioSource>() != null && GetComponent<AudioSource>().clip != instance.audioSource.clip) {
            instance.audioSource.clip = GetComponent<AudioSource>().clip;
            instance.audioSource.volume = GetComponent<AudioSource>().volume;
            instance.audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
