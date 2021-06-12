using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Instance = this;
    }

    void Start()
    {
        Play("theme");
    }

    public void Play (string name, string soundNameWaitFor = "")
    {        
        Sound s = Array.Find(sounds, sound => sound.name == name);

        float waitTime = 0.1f;
        if(soundNameWaitFor != "")
        {
            Sound wf = Array.Find(sounds, sound => sound.name == soundNameWaitFor);
            waitTime = wf.clip.length;
        }
        StartCoroutine(playEngineSound(s, waitTime));
    }

    public void Play2(string name, float waitTime = 0)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        StartCoroutine(playEngineSound(s, waitTime));
    }

    IEnumerator playEngineSound(Sound s, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        s.source.Play();
    }

    public void DisableLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = false;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }
}