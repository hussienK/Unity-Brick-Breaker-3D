/* < 8 - 13 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for Loading assigning a load level function and text for each button in menu
 */
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    //my text refernce
    [SerializeField] private TextMeshProUGUI levelText;
    //the scene to load
    private string levelToLoad;

    //assign the button, called from grid
    public void SetButton(string _levelText, string _levelToLoad)
    {
        levelText.text = _levelText;
        levelToLoad = _levelToLoad;
    }

    //load level, assigned in inspector
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
