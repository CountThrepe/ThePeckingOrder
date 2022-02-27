using UnityEngine;

public class LevelManager : MonoBehaviour {
    public GameObject environment;
    public PauseManager pause;
    public Transform cam;
    public GameObject rift;

    public static LevelManager self;
    private AudioSource pauseMusic;
    private AudioSource[] musics;
    private bool riftMemory = false;

    void Start() {
        pauseMusic = GetComponent<AudioSource>();
        self = GetComponent<LevelManager>();

        musics = rift.GetComponents<AudioSource>();
    }

    public static LevelManager GetInstance() {
        return self;
    }

    public void TogglePause() {
        pause.TogglePause();

        if (pauseMusic != null) {
            if (!pauseMusic.isPlaying) {
                ToggleOutsideAudio();
                pauseMusic.Play();
            } else {
                pauseMusic.Pause();
                ToggleOutsideAudio();
            }
        }
    }

    private void ToggleOutsideAudio() {
        foreach (AudioSource music in musics) {
            if (music.clip.name.Contains("Level")) {
                if (music.isPlaying) music.Pause();
                else music.Play();
            } else {
                if (music.isPlaying) {
                    music.Pause();
                    riftMemory = true;
                } else if (riftMemory) {
                    music.Play();
                    riftMemory = false;
                }
            }
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public Vector2 GetCameraPosition() {
        return cam.position;
    }
}
