using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour {
    public bool crash = false;
    public int nextScene = 1;

    void Update() {
        if (Keyboard.current.escapeKey.isPressed) NextScene();
    }

    public void SetNextScene(int val) {
        nextScene = val;
    }

    public void NextScene() {
        if(crash) Application.Quit();
        else SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + nextScene) % SceneManager.sceneCountInBuildSettings);
    }
}
