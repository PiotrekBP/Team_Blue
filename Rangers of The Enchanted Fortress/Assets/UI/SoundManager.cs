using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    [HideInInspector]
    public string name;
    public AudioClip clip;

    

    public void Adjust()
    {
        name = clip.name;
    }

}

    
    

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private float volume = 0.5f;
    [SerializeField]
    private float pitch = 1f;
    public bool play;
    public bool playCalm;

    [SerializeField]
    private Sound[] sounds;

    [SerializeField]
    private GameObject PlayerCharacter;
    [SerializeField]
    private GameObject PlayerCharacterSoundCollider;
    [SerializeField]
    private AudioSource PlayerCharacterSourceCombat;
    [SerializeField]
    private AudioSource PlayerCharacterSourceCalm;
    [SerializeField]
    private AudioSource PlayerCharacterSourceSearch;
    [SerializeField]
    private Sound combatMusic;
    [SerializeField]
    private Sound calmMusic;

    [SerializeField]
    private float speed;

    public static SoundManager instanceRef;


    private void Awake()
    { 
    
        if (instanceRef == null) {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
}
        else if (instanceRef != this)
            Destroy (gameObject);

        foreach (Sound s in sounds)
        {
            s.Adjust();
        }
        PlayerCharacterSourceCombat.Pause();
    }

    private void Update()
    {
        if (play)
        {
            PlayCombatMusic();
            play = false;
        }
        if (playCalm)
        {
            PlayCalmMusic();
            playCalm = false;
        }

    }

    private void Play(Sound sound, AudioSource source)
    {
        source.clip = sound.clip;
        source.pitch = pitch;
        source.volume = volume;
        source.Play(0);
    }

    private void Stop(AudioSource source)
    {
        source.Stop();
    }

    public void PlaySound(string _name, AudioSource source)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                Play(sounds[i], source);
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not found in list, " + _name);
    }

    public void StopSound(AudioSource source)
    {
        Stop(source);
    }


    //Muzyka

    public void PlaySearchMusic()
    {
        SwitchMusic(1);
    }
    public void PlayCombatMusic()
    {
        SwitchMusic(0);
    }
    public void PlayCalmMusic()
    {
        SwitchMusic(2);
    }

    private void SwitchMusic(int type)
    {
        if (type == 0)
        {
            if (PlayerCharacterSourceCalm.isPlaying)
            {
                StartCoroutine(FadeOut(PlayerCharacterSourceCalm, PlayerCharacterSourceCombat, speed));
                // StartCoroutine(FadeIn(PlayerCharacterSourceCombat, speed));

            }
        }
        else if (type == 1)
        {
            if (PlayerCharacterSourceCombat.isPlaying)
            {
                StartCoroutine(FadeOut(PlayerCharacterSourceCombat, PlayerCharacterSourceSearch, speed));
                // StartCoroutine(FadeIn(PlayerCharacterSourceCalm, speed));

            }
        }
        else if (type == 2)
        {
            if(PlayerCharacterSourceSearch.isPlaying)
            {
                StartCoroutine(FadeOut(PlayerCharacterSourceSearch, PlayerCharacterSourceCalm, speed));
            }
        }
    }
    private static IEnumerator FadeOut(AudioSource audioSourceIn, AudioSource audioSourceOut, float FadeTime)
    {
        float startVolume = 1;

        audioSourceOut.Play();

        while (audioSourceIn.volume > 0)
        {
            audioSourceIn.volume -= startVolume * Time.deltaTime / FadeTime;
            audioSourceOut.volume += startVolume * Time.deltaTime / FadeTime;


            yield return null;
        }

        audioSourceIn.Pause();
    }

}