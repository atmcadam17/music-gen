using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// TODO: implement data structure
public class Chord
{
    public int[] Notes;
    public Types Type;

    public Chord(int[] notes, Types type)
    {
        Notes = notes;
        Type = type;
    }
    
    public enum Types{ // these aren't entirely right but for all intents & purposes are fine categories for now
        Dom,
        Subdom,
        Tonic
    }
}

public class ChordGenerator : MonoBehaviour
{
    private List<Chord> cMajorChords = new List<Chord>()
    {
        new Chord(new []{0,4,7}, Chord.Types.Tonic), // Cmaj
        new Chord(new []{-8,0,5}, Chord.Types.Tonic), // Cmaj 1st inversion
        new Chord(new []{2,5,9}, Chord.Types.Subdom), // Dm
        new Chord(new []{4,7,11}, Chord.Types.Tonic), // Em
        new Chord(new []{5,9,12}, Chord.Types.Subdom), // Fmaj
        new Chord(new []{0,5,9}, Chord.Types.Subdom), // Fmaj 1st inversion
        new Chord(new []{7,11,14}, Chord.Types.Dom), // Gmaj
        new Chord(new []{2,7,11}, Chord.Types.Dom), // Gmaj 1st inv
        new Chord(new []{4,9,12}, Chord.Types.Tonic), // Am 1st inv
        new Chord(new []{0,4,9}, Chord.Types.Tonic), // Am 2nd inv
        new Chord(new []{2,5,11}, Chord.Types.Dom), // Bdim 2nd inv
    };
    
    /*
    private List<int[]> cMajorChords = new List<int[]>()
    {
        new []{0,4,7}, // cmajor
        new []{3,6,10}, // dmaj?
        new []{4,7,11}, // em
        new []{5,9,12}, // gmaj
    };*/
    
    public enum ChordNote
    {
        Hi,
        Low,
        Mid
    }

    [SerializeField] private TextMeshProUGUI chordsTmp;
    private int[][] _currentChords;

    public void GenerateSong(int length) // takes length in beats
    {
        _currentChords = new int[length][];
        for (int i = 0; i < _currentChords.Length; i++)
        {
            if (i % 4 == 0)
            {
                _currentChords[i] = RandomChord(length, i/4);
            }
            else
            {
                _currentChords[i] = new []{99,99,99};
            }
        }
    }

    // called during song generation
    private int[] RandomChord(int songLength, int currentBar)
    {
        Chord.Types chordType = Chord.Types.Tonic; // tonic by default
        
        // choose chord type
        if (songLength % 4 == 0) // 4 bar loop: tonic, subdom, dom, tonic
        {
            switch (currentBar % 4)
            {
                case 0: // bar 1
                    chordType = Chord.Types.Tonic;
                    break;
                case 1: // bar 2
                    chordType = Chord.Types.Subdom;
                    break;
                case 2: // and so on
                    chordType = Chord.Types.Dom;
                    break;
                case 3:
                    chordType = Chord.Types.Tonic; // TODO: SAME TONIC CHORD AS FIRST
                    break;
            }
        } else if (songLength % 2 == 0) // 2 bar loop: tonic, dom
        {
            switch (currentBar % 2)
            {
                case 0: // 1st bar
                    chordType = Chord.Types.Tonic;
                    break;
                case 1: // 2nd bar
                    chordType = Chord.Types.Dom;
                    break;
            }
        } else if (songLength % 3 == 0) // 3 bar loop: tonic, subdom, dom
        {
            switch (currentBar % 3)
            {
                case 0:
                    chordType = Chord.Types.Tonic;
                    break;
                case 1:
                    chordType = Chord.Types.Subdom;
                    break;
                case 2:
                    chordType = Chord.Types.Dom;
                    break;
            }
        }
        else
        { // default to 4 bar. fix to be less repetitive - code is the same as above...
            switch (currentBar % 4)
            {
                case 0: // bar 1
                    chordType = Chord.Types.Tonic;
                    break;
                case 1: // bar 2
                    chordType = Chord.Types.Subdom;
                    break;
                case 2: // and so on
                    chordType = Chord.Types.Dom;
                    break;
                case 3:
                    chordType = Chord.Types.Tonic;
                    break;
            }
        }
        
        // get chord of chordType
        List<Chord> possibleChords = new List<Chord>();
        foreach (var c in cMajorChords)
        {
            if (c.Type == chordType)
            {
                possibleChords.Add(c);
            }
        }
        return possibleChords[Random.Range(0, possibleChords.Count)].Notes; // choose randomly & return notes
    }

    public int[] GetChordProgression(ChordNote note)
    {
        var index = 0;
        switch (note)
        {
            case ChordNote.Low:
                index = 0;
                break;
            case ChordNote.Mid:
                index = 1;
                break;
            case ChordNote.Hi:
                index = 2;
                break;
        }
        
        var progression = new int[0];
        var newProgression = new int[0];
        for (int i = 0; i < _currentChords.Length; i++)
        {
            newProgression = new int[progression.Length + 1];
            Array.Copy(progression, newProgression, progression.Length);
            newProgression[i] = _currentChords[i][index];
            progression = newProgression;
        }

        return progression;
    }
}
