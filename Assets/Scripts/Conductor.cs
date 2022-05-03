using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public Synthesizer[] synths; // chord synths MUST come first for generation to work
    [SerializeField] private int minLength = 2;
    [SerializeField] private int maxLength = 8;
    
    private int _lengthInBars = 4;
    
    private float _bpm;
    private float _secPerBeat;
    private float _nextBeatTime; // counts up

    void Start()
    {
        GenerateNewSong();
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
        // randomize speed
        _bpm = Random.Range(60, 140);
        _secPerBeat = 60f / _bpm;
        
        // randomize length
        _lengthInBars = Random.Range(minLength, maxLength);
        
        // synths generate their own pattern
        for (int i = 0; i < synths.Length; i++)
        {
            synths[i].Generate(_lengthInBars * 4);
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
}
