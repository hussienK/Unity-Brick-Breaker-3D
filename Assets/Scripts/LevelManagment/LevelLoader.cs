/* < 8 - 13 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for creating buttons in grid and assigning there level to load
 */
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    //the button prefab
    [SerializeField] GameObject levelButtonPrefab;
    //name of all levels
    [SerializeField] string[] levelsToLoad;
    //the grid with layout on it
    [SerializeField] Transform grid;

    private void Start()
    {
        CreateLevelButton();
    }

    private void CreateLevelButton()
    {
        //loop through all designed levels
        for (int i = 0; i < levelsToLoad.Length; i++)
        {
            //create a button
            GameObject newButton = Instantiate(levelButtonPrefab);
            //assign button text and level
            newButton.GetComponent<LevelButton>().SetButton((i + 1).ToString(), levelsToLoad[i]);
            //set button as part of layout
            newButton.transform.SetParent(grid, false);
        }
    }
}
