using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private List<AudioData> audioData;

    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) CopyToDictionary();
    }

    private void Start()
    {
        if (instance == null)
        {
            bgmSource.clip = GetAudioCip("bgm");
            bgmSource.loop = true;
            bgmSource.Play();

            instance = this;
            SetVolumeBGM(0.5f);

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Copy the value of clips audioData list to clips Dictionary, so I can call it by using clip's name.
    void CopyToDictionary()
    {
        foreach (AudioData data in audioData)
        {
            clips.Add(data.clipName, data.clip);
        }
    }

    // To get the audio that I want to call by its clip's name
    public AudioClip GetAudioCip(string nameClip)
    {
        return clips[nameClip];
    }

    // To set how louder or lower the bgm plays
    public void SetVolumeBGM(float volume)
    {
        bgmSource.volume = volume;
    }

    // To set how louder or lower the sfx plays
    public void SetVolumeSFX(float volume)
    {
        sfxSource.volume = volume;
    }

    // To play sfx just one time, not looping
    public void PlaySFX(string nameClip)
    {
        sfxSource.PlayOneShot(GetAudioCip(nameClip));
    }
}

[System.Serializable]
public class AudioData
{
    // An Abstract Data Type to store clips and its name, so I can use it to my list and dictionary
    public string clipName;
    public AudioClip clip;
}
