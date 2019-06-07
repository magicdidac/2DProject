using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : AController
{
    #region Variables
    //public static AudioController _audioManager = null;

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
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        PlayMusic("radioSong");

    }

    public void Start()
    {
        //esta linea da problemas de audio
        //setAllVolumes(PlayerPrefs.GetFloat("MasterVolume"));
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
        Debug.Log("Stopa " + name);
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
        foreach (Sound m in music)
        {
            Debug.Log("StopB " + m.name);
            m.source.Stop();
        }
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

    public void setAllVolumes(float volume)
    {
        foreach (Sound m in music)
        {
            m.source.volume = 1;
        }
        foreach (Sound s in sounds)
        {
            s.source.volume = 1;
        }
    }

    public void PauseMusic()
    {
        foreach (Sound m in music) m.source.Pause();
    }

    /*public void PauseAudio()
    {
        //foreach (Sound m in music) m.source.Pause();

        if (!GameManager._manager.mainMenu)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<GodUsopp>().PauseSounds();

            /*GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

            foreach (GameObject e in enemies)
            {
                e.GetComponent<Enemy>().PauseSounds();
            }      
        }        
    }

    public void ResumeAudio()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().ResumeSounds();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject e in enemies)
        {
            e.GetComponent<Enemy>().ResumeSounds();
        }  

        //PlayActiveMusic();
    }*/

    public void PauseSounds()
    {
        foreach (Sound s in sounds)
        {
            if (s != null)
            {
                if (s.source.isPlaying)
                {
                    s.source.Stop();
                    pausedSounds.Add(s);
                }
            }
        }
    }

    public void ResumeSounds()
    {
        foreach (Sound s in pausedSounds)
        {
            if (pausedSounds.Count > 0 || s != null)
            {
                s.source.volume = PlayerPrefs.GetFloat("MasterVolume");
                s.source.Play();
            }
        }
        pausedSounds.Clear();
    }

    #endregion
}
