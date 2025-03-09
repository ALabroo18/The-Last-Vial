using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        // Limit frame rate to the refresh rate of the monitor.
        QualitySettings.vSyncCount = 1;
    }

    private void Update()
    {
       
    }

    public void LoadCredits()
    {
        LoadScene("End Credits 1");
    }

    public void LoadControls()
    {
        LoadScene("Controls");
    }
    // This function will make it so if you press the quit button, the application will close.
    public void QuitButton()
    {
        Application.Quit();
    }

    public void LoadMenu(){
        LoadScene("TitleScreen");
    }

    public void Resume() {

        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

    // Function that loads the scene.
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadDeathScreen(GameObject deathScreen) {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
    }

}
