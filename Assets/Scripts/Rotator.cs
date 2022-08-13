/* < 8 - 7 - 2022 >
 * Hussien kenaan
 * 
 * This script is for rotating the preview block
 */
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
