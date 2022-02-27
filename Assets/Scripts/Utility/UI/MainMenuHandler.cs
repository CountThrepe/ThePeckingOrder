using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {
    public void StartPlaying() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() {
        Debug.Log("Exit!");
        Application.Quit();
    }
}
