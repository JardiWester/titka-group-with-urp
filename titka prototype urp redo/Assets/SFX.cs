using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walk; 
    public AudioClip run;
    public AudioClip paigeHmm;
    public AudioClip ManHmm;
    public AudioClip ButtonPress;
    public AudioClip ButtonPress2;
    public AudioClip pageFlip;
    void Start()
    {
        // Ensure the audio source is attached
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayWalkSound()
    {
        audioSource.clip = walk;
        audioSource.loop = true; // Enable looping
        audioSource.Play();
        Debug.Log("walksound");
    }

    public void PlayRunSound()
    {
        audioSource.clip = run;
        audioSource.loop = true; // Enable looping
        audioSource.Play();
        Debug.Log("runsound");
    }

    public void PlayDialogueSound()
    {
        audioSource.clip = paigeHmm;
        audioSource.Play();
    }

    public void PlayManHmmSound()
    {
        audioSource.clip = ManHmm;
        audioSource.Play();
    }

    public void PlayButtonSound()
    {
        audioSource.clip = ButtonPress;
        audioSource.Play();
    }

    public void PlayButtonSound2()
    {
        audioSource.clip = ButtonPress2;
        audioSource.Play();
    }

    public void PlayPageFlipSound()
    {
        audioSource.clip = pageFlip;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop(); //stop all sounds
    }
}
