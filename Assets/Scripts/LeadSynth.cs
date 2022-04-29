using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeadSynth : Synthesizer
{
    [SerializeField] private TextMeshProUGUI melodyTmp;
    
    new void Start() {
        base.Start();
        Generate(16);
    }

    public override void Generate(int length)
    {
        // generates semitones w/in 2 octaves
        // ONLY choose notes on the major scale
        int[] majScale = new[] {-10,-8,-7,-5,-3,-1,0, 2, 3, 5, 7, 9, 10};
        for (int i = 0; i < length; i++)
        {
            notes[i] = majScale[Random.Range(0, majScale.Length)];
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
