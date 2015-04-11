using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    // get the speed and acceleration of object
    public float maxSpeed = 10;
    public float acceleration = 3;

    // grab Object's positions.
    public float posX;
    public float posY;

	// Use this for initialization
	void Start () {
	       
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement()
    {
        posX = Input.GetAxis("Horizontal");
        posY = Input.GetAxis("Vertical");

        rigidbody2D.velocity = new Vector2(posX * maxSpeed, posY * maxSpeed);

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, maxSpeed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.AddForce(new Vector2(-maxSpeed, rigidbody2D.velocity.y));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, -maxSpeed));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.AddForce(new Vector2(maxSpeed, rigidbody2D.velocity.y));
        }
    }
}
