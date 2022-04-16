using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : AudioManager
{
    [SerializeField] string _jumpAudioName;
    [SerializeField] string _landAudioName;
    [SerializeField] string _hitAudioName;
    [SerializeField] string[] _walkAudiosName;
    [SerializeField] string[] _attackAudiosName;
    List<Audio> _walkAudios = new List<Audio>();
    List<Audio> _attackAudios = new List<Audio>();
    Audio _jumpAudio;
    Audio _landAudio;
    Audio _hitAudio;
    int _walkAudiosIndex = 0;
    int _attackAudiosIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        foreach(string name in _walkAudiosName) {
            var audio = GetAudio(name);
            _walkAudios.Add(audio);
            audio.loop = false;
            audio.stack = true;
        }

        foreach(string name in _attackAudiosName) {
            var audio = GetAudio(name);
            _attackAudios.Add(audio);
            audio.loop = false;
            audio.stack = true;
        }

        _jumpAudio = GetAudio(_jumpAudioName);
        _landAudio = GetAudio(_landAudioName);
        _hitAudio = GetAudio(_hitAudioName);
    }

    public void PlayWalkAudio() {
        _walkAudios[_walkAudiosIndex++].Play();
        _walkAudiosIndex %= _walkAudiosName.Length;
    }

    public void PlayJumpAudio() {
        _jumpAudio.Play();
    }
    public void ResetAttack() {
        _attackAudiosIndex = 0;
    }
    public void PlayAttackAudio() {
        _attackAudios[_attackAudiosIndex++].Play();
        _attackAudiosIndex %= _attackAudiosName.Length;
    }
    public void PlayLandAudio() {
        _landAudio.Play();
    }
    public void PlayHitAudio() {
        _hitAudio.Play();
    }
}
