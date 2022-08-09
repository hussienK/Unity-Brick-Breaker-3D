/* < 8 - 9 - 2022 >
 * Hussien Kenaan
 * 
 * This script is responsible for moving paddles, handling ball collisions with paddle, resizing the paddle
 */
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //create a refernce accessable everywhere
    public static Paddle Instance { get; private set; }

    //private components
    private Rigidbody rb;
    private BoxCollider col;

    //changable variables
    private float speed = 10;
    [SerializeField] private float newSize = 2f;

    [Header("Paddle Parts")]
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject leftCap, rightCap;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //assign components
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();

        //set the initial size
        Resize(newSize);
    }

    private void FixedUpdate()
    {
        //get input
        float hInput = Input.GetAxisRaw("Horizontal");

        //reset speed when not moving
        if ((int)hInput == 0 && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }

        //move the paddle based on speed and input
        rb.MovePosition(transform.position + new Vector3(hInput, 0, 0).normalized * speed * Time.fixedDeltaTime);
    }

    //when hits something
    private void OnCollisionEnter(Collision collision)
    {
        //if hits the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            //get the rigidbody and speed of the ball
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            float vel = Ball.inititalForce;
            //get point of contact between ball and paddle
            Vector3 hitPoint = collision.contacts[0].point;
            //get where on the paddle the ball hit
            float difference = transform.position.x - hitPoint.x;

            //apply force based on where on the paddle the ball hits
            if (hitPoint.x < transform.position.x)
            {
                ballRb.AddForce(new Vector3(-(Mathf.Abs(difference * 200)), vel, 0));
            }
            else
            {
                ballRb.AddForce(new Vector3((Mathf.Abs(difference * 200)), vel, 0));
            }
        }
    }

    //resize the paddle into a give scale
    private void Resize(float xScale)
    {
        //get old center peice scale, modify it by xScale and apply it again
        Vector3 initScale = center.transform.localScale;
        initScale.x = xScale;
        center.transform.localScale = initScale;

        //calculate where left pos should be based on new scale and update it
        Vector3 leftCapPos = new Vector3(center.transform.position.x - (xScale / 2), leftCap.transform.position.y, leftCap.transform.position.z);
        leftCap.transform.position = leftCapPos;
        //calculate where right pos should be based on new scale and update it
        Vector3 rightCapPos = new Vector3(center.transform.position.x + (xScale / 2), rightCap.transform.position.y, rightCap.transform.position.z);
        rightCap.transform.position = rightCapPos;

        //get old collider scale
        Vector3 colScale = initScale;
        //add padding because of caps
        colScale.x += 0.6f * 2;
        //apply new size
        col.size = colScale;
    }

    //reset the paddle into center and give original size
    public void ResetPaddlePosition()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, 0);
        Resize(newSize);
    }
}