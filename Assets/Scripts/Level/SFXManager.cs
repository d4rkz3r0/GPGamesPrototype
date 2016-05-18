using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngineInternal;

//Totally a singleton
public class SFXManager : MonoBehaviour
{
    //Main
    private static SFXManager instance;
    public List<AudioClip> audioClipBank;
    private Dictionary<string, int> audioLookupTable;

    //Field Accessor
    public static SFXManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<SFXManager>();

                if (!instance)
                {
                    GameObject freshInstance = new GameObject();
                    freshInstance.name = "SFX Manager";
                    instance = freshInstance.AddComponent<SFXManager>();
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        if (!GenerateMap())
        {
            Debug.LogWarning("SFXManager: AudioClip Array is Empty!");
        }

    }


    //FileName OR Index Lookup
    public void PlaySFX(string audioClipFileName = "", int audioClipArrayIndex = -1, float volume = 1.0f, float pitch = 1.0f)
    {
        bool fileNameLookup = false;

        if (audioClipFileName != "" && audioClipArrayIndex != -1)
        {
            Debug.LogError("SFXManager: Invalid use of PlaySFX(FileName or Index!)");
        }

        if (audioClipFileName != "" && audioClipArrayIndex == -1)
        {
            fileNameLookup = true;
        }

        if (fileNameLookup)
        {
            if (!audioLookupTable.ContainsKey(audioClipFileName))
            {
                Debug.LogWarning("SFXManager: Couldn't find your AudioFile, check " +
                                 "the array on the ServiceContainer.");
            }
            PlaySFXHelper(audioClipBank[audioLookupTable[audioClipFileName]], volume, pitch);
        }
        else
        {
            if (!audioLookupTable.ContainsValue(audioClipArrayIndex))
            {
                Debug.LogWarning("SFXManager: Couldn't find your AudioFile, check " +
                                 "the array on the ServiceContainer.");
            }
            PlaySFXHelper(audioClipBank[audioClipArrayIndex], volume, pitch);
        }
    }

    //-Helper Funcs-//
    private bool GenerateMap()
    {
        audioLookupTable = new Dictionary<string, int>();

        for (int audioClipIndex = 0; audioClipIndex < audioClipBank.Count; audioClipIndex++)
        {
            if (audioClipBank[audioClipIndex].name != "Null")
            {
                string temp = audioClipBank[audioClipIndex].name;
                audioLookupTable.Add(temp, audioClipIndex);
            }
            else
            {
                break;
            }
        }

        if (audioLookupTable.Count > 0)
        {
            return true;
        }
        return false;
    }

    private void PlaySFXHelper(AudioClip audioClipToPlay, float volume, float pitch)
    {
        //Instantiate
        GameObject audioSourceGameObject = new GameObject();
        audioSourceGameObject.name = "SFX Object: " + audioClipToPlay.name;
        audioSourceGameObject.transform.position = new Vector3(666.0f, 666.0f, 666.0f);

        //Configure
        AudioSource audioSourceComponent = audioSourceGameObject.AddComponent<AudioSource>();
        audioSourceComponent.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSourceComponent.clip = audioClipToPlay;
        audioSourceComponent.volume = volume;
        audioSourceComponent.pitch = pitch;
        //Play
        audioSourceComponent.Play();

        //Auto Cleanup
        Destroy(audioSourceComponent.gameObject, audioClipToPlay.length);
    }
}