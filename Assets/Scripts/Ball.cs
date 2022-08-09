/* < 8 - 9 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for shooting the ball and managing its state
 */
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    public static float inititalForce = 600f;

    private bool ballStarted;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //when hits something
    private void OnCollisionEnter(Collision collision)
    {
        //check if brick exists
        Brick brick = collision.gameObject.GetComponent<Brick>();
        if (brick != null)
        {
            //damage the brick
            brick.TakeDamage();
        }
    }

    //shoot the ball
    public void StartBall()
    {
        //if not already shot
        if (!ballStarted)
        {
            //allow to move
            rb.isKinematic = false;

            //get paddle movement direction and shoot in directio based on it
            float hInput = Input.GetAxis("Horizontal");
            if (hInput > 0)
            {
                rb.AddForce(new Vector3(inititalForce, inititalForce, 0));
            }
            else if (hInput < 0)
            {
                rb.AddForce(new Vector3(-inititalForce, inititalForce, 0));
            }
            else
            {
                rb.AddForce(new Vector3(0, inititalForce, 0));
            }

            //set parent to null to allow movement
            ballStarted = true;
            transform.SetParent(transform.parent.parent);
        }
    }
    //to check ball condition
    public bool BallStarted()
    {
        return ballStarted;
    }
}
