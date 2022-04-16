using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Audio
{
    [SerializeField] string _name;
    [SerializeField] AudioClip _clip;
    [SerializeField, Range(0, 1)] float _volume = 1;
    [SerializeField] bool _stack;
    [SerializeField] bool _loop;

    public string name {get => _name;}
    public float volume {get => _volume; set => _volume = _source.volume = value; }
    public AudioClip clip {get => _clip; set => _clip = _source.clip = value; }
    public bool stack {get => _stack; set => _stack = value; }
    public bool loop {get => _loop; set => _loop = _source.loop = value; }
    public bool isPlaying {get => _source.isPlaying;}

    AudioSource _source;


    public void Init(GameObject obj) {
        _source = obj.AddComponent<AudioSource>();
        _source.clip = _clip;
        _source.loop = _loop;
        _source.volume = _volume;
    }

    public void Play() { 
        if (_stack) {
            _source.PlayOneShot(_clip);
        } else {
            _source.Play();
        }

    }

    public void Stop() {
        _source.Stop();
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] protected Audio[] _audios;
    [SerializeField] protected GameObject _soundSourceObject;

    protected virtual void Awake()
    {
        if (_soundSourceObject == null) {
            _soundSourceObject = gameObject;
        }

        foreach (var audio in _audios) {
            audio.Init(_soundSourceObject);
        }
    }

    public void Play(string name) {
        GetAudio(name).Play();
    }

    public Audio GetAudio(string name) {
        foreach(var audio in _audios) {
            if (audio.name == name) return audio;
        }

        return null;
    }
}
