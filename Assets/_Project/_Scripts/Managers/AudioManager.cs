using System;
using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource MainAudioSource;

    private IEnumerator SetAudioCoroutine(AudioClip audioClip, float volume, float timeBeforePlaying, bool isAudioClipLoop)
    {
        yield return new WaitForSeconds(timeBeforePlaying);

        if (isAudioClipLoop)
        {
            float length = audioClip.length;

            while (true)
            {
                MainAudioSource.PlayOneShot(audioClip, volume);
                yield return new WaitForSeconds(length);
            }
        }
        else
        {
            MainAudioSource.PlayOneShot(audioClip, volume);
        }
    }

    public void PlayAudio(AudioClip audioClip, float volume, float waitBefore, bool isLoop)
    {
        if (SettingsManager.Instance.IsSoundActivated)
        {
            StartCoroutine(SetAudioCoroutine(audioClip, volume, waitBefore, isLoop));
        }
    }
}