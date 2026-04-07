using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public DifficultyManager difficultyManager;
    public AudioSource audioSource;
    public AudioClip changeSound;
    public void PlayGame()
    {
        audioSource.PlayOneShot(changeSound);
        string diff = difficultyManager.GetDifficulty();

        if (diff == "Easy")
            SceneManager.LoadScene("EasyMaze");
        else if (diff == "Medium")
            SceneManager.LoadScene("MediumMaze");
        else if (diff == "Hard")
            SceneManager.LoadScene("MediumMaze");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}