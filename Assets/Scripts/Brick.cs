/* < 8 - 9 - 2022 >
 * Hussien Kenaan
 *
 * This script is for managing bricks health and calling functions when brick is destroyed
 */
using UnityEngine;

public class Brick : MonoBehaviour
{
    //my health, each hit takes away one health
    public int health = 1;
    //score given to player when destroyed
    public int score = 50;

    [SerializeField] private GameObject explostionEffect;
    private void Start()
    {
        //add brick to gameManager
        GameManager.Instance.AddBrick(this.gameObject);
    }

    public void TakeDamage()
    {
        //manage health
        health--;

        if (health <= 0)
        {
            Instantiate(explostionEffect, transform.position, Quaternion.identity);
            GameManager.Instance.RemoveBrick(this.gameObject);
            ScoreManager.Instance.AddScore(score);
            Destroy(gameObject);
        }
    }
}
