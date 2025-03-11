using UnityEngine;
using UnityEngine.SceneManagement; 

public class VialWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            LoadWinScreen(); LoadWinScreen();
        }
    }
    void LoadWinScreen()
    {
        Debug.Log("You Win!");
        SceneManager.LoadScene("WinScreen");
    }
}