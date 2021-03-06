using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (AudioManager.instance != null)
            Destroy(AudioManager.instance.gameObject);
          
        instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " was not found. CHECK SPELLING");
            return;
        }

        s.source.Play();
    }

    void Start()
    {
        Play("Drone");
    }
}
