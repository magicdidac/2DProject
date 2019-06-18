using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : AController
{
    #region Variables
    //public static AudioController _audioManager = null;

    [SerializeField] private AudioMixer mixer = null;

    public List<Sound> music = new List<Sound>();
    public List<Sound> sounds = new List<Sound>();

    [HideInInspector]
    public List<string> playingMusic = new List<string>();

    private GameObject player;
    private List<Sound> pausedSounds = new List<Sound>();

    #endregion


    #region Initializers
    private void Awake()
    {
        /*if (_audioManager == null) _audioManager = this;
        else
        {
            Destroy(gameObject);
            return;
        }*/

        //DontDestroyOnLoad(this);  //<-- yo creo que se necesita descomentar

        foreach (Sound m in music)
        {
            m.source =  gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.loop = m.loop;
            m.source.playOnAwake = m.playOnAwake;
            m.source.outputAudioMixerGroup = m.mixer;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.mixer;
        }

        

        PlayMusic("radioSong");

    }

    public void Start()
    {
        //esta linea da problemas de audio
        //setAllVolumes(PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 0.75f)) * 20);
    }

    #endregion


    #region Others
    public void PlayMusic(string name)
    {
        Sound m = music.Find(music => music.name == name);
        if (m == null) return;
        //AddPlayingMusic(name);
        if (!m.source.isPlaying)
        {
            m.source.Play();
        }
    }

    public void PlayNestedSound(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("sound " + name + " NOT exist");
            return;
        }
        s.source.Play();
    }

    public void PlaySound(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("sound " + name + " NOT exist");
            return;
        }

        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
        else Debug.LogWarning("sound + " + name + " is Playing");
    }

    public void PlayNewMusic(string name)
    {
        Sound m = music.Find(music => music.name == name);
        if (m == null) return;

        foreach (Sound music in music)
        {
            if (music != m) music.source.Stop();
            if (music != m)
            {
                //
            }
        }
        if (!m.source.isPlaying)
        {
            m.source.Play();
        }
    }


    public void StopSound(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    public void StopMusic(string name)
    {
        Sound m = music.Find(music => music.name == name);
        if (m == null) return;
        Debug.Log("Stopa " + name);
        m.source.Stop();
    }

    public void StopAllMusic()
    {
        foreach (Sound m in music) m.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (Sound m in sounds) m.source.Stop();
        
    }


    public void PlayActiveMusic()
    {
        foreach (Sound m in music)
        {
            foreach (string name in playingMusic)
            {
                if (m.name == name) m.source.Play();
            }
        }
    }

    public void AddPlayingMusic(string name)
    {
        playingMusic.Add(name);
    }

    public void RemoveAllPlayingMusic()
    {
        playingMusic.Clear();
    }

    /*public void SetAllVolumes(float volume)
    {
        Debug.Log(volume);
        foreach (Sound m in music)
        {
            //mapear el volumen
            m.source.volume = volume;
        }
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }*/

    public void PauseAudio()
    {
        foreach (Sound s in sounds)
        {
            if (s != null)
            {
                if (s.source.isPlaying)
                {
                    s.source.Pause();
                    pausedSounds.Add(s);
                }
            }
        }

        foreach (Sound s in music)
        {
            if (s != null)
            {
                if (s.source.isPlaying)
                {
                    s.source.Pause();
                    pausedSounds.Add(s);
                }
            }
        }
    }

    public void ResumeAudio()
    {
        foreach (Sound s in pausedSounds)
        {
            if (s != null)
            {
                s.source.UnPause();
                Debug.Log(s.source.volume);
            }
        }
        pausedSounds.Clear();
    }

    #endregion
}
