using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour {

    public float inpX, inpY;
    public Camera cam;
    public Rigidbody rb;
    public float friction = 1;
    public float frictionMovingMultiplier = 0.5f; //friction applied harder when we aren't trying to move
    public float acceleration = 7;
    public float maxSpeed = 7;

    void Update() {
        inpX = Input.GetAxisRaw("Horizontal"); //get the axis movement (left/right and up/down)
        inpY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        Vector3 frictionVec = new Vector3(-rb.velocity.x, 0, -rb.velocity.z) * friction * ((Mathf.Abs(inpX) > 0.1 || Mathf.Abs(inpY) > 0.1) ? frictionMovingMultiplier : 1); //attach friction depending on current velocity

        Vector3 leftright = HeadRotation() * Vector3.right * inpX * acceleration;
        Vector3 forwardback = HeadRotation() * Vector3.forward * inpY * acceleration;
        Vector3 newVelocity = (forwardback + leftright).normalized; //resultant velocity from the inputs
        rb.velocity = Vector3.ClampMagnitude(rb.velocity + newVelocity + frictionVec, maxSpeed);
    }

    Quaternion HeadRotation() {
        return Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up);
    }
}
