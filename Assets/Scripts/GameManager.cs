/* < 8 - 9 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for managing the game, shooting the ball, managing player lifes
 */
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //the spawnable ball prefab
    [SerializeField] GameObject ballPrefab;
    [SerializeField] TextMeshProUGUI lifesText;

    //keep track of spawned balls
    List<GameObject> ballList = new List<GameObject>();
    //keep track of spawned bricks
    List<GameObject> brickList = new List<GameObject>();

    int lifes;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        //if player presses space
        if (Input.GetKeyDown(KeyCode.Space) && ballList.Count > 0)
        {
            //if ball isn't started
            if (ballList[0] != null && !ballList[0].GetComponent<Ball>().BallStarted())
            {
                StartBall();
            }
        }
    }

    #region ballShooting
    //creating the ball
    private void CreateBall()
    {
        //create GO and set position and parent to follow player
        GameObject newBall = Instantiate(ballPrefab);
        newBall.transform.position = Paddle.Instance.gameObject.transform.position + new Vector3(0, 1.5f, 0);
        newBall.transform.SetParent(Paddle.Instance.gameObject.transform);
        newBall.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //add ball to ball list
        ballList.Add(newBall);
    }
    //shoot the ball
    private void StartBall()
    {
        ballList[0].GetComponent<Ball>().StartBall();
    }
    #endregion

    #region bricks
    //when bricks are spawned
    public void AddBrick(GameObject brick)
    {
        brickList.Add(brick);
    }
    //when bricks are despawned
    public void RemoveBrick(GameObject brick)
    {
        brickList.Remove(brick);
        //check if game should end
        if (brickList.Count == 0)
        {
            Debug.Log("You Won!");
        }
    }
    #endregion

    #region lifes
    //remove a life from player when no more balls are in the scene
    private void RemoveLife()
    {
        //take away a life
        lifes--;
        //Update lifes text
        UpdateLifesUI();

        //check if player lost
        if (lifes <= 0)
        {
            Debug.Log("You Lost");
            return;
        }

        //reset the paddle and get a new ball
        CreateBall();
        Paddle.Instance.ResetPaddlePosition();
    }

    //when a ball is lost into dead zone
    public void LostBall(GameObject ball)
    {
        //remove ball from tracking list and destroy the GO
        ballList.Remove(ball);
        Destroy(ball);

        //if no more active balls remove a life
        if (ballList.Count == 0)
        {
            RemoveLife();
        }
    }

    //update the life text ui
    private void UpdateLifesUI()
    {
        lifesText.text = "Lifes: " + lifes.ToString("D2");
    }
    #endregion

    //when game is started
    private void ResetGame()
    {
        lifes = 3;
        CreateBall();
        UpdateLifesUI();
    }
}
