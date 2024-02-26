using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public string Name;
    public AudioClip soundFile;
    [Header("Use for multiple clips")]
    public List<AudioClip> soundFiles;
    public float volume = 1.0f;

    public AudioClip GetRandomSound()
    {
        if (soundFiles == null || soundFiles.Count == 0) return null;
        int index = Random.Range(0, soundFiles.Count);
        return soundFiles[index];
    }
}
