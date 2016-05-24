using UnityEngine;
using System.Collections;

public class RandomizedAudio : MonoBehaviour
{

    public AudioClip[] clipsToPlay;
    public bool pitchChange = false;
	
	void Start ()
    {
        int selection = Random.Range(0, clipsToPlay.Length);
	    float pitch = 0.0f;
	    if (pitchChange)
	    {
            pitch = Random.Range(0.8f, 1.4f);
        }
	    else
	    {
	        pitch = 1.0f;
	    } 

        AudioSource audioSource = GetComponent<AudioSource>();

        float oldPitch = audioSource.pitch;

        audioSource.pitch = pitch;

        audioSource.PlayOneShot(clipsToPlay[selection]);

        audioSource.pitch = oldPitch;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
