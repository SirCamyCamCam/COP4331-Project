//-------------------------------------------------------------------------------------------------
//  Coloniant - AudioCore                                                                 2/16/2020
// 
//  AUTHOR:  Cameron Carstens
//  CONTACT: cameroncarstens@knights.ucf.edu
//-------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCore : MonoBehaviour {

    #region Enums

    public enum AudioChannel : int
    {
        PLAYER_SOUNDS,
        EFFECT_SOUNDS,
        BACKGROUND_MUSIC,
        MENU_SOUNDS
    }

    #endregion

    #region Inner Classes

    [System.Serializable]
    public class AudioRef
    {
        public string name;
        public AudioClip clipRef;
        [Range(0f,1f)]
        public float volume = 1f;
        public bool isLooping;
    }

    #endregion

    #region Static Fields

    public static AudioCore main;

    public static AudioCore Instance { get { return main; } }

    #endregion

    #region Inspector Fields

    public AudioSource[] audioSources;

    [Space(10)]

    public AudioRef[] audioReferences;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            main = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        //PlaySound(AudioChannel.BACKGROUND_MUSIC, "BackgroundMusic", false, null);
    }

    #endregion

    #region Public Methods

    // Determines channel and clip to play
    public void PlaySound(AudioChannel audioChannel, string clipName, bool isPlayAfter, string nextClip)
    {
        AudioRef myRef = ParseAudioRef(clipName);

        int channelIndex = (int)audioChannel;

        StopChannel(audioChannel);

        // This will be used later to save volume
        /*if (SaveLoad.main != null)
        {
            audioSources[channelIndex].volume = SaveLoad.main.gameData.masterVolume * myRef.volume;
        }*/

        audioSources[channelIndex].clip = myRef.clipRef;
        audioSources[channelIndex].loop = myRef.isLooping;

        audioSources[channelIndex].Play();

        if(isPlayAfter == true)
        {
            StartCoroutine(WaitForSound(nextClip));
        }
    }

    // Stops channel no matter what sound is playing
    public void StopChannel(AudioChannel audioChannel)
    {
        int channelIndex = (int)audioChannel;

        audioSources[channelIndex].Stop();
    }

    // Pauses selected channel no matter what sound is playing
    public void PauseChannel(AudioChannel audioChannel)
    {
        int channelIndex = (int)audioChannel;

        audioSources[channelIndex].Pause();
    }

    // UnPauses the selected channel
    public void UnPauseSound(AudioChannel audioChannel)
    {
        int channelIndex = (int)audioChannel;

        audioSources[channelIndex].UnPause();
    }

    // Returns a AudioRef to use based on the string
    public AudioRef ParseAudioRef(string name)
    {
        AudioRef foundRef = null;

        foreach (AudioRef audioRef in audioReferences)
        {
            if (audioRef.name == name)
            {
                foundRef = audioRef;
                break;
            }
        }

        // In case miss spelling of audio clip
        if (foundRef == null)
        {
            Debug.LogError("<b>" + name + "</b> audio effect not found!");
        }

        return foundRef;
    }

    public void UpdateBackgoundVolume(float volume)
    {
        int channelIndex = (int)AudioChannel.BACKGROUND_MUSIC;
        audioSources[channelIndex].volume = volume;
    }

    #endregion

    #region Coroutine

    private IEnumerator WaitForSound(string clipname)
    {
        AudioRef myRef = ParseAudioRef(clipname);
        yield return new WaitForSeconds(myRef.clipRef.length);
        PlaySound(AudioChannel.PLAYER_SOUNDS, clipname, false, null);
    }

    #endregion
}
