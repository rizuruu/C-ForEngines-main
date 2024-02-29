using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] List<SoundEffect> sounds = new List<SoundEffect>();
    AudioSource audioSource;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public static void Play(int index)
    {
        if (!ValidateInstance()) return;

        var sound = Instance.sounds[index];
        if (sound == null) return;

        Instance.audioSource.PlayOneShot(sound.soundFile, sound.volume);
    }

    public static void Play(string soundName)
    {
        if (!ValidateInstance()) return;

        var sound = Instance.sounds.Find(x => x.Name == soundName);
        if (sound == null) return;

        Instance.audioSource.PlayOneShot(sound.soundFile, sound.volume);
    }

    public static void PlayRandom(string soundName)
    {
        if (!ValidateInstance()) return;

        var sound = Instance.sounds.Find(x => x.Name == soundName);
        if (sound == null || sound.soundFiles.Count == 0) return;

        AudioClip clipToPlay = sound.GetRandomSound();
        if (clipToPlay != null)
        {
            Instance.audioSource.PlayOneShot(clipToPlay, sound.volume);
        }
    }

    static bool ValidateInstance()
    {
        if (Instance == null)
        {
            Debug.LogError("Tried to play a sound, but instance hasn't been set.");
            return false;
        }
        return true;
    }
}
