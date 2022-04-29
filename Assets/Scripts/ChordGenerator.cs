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
}

public class ChordGenerator : MonoBehaviour
{
    
    public enum ChordNote
    {
        Hi,
        Low,
        Mid
    }

    [SerializeField] private TextMeshProUGUI chordsTmp;
    private int[][] _currentChords;

    public void GenerateSong(int length)
    {
        _currentChords = new int[length][];
        for (int i = 0; i < _currentChords.Length; i++)
        {
            if (i % 4 == 0)
            {
                _currentChords[i] = RandomChord();
            }
            else
            {
                _currentChords[i] = new []{99,99,99};
            }
        }
    }

    private int[] RandomChord()
    {
        List<int[]> cMajorChords = new List<int[]>()
        {
            new []{0,4,7}, // cmajor
            new []{3,6,10}, // dmaj?
            new []{4,7,11}, // em
            new []{5,9,12}, // gmaj
        };
        return cMajorChords[Random.Range(0, cMajorChords.Count)];
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
