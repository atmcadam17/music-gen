using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chords : Synthesizer
{
    public ChordGenerator.ChordNote chordNote;
    public ChordGenerator chordGenerator;
    public TextMeshProUGUI noteTMP;
    
    new void Start() {
        base.Start();
    }
    
    public override void Generate(int length) // takes length in beats
    {
        chordGenerator.GenerateSong(length);
        notes = chordGenerator.GetChordProgression(chordNote);
        DisplayInfo();
    }

    public override void DisplayInfo()
    {
        var str = "";
        foreach (var n in notes)
        {
            str += n + ",";
        }

        noteTMP.text = "Chord" + chordNote + ":" + str;
    }
}
