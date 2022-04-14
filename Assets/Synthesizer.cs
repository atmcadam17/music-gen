using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Synthesizer : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public int[] notes = new int[16];
    
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

    // pitch formula to raise a note by n semitones: 1.05946^n
    // plays notes based on distance from root
    private void PlayNote(int semitonesFromRoot)
    {
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

    private void Reset()
    {
        currentIndex = 0;
    }
}
