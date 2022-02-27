using UnityEngine;

public class LevelManager : MonoBehaviour {
    public GameObject environment;
    public PauseManager pause;
    public Transform cam;
    public AudioSource music;

    public static LevelManager self;
    private AudioSource pauseMusic;

    void Start() {
        pauseMusic = GetComponent<AudioSource>();
        self = GetComponent<LevelManager>();
    }

    public static LevelManager GetInstance() {
        return self;
    }

    public void TogglePause() {
        pause.TogglePause();

        if (pauseMusic != null) {
            if (music.isPlaying) {
                music.Pause();
                pauseMusic.Play();
            } else {
                pauseMusic.Pause();
                music.Play();
            }
        }
    }

    public void Quit() {
        Debug.Log("Quit!!!");
        Application.Quit();
    }

    public Vector2 GetCameraPosition() {
        return cam.position;
    }
}
