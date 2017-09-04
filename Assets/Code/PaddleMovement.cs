using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float playerSpeed;
    public float rotateSpeed;

    private bool canMove;

    void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Input.GetAxisRaw("Vertical") * playerSpeed);
        }

        if(Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        }



        if (Input.GetAxisRaw("Rotate") > 0.5f || Input.GetAxisRaw("Rotate") < -0.5f)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Input.GetAxisRaw("Rotate") * Time.deltaTime);
        }

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        //}
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "prop_powerCube")
        {
            Destroy(col.gameObject);
        }
    }
}
