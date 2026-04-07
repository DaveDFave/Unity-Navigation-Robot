using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;

    private int difficultyIndex = 0;
    private string[] difficulties = { "Easy", "Medium", "Hard" };
    public AudioSource audioSource;
    public AudioClip changeSound;

    public void ChangeDifficulty()
    {
        difficultyIndex = (difficultyIndex + 1) % difficulties.Length;
        difficultyText.text = "Difficulty: " + difficulties[difficultyIndex];
        audioSource.PlayOneShot(changeSound);
    }

    public string GetDifficulty()
    {
        return difficulties[difficultyIndex];
    }
}