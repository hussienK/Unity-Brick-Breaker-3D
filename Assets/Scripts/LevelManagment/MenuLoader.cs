/* < 8 - 13 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for Loading scenes from menu buttons
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    //restart the current level
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //load the level selector menu
    public void BacktoMenu()
    {
        SceneManager.LoadScene("LevelLoader");
    }
    //load the main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
