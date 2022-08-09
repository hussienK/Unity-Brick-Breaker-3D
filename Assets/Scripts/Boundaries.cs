/* < 8 - 9 - 2022 >
 * Hussien Kenaan
 * 
 * This scipt Positions and scales bounderies with respect to camera so that they are always in correct position
 */
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    //the main walls to resize
    [SerializeField] private GameObject leftWall, rightWall, topWall, bottomWall;

    //the corner peices
    [Header("Corners")]
    [SerializeField] private GameObject leftCorner;
    [SerializeField] private GameObject rightCorner;

    //vars to be used
    private float distanceToCamera;
    private Vector3 screenBoundries;
    private Vector3 screenPoint;

    private void Start()
    {
        //camera is always in center so position to camera is position to z (depth)
        distanceToCamera = Camera.main.transform.position.z;

        CalculateBoundaries();
    }

    private void CalculateBoundaries()
    {
        //get the height by using math on FOV
        float frustrumeHeight = 2.0f * distanceToCamera * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //get width by using camera aspect ratio
        float frustrumeWidth = frustrumeHeight * Camera.main.aspect;

        //create screen boundaries and screen point for easier access later
        screenBoundries = new Vector3(frustrumeWidth, frustrumeHeight, 0);
        screenPoint = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

        //get left border position and scale
        Vector3 leftPoint = new Vector3(screenPoint.x - Mathf.Abs(frustrumeWidth / 2), screenPoint.y, 0);
        leftWall.transform.position = leftPoint;
        leftWall.transform.localScale = new Vector3(1, Mathf.Abs(screenBoundries.y), 1);
        //get right border position and scale
        Vector3 rightPoint = new Vector3(screenPoint.x + Mathf.Abs(frustrumeWidth / 2), screenPoint.y, 0);
        rightWall.transform.position = rightPoint;
        rightWall.transform.localScale = new Vector3(1, Mathf.Abs(screenBoundries.y), 1);
        //get top border position and scale
        Vector3 topPoint = new Vector3(screenPoint.x, screenPoint.y + Mathf.Abs(frustrumeHeight / 2), 0);
        topWall.transform.position = topPoint;
        topWall.transform.localScale = new Vector3(Mathf.Abs(screenBoundries.x), 1, 1);
        //get bottom border position and scale
        Vector3 bottomPoint = new Vector3(screenPoint.x, screenPoint.y - Mathf.Abs(frustrumeHeight / 2), 0);
        bottomWall.transform.position = bottomPoint;
        bottomWall.transform.localScale = new Vector3(Mathf.Abs(screenBoundries.x), 1, 1);

        //get corner peices positions
        rightCorner.transform.position = new Vector3(rightPoint.x, topPoint.y, 0);
        leftCorner.transform.position = new Vector3(leftPoint.x, topPoint.y, 0);
    }
}
