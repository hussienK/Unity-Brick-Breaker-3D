/* < 8 - 13 - 2022 >
 * Hussien Kenaan
 *
 * This script put on objects to destroy over time
 */
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float destroyTime = 1;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
