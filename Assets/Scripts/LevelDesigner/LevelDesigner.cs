/* < 8 - 10 - 2022 >
 * Hussien Kenaan
 * 
 * This is script takes the level data and creates levels based on it with custome settings from inspector
 */
using UnityEngine;
using System.Collections.Generic;

public class LevelDesigner : MonoBehaviour
{
    //create a class for brick sets
    [System.Serializable]
    public class StoneSet
    {
        public GameObject brickPrefab;
        public Color32 color;
    }


    //create a list of avaliable sets
    [SerializeField] private List<StoneSet> brickList = new List<StoneSet>();

    [Header("Level to load")]
    [SerializeField] private SCR_Level level;

    [Header("Start Position")]
    [SerializeField] private Vector2 startPosition;

    [Header("Margin")]
    [SerializeField] private float distanceX = 2;
    [SerializeField] private float distanceY = 1;

    [Header("Odd Offset")]
    [SerializeField] private bool offsetOddRow;
    [SerializeField]  private float offset = 1;

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        //quit if no level selected
        if (level == null)
        {
            return;
        }

        //loop through the data
        for (int i = 0; i < level.rows.Length; i++)
        {
            for (int j = 0; j < level.rows[i].columns.Length; j++)
            {
                //get spawn positions
                float xDist = ((i % 2 == 1) && offsetOddRow) ? j * distanceX + offset : j * distanceX;
                Vector3 pos = new Vector3(startPosition.x + xDist, startPosition.y + i * -distanceY, 0);

                //check if should build brick
                if (level.rows[i].columns[j] != 0)
                {
                    //get type of brick to spawn
                    int number = level.rows[i].columns[j];
                    GameObject newBrick = Instantiate(brickList[number - 1].brickPrefab, pos, Quaternion.identity);
                    newBrick.GetComponent<MeshRenderer>().material.color = brickList[number - 1].color;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //check if level selected
        if (level != null)
        {
            //loop through the grid
            for (int i = 0; i < level.gridSize.y; i++)
            {
                for (int j = 0; j < level.gridSize.x; j++)
                {
                    //get spawn positions
                    float xDist = ((i % 2 == 1) && offsetOddRow) ? j * distanceX + offset : j * distanceX;
                    Vector3 pos = new Vector3(startPosition.x + xDist, startPosition.y + i * -distanceY, 0);

                    Gizmos.DrawWireCube(pos, new Vector3(2, 1, 2));
                }
            }
        }
    }

}
