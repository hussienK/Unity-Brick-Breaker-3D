/* < 8 - 10 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for managing the scores and updating the UI for it
 */
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    //increase the score then update the ui
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateUI();
    }

    //update the UI for score
    private void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore.ToString("D5");
    }
}
