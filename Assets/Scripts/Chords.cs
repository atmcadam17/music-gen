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
        notes = new int[length];
        chordGenerator.GenerateSong(length);
        notes = chordGenerator.GetChordProgression(chordNote);
        chordGenerator.setRandomSound(possibleSounds[Random.Range(0,possibleSounds.Count)]);
        DisplayInfo();
    }

    public override void DisplayInfo()
    {
        var str = "";
        foreach (var n in notes)
        {
            if (n == 99)
            {
                str += "~,";
            }
            else
            {
                str += n + ",";
            }
        }

        noteTMP.text = "Chord" + chordNote + ":" + str;
    }
}
