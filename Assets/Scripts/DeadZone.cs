/* < 8 - 9 - 2022 >
 * Hussien Kenaan
 * 
 * This script is manages what happend when ball leaves play area
 */
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    //something entered dead zone
    private void OnTriggerEnter(Collider collision)
    {
        //if is ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            //lose the ball
            GameManager.Instance.LostBall(collision.gameObject);
        }
    }
}
