using RPG.Scriptable.Base.Event.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioSource BGMSource;

    [Header("Listening Channel")]
    public EventAudio ChannelSFX;
    public EventAudio ChannelBGM;

    public void Awake()
    {
        ChannelSFX.Channel += PlaySFX;
        ChannelBGM.Channel += PlayBGM;
    }

    void PlaySFX(AudioClip clip) {
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    void PlayBGM(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

}
