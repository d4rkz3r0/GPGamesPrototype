using UnityEngine;
using System.Collections;

public class RandomizedAudio : MonoBehaviour {

    public AudioClip[] clipsToPlay;

	// Use this for initialization
	void Start () {
        int selection = Random.Range(0, clipsToPlay.Length);
        float pitch = Random.Range(.8f, 1.4f);

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
