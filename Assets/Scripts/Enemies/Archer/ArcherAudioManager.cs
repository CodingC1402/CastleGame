using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAudioManager : AudioManager
{
    [SerializeField] string _bowDrawingName = "";
    [SerializeField] string _bowFireName = "";

    Audio _bowDrawing = null;
    Audio _bowFire = null;

    protected override void Awake()
    {
        base.Awake();
        _bowDrawing = GetAudio(_bowDrawingName);
        _bowFire = GetAudio(_bowFireName);
    }

    public void PlayBowDrawing() { 
        _bowDrawing.Play();
    }

    public void PlayBowFire() {
        _bowFire.Play();
    }
}
