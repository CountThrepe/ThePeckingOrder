using UnityEngine;

public class LevelManager : MonoBehaviour {
    public GameObject environment;
    public PauseManager pause;

    public static LevelManager self;

    void Start() {
        self = GetComponent<LevelManager>();
    }

    public static LevelManager GetInstance() {
        return self;
    }

    public void TogglePause() {
        pause.TogglePause();
    }

    public void Quit() {
        Debug.Log("Quit!!!");
        Application.Quit();
    }
}
