using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController _audioManager = null;

    public List<Sound> music = new List<Sound>();
    public List<Sound> sounds = new List<Sound>();

    [HideInInspector]
    public List<string> playingMusic = new List<string>();

    private GameObject player;
    private List<Sound> pausedSounds = new List<Sound>();

    private void Awake()
    {
        if (_audioManager == null) _audioManager = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        foreach (Sound m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.loop = m.loop;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

    }

    public void Start()
    {
        setAllVolumes(PlayerPrefs.GetFloat("MasterVolume"));
    }

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

    public void PlaySound(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;

        if (name == "TargetSlash") s.source.Play(); // targetslash sound has more duration than his cooldown
        else if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void PlayNewMusic(string name)
    {
        Sound m = music.Find(music => music.name == name);
        if (m == null) return;
        foreach (Sound music in music)
        {
            if (music != m) music.source.Stop();
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
        m.source.Stop();
    }

    public void StopAllMusic()
    {
        foreach (Sound m in music) m.source.Stop();
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
            m.source.volume = volume;
        }
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
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
}
