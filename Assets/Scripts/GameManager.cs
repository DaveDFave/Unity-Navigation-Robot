using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip hitSound;
    public int wallHits = 0;
    public TextMeshProUGUI wallHitText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    private bool canHitWall = true;
    public float hitCooldown = 1f;
    public float timeElapsed = 0f;
    private bool timeRunning = true;
    private float score;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("You Win!");
            audioSource.PlayOneShot(winSound);
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
            StopTimer();
            Time.timeScale = 0f; // pause game
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            if(canHitWall){
            wallHits++;
            audioSource.PlayOneShot(hitSound);
            Debug.Log("Wall hit: " + wallHits);
            StartCoroutine(WallHitCooldown());
            }
        }
    }
    IEnumerator WallHitCooldown()
{
    canHitWall = false;
    yield return new WaitForSeconds(hitCooldown);
    canHitWall = true;
}
    void Update()
    {
        wallHitText.text = "Wall Hits: " + wallHits;
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        timerText.text = $"Time: {minutes}:{seconds:00}";
        score = timeElapsed + (wallHits * 2f);
        if (timeRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        timeRunning = false;
        Debug.Log("Final Time: " + timeElapsed);
    }
}