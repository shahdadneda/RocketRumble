using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // This function is called when the "Start" button is clicked
    public void StartGame()
    {
        // Replace "Level1" with the name of your first level scene
        SceneManager.LoadScene("Level_1");
    }
}
