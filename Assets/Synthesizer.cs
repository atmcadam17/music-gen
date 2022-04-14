using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Synthesizer : MonoBehaviour
{
    private AudioSource _audioSource;

    public int currentIndex = 0;
    public int[] notes = new int[16];
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        // StartCoroutine(Test());
        
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i] = Random.Range(-20, 20);
        }
    }

    void Update()
    {
    }
    
    private IEnumerator Test()
    {
        /*
        for (int i = 0; i > -40; i--)
        {
            PlayNote(i);
            yield return new WaitForSeconds(.5f);
        }*/

        // fully random melody
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i] = Random.Range(-20, 20);
        }
        
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
        _audioSource.pitch = Mathf.Pow(1.05946f, semitonesFromRoot);
        _audioSource.Play();
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
            ResetSong();
        }
    }

    private void ResetSong()
    {
        currentIndex = 0;
    }
}
