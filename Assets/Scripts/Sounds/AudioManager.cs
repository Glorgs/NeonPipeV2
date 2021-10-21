using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MySingleton<AudioManager>
{

    public SoundParam[] soundsFx;

    public SoundParam[] soundsMusic;

    [SerializeField]
    private float volumeMusic = 1f;

    [SerializeField]
    private float volumeSFX = 1f;

    public float VolumeMusic
    {
        get
        {
            return volumeMusic;
        }
        set
        {
            volumeMusic = value;
        }
    }

    public float VolumeSFX
    {
        get
        {
            return volumeSFX;
        }
        set
        {
            volumeSFX = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        if(PlayerPrefs.HasKey("VolumeMusic"))
        {
            volumeMusic = PlayerPrefs.GetFloat("VolumeMusic");
        }

        if(PlayerPrefs.HasKey("VolumeSFX"))
        {
            volumeSFX = PlayerPrefs.GetFloat("VolumeSFX");
        }

        PlayMusic("Music", this.gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StopSFX(string name)
    {
        try
        {
            SoundParam sp = Array.Find(soundsFx, sound => sound.name.Equals(name));
            sp.audioSource.Stop();
        }
        catch (NullReferenceException)
        {
            Debug.Log("Stop Audio clip" + name + " non existant");
        }
    }

    public void StopMusic(string name)
    {
        try
        {
            SoundParam sp = Array.Find(soundsMusic, sound => sound.name.Equals(name));
            sp.audioSource.Stop();
        }
        catch (NullReferenceException)
        {
            Debug.Log("Stop Audio clip" + name + " non existant");
        }
    }

    public void PlaySFX(string name, GameObject sourceObject)
    {
        try{
            SoundParam sp = Array.Find(soundsFx, sound => sound.name.Equals(name));

            

            //Si vide, on l'initialise (il va avoir la position de l'objet child)
            if(sp.audioSource == null)
            {

                LoadSource(sp, sourceObject);
                sp.audioSource.volume = volumeSFX;

            }

            if(sp.audioSource.isPlaying)
            {
                return;
            }

            if ( name.Equals("FootSteps"))
            {
                float rand = UnityEngine.Random.Range(0.95f,1.5f);
                sp.audioSource.pitch = rand;
            }
            if(sp.loop == true){
                sp.audioSource.Play();
            }
            {
                sp.audioSource.PlayOneShot(sp.audioClip);
            }

            
        }
        catch(NullReferenceException){
             Debug.Log("Audio clip" + name + " non existant");
        }
    }

    public void PlayMusic(string name, GameObject sourceObject)
    {
        try
        {
            SoundParam sp = Array.Find(soundsMusic, sound => sound.name.Equals(name));



            //Si vide, on l'initialise (il va avoir la position de l'objet child)
            if (sp.audioSource == null)
            {

                LoadSource(sp, sourceObject);
                sp.audioSource.volume = volumeMusic;

            }

            if (sp.audioSource.isPlaying)
            {
                return;
            }

            if (name.Equals("FootSteps"))
            {
                float rand = UnityEngine.Random.Range(0.95f, 1.5f);
                sp.audioSource.pitch = rand;
            }
            if (sp.loop == true)
            {
                sp.audioSource.Play();
            }
            {
                sp.audioSource.PlayOneShot(sp.audioClip);
            }


        }
        catch (NullReferenceException)
        {
            Debug.Log("Audio clip" + name + " non existant");
        }
    } 

    void LoadSource(SoundParam sp, GameObject sourceObject)
    {
        sp.audioSource = sourceObject.AddComponent<AudioSource>();

        sp.audioSource.clip = sp.audioClip;

        sp.audioSource.pitch = sp.pitch;
        sp.audioSource.loop = sp.loop;

        sp.audioSource.spatialBlend = sp.spatialBlend;
    }

    public void SetVolumeMusic()
    {
        foreach (SoundParam sp in soundsMusic)
        {
            if (sp.audioSource != null)
            {
                sp.audioSource.volume = volumeMusic;
            }
        }

        PlayerPrefs.SetFloat("VolumeMusic", volumeMusic);
    }

    public void SetVolumeSFX()
    {
        foreach (SoundParam sp in soundsFx)
        {
            if (sp.audioSource != null)
            {
                sp.audioSource.volume = volumeSFX;
            }
        }

        PlayerPrefs.SetFloat("VolumeSFX", volumeSFX);
    }
}
