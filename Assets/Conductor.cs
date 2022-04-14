using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public Synthesizer[] synths;
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
