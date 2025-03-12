using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class VialWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            LoadWinScreen(); LoadWinScreen();
        }
    }
    void LoadWinScreen()
    {
        Debug.Log("You Win!");
        SceneManager.LoadScene("WinScreen");
    }
}