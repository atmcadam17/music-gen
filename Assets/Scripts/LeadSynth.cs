using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeadSynth : Synthesizer
{
    [SerializeField] private TextMeshProUGUI melodyTmp;
    [SerializeField] private ChordGenerator _chordGen;
    
    [SerializeField] private float restChance = .1f; // chance for any note to be a rest
    [SerializeField] private float anchorRestChance = .03f; // rest chance for notes & start of bar
    
    new void Start() {
        base.Start();
    }

    public override void Generate(int length) // takes length in beats
    {
        // generates semitones w/in 2 octaves
        // ONLY choose notes on the major scale
        int[] majScale = {-12,-10,-8,-7,-5,-3,-1,0,2,4,5,7,9,11,12};

        int currentBar = 0;
        for (int i = 0; i < length; i++)
        {
            var randRest = Random.value;
            if (i % 4 == 0)
            {
                // ANCHOR NOTE - generates based on chord generator
                if (randRest < anchorRestChance)
                {
                    notes[i] = 99;
                    continue;
                }

                List<int> referenceNotes = new List<int>();
                foreach (var note in _chordGen.CurrentChords[currentBar * 4])
                {
                    referenceNotes.Add(note);
                }

                var chosenRef = referenceNotes[Random.Range(0, referenceNotes.Count)];
                var r = Random.value;
                if (r<.33f) // third from ref note
                {
                    notes[i] = chosenRef + 3;
                }
                else if (r<.66f) // match ref note
                {
                    notes[i] = chosenRef;
                } else // fifth from ref note
                {
                    notes[i] = chosenRef + 5;
                }
                currentBar++;
                continue;
            }
            
            // PROCESS FOR NOT AN ANCHOR NOTE
            
            if (randRest < restChance)
            {
                notes[i] = 99;
            }
            else
            {
                // TODO: most likely either a step, 3rd, or 5th from prev note?
                notes[i] = majScale[Random.Range(0, majScale.Length)]; // default: random note
            }
        }

        DisplayInfo();
    }

    public override void DisplayInfo()
    {
        var str = "";
        foreach (var n in notes)
        {
            str += n + ",";
        }

        melodyTmp.text = "Melody: " + str;
    }
}
