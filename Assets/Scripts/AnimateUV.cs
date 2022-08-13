/* < 8 - 13 -  2022 >
 * Hussien Kenaan
 * 
 * This script is Animating the background
 */
using UnityEngine;
using System.Collections;

public class AnimateUV : MonoBehaviour
{
    //private variables to use
    private bool Y = true;
    private float offset;
    private Material mat;

    //set the speed for scrolling
    [SerializeField] private float scrollSpeed = 0.5f;

    private void Start()
    {
        //get the material reference and start the scrolling
        mat = GetComponent<MeshRenderer>().material;
        StartCoroutine(ScrollUV());
    }

    private IEnumerator ScrollUV()
    {
        while (true)
        {
            //calculate offset while always keeping it less than 1
            offset = Time.time * scrollSpeed % 1;

            //if should move
            if (Y)
            {
                //apply the offset to visualize moving effect
                mat.mainTextureOffset = new Vector2(0, offset);
            }
            yield return null;
        }
    }
}
