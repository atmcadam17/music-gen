using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public ChordGenerator chordGenerator;
    public Synthesizer[] synths;
    public int length; // # of beats
    
    private float _bpm;
    private float _secPerBeat;
    private float _nextBeatTime; // counts up

    void Start()
    {
        RandomizeSpeed();
    }

    void Update()
    {
        if (Time.time >= _nextBeatTime){
            PlayNextNote();
            _nextBeatTime = Time.time + _secPerBeat;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateNewSong();
        }
    }

    private void GenerateNewSong()
    {
        for (int i = 0; i < synths.Length; i++)
        {
            synths[i].Generate(length);
            synths[i].Reset();
        }
    }

    private void PlayNextNote()
    {
        foreach (var synth in synths)
        {
            synth.PlayNextNote();
        }
    }

    private void RandomizeSpeed()
    {
        _bpm = Random.Range(60, 140);
        _secPerBeat = 60f / _bpm;
    }
}
