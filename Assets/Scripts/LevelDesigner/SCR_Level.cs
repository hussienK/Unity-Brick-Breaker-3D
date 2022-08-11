/* < 8 - 10 - 2022 >
 * Hussien Kenaan
 * 
 * This is a scriptable object for customizing levels
 */
using UnityEngine;

//create a scriptable object
[CreateAssetMenu(fileName ="New Level", menuName = "Bricks/Create Level")]
public class SCR_Level : ScriptableObject
{
    //the width and height of the grid to be created
    public Vector2Int gridSize;

    public LevelSetup[] rows;
}

[System.Serializable]
public class LevelSetup
{
    public int[] columns;
}