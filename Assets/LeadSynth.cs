using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadSynth : Synthesizer
{
    new void Start() {
        base.Start();
        GenerateMelody();
    }

    void GenerateMelody()
    {
        // generates semitones w/in 2 octaves
        // ONLY choose notes on the major scale
        int[] majScale = new[] {-10,-8,-7,-5,-3,-1,0, 2, 3, 5, 7, 9, 10};
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i] = majScale[Random.Range(0, majScale.Length)];
        }
    }
}
