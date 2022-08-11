/* < 8 - 10 - 2022 >
 * Hussien Kenaan
 * 
 * This is an editor script to allow realtime editing in inspector and make the level editor look good
 */
using UnityEditor;
using UnityEngine;

//set this custome editor script to only look for SCR_Level scripts
[CustomEditor(typeof(SCR_Level))]
public class SCR_LevelEditor : Editor
{
    //size of cells containing the data
    private int cellWidth = 32;
    private int cellHeight = 16;
    int margin = 5;
    //rect to contain the data
    Rect rect;

    //the level scriptable object
    SerializedObject level;
    //store data gotten from level
    SerializedProperty gridSize;
    SerializedProperty rows;

    //local version of level variables
    private Vector2Int newGRidSize;
    private Vector2 cellSize;

    private bool wrongSize;
    private bool gridChanged;

    private void OnEnable()
    {
        //get the level looking at
        level = new SerializedObject(target);
        //get the data fro current level
        gridSize = level.FindProperty("gridSize");
        rows = level.FindProperty("rows");

        //transform gotten gridsize data into vector
        newGRidSize = gridSize.vector2IntValue;
        cellSize = new Vector2(cellWidth, cellHeight);
    }

    //when modfiy scriptable object
    public override void OnInspectorGUI()
    {
        //IMPORTANT
        level.Update();

        //This renders the SCR_level gridSize and applies changes to it
        //start a layout for grid size
        EditorGUILayout.BeginHorizontal();
        {
            //check if code has changed anything
            EditorGUI.BeginChangeCheck();
            //set a parameter to input the grid size
            newGRidSize = EditorGUILayout.Vector2IntField("Grid Size", newGRidSize);

            //check if wrong size and if updated
            wrongSize = (newGRidSize.x <= 0 || newGRidSize.y <= 0);
            gridChanged = (newGRidSize != gridSize.vector2IntValue);

            //if the grid has been updated, create a button to apply changes
            GUI.enabled = gridChanged && !wrongSize;
            //create a button and get click function
            if (GUILayout.Button("Apply", EditorStyles.miniButton))
            {
                //Create a new cell grid
                CreateGridCells(newGRidSize);
            }

            //set the rest to show
            GUI.enabled = true;
        }
        //end layout for grid size
        EditorGUILayout.EndHorizontal();

        //say there is an error if using negative numbers
        if (wrongSize)
        {
            EditorGUILayout.HelpBox("Values have to be heigher than 0", MessageType.Error);
        }
        EditorGUILayout.Space();
        //Get correct spacing
        if (Event.current.type == EventType.Repaint)
        {
            rect = GUILayoutUtility.GetLastRect();
        }

        DisplayGrid(rect);

        //IMPORTANT
        level.ApplyModifiedProperties();

    }

    private void CreateGridCells(Vector2Int newSize)
    {
        //reset the array
        rows.ClearArray();

        for (int i = 0; i < newSize.y; i++)
        {
            //create a place at index
            rows.InsertArrayElementAtIndex(i);
            //gets number of columnds in this array
            SerializedProperty col = GetRowAt(i);
            //reset the coll
            col.arraySize = 0;
            //loop through the coll and and apply there size
            for (int j = 0; j < newSize.x; j++)
            {
                col.InsertArrayElementAtIndex(j);
            }
        }

        gridSize.vector2IntValue = newGRidSize;
    }

    private void DisplayGrid(Rect start)
    {
        //set the position and start as the precreated rect
        Rect cellPos = start;
        //space a bit
        cellPos.y += 10;
        cellPos.size = cellSize;

        float startX = cellPos.x;
        //loop through y axis
        for (int i = 0; i < gridSize.vector2IntValue.y; i++)
        {
            //get the collumn
            SerializedProperty col = GetRowAt(i);
            cellPos.x = startX;
            //loop through the column and cells
            for (int j = 0; j < gridSize.vector2IntValue.x; j++)
            {
                EditorGUI.PropertyField(cellPos, col.GetArrayElementAtIndex(j), GUIContent.none);
                cellPos.x += cellSize.x + margin;
            }

            cellPos.y += cellSize.y + margin;

            //just a small space

            GUILayout.Space(cellSize.y + margin);
        }
        

    }

    private SerializedProperty GetRowAt(int index)
    {
        return rows.GetArrayElementAtIndex(index).FindPropertyRelative("columns");
    }
}
