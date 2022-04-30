using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Synthesizer : MonoBehaviour
{
    public bool muted;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public int[] notes; // this is a BAD SOLUTION, but a blank note is 99.
    
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (this is Synthesizer)
        {
            Debug.Log("main synth class");
        }
        // StartCoroutine(Test());
    }
    
    private protected IEnumerator Test()
    {
        /*
        for (int i = 0; i > -40; i--)
        {
            PlayNote(i);
            yield return new WaitForSeconds(.5f);
        }*/

        // fully random melody
        
        foreach (var n in notes)
        {
            PlayNextNote();
            yield return null;
        }
    }

    public virtual void Generate(int length) // takes length in beats
    {
        
    }

    // pitch formula to raise a note by n semitones: 1.05946^n
    // plays notes based on distance from root
    private void PlayNote(int semitonesFromRoot)
    {
        if (muted || semitonesFromRoot == 99) return;
        audioSource.pitch = Mathf.Pow(1.05946f, semitonesFromRoot);
        audioSource.Play();
    }
    
    // called by the conductor
    public void PlayNextNote()
    {
        if (currentIndex < notes.Length)
        {
            PlayNote(notes[currentIndex]);
            currentIndex++;
        }
        else
        {
            Reset();
        }
    }

    public void Reset()
    {
        currentIndex = 0;
    }

    public virtual void DisplayInfo()
    {
        Debug.Log(gameObject);
    }
}
