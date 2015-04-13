using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    // get the speed and acceleration of object
    public float maxSpeed = 10;
    public float speed = 5.0f;

    // grab Object's positions.
    public float posX;
    public float posY;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    // Handles all of the movement. (Called in Update)
    private void Movement()
    {
        // get position.
        posX = Input.GetAxis("Horizontal");
        posY = Input.GetAxis("Vertical");

        // Set the vector
        Vector2 move = (new Vector2(posX, posY).normalized) * speed;
        //Vector2 slow = (new Vector2(posX, posY).normalized) * -speed;

        // Move the rigidbody, using the vector
        rigidbody2D.AddForce(move, ForceMode2D.Impulse);

        //if (move == Vector2.zero)
        //{
        //    rigidbody2D.velocity = slow;
        //}
        // if the speed of the object goes over the maxspeed, set the speed to maxspeed.
        if (Mathf.Abs(rigidbody2D.velocity.x) >= maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(maxSpeed * rigidbody.velocity.x, rigidbody.velocity.y);
        }
        if (Mathf.Abs(rigidbody2D.velocity.y) >= maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody.velocity.x, maxSpeed * rigidbody.velocity.y);
        }
    }
}