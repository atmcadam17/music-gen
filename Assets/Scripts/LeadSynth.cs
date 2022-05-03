using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeadSynth : Synthesizer
{
    [SerializeField] private TextMeshProUGUI melodyTmp;
    [SerializeField] private ChordGenerator _chordGen;

    [SerializeField] private float clampDistance = 5; // how far regular notes can jump
    
    [SerializeField] private float restChance = .1f; // chance for any note to be a rest
    [SerializeField] private float anchorRestChance = .03f; // rest chance for notes & start of bar
    
    new void Start() {
        base.Start();
    }

    public override void Generate(int length) // takes length in beats
    {
        // generates semitones w/in 2 octaves
        // ONLY choose notes on the major scale
        int[] majScale = {-12,-10,-8,-7,-5,-3,-1,0,2,4,5,7,9,11,12,14,16,17,19};

        notes = new int[length];
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
                // TODO: because of how intervals work this sometimes generates a godawful note
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
                
                var onMajorScale = false;
                foreach (var n in majScale)
                {
                    if (n == notes[i])
                    {
                        onMajorScale = true;
                    }
                }

                if (!onMajorScale)
                {
                    if (Random.value < .5f)
                    {
                        notes[i] += 1;
                    }
                    else
                    {
                        notes[i] -= 1;
                    }
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
                // clamp distance
                notes[i] = majScale[Random.Range(0, majScale.Length)]; // default: random note
                
                // TODO:
                // find index of prev note
                // choose random index within ClampDistance
                // that's your note!
                
                /* no.
                while (notes[i-1] != 99 && !(notes[i] <= notes[i-1] + clampDistance && notes[i] >= notes[i-1] - clampDistance))
                { // while NOT within clampDistance of previous note, keep generating until it is
                    notes[i] = majScale[Random.Range(0, majScale.Length)];
                }*/
            }
        }

        DisplayInfo();
    }

    public override void DisplayInfo()
    {
        var str = "";
        foreach (var n in notes)
        {
            if (n == 99)
            {
                str += "-,";
            }
            else
            {
                str += n + ",";
            }
        }

        melodyTmp.text = "Melody: " + str;
    }
}
