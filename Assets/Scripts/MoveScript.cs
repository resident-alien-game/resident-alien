// This is a simple script for moving a character around the screen and jumping.
// To use it attach this script to the your game character.
// Make sure to also add a Rigidbody and a Character Controller to your game character.
// You can find these in the COMPONENTS menu in Physics.

// The character can move forward/backward with W/S key and turn with A/D keys.
// If there are other objects in the scene with rigidbody and collider components then
// this code will also allow the character to push those objects.
// This code also incorporates the ability to Jump (SPACE BAR)



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
        public float walkSpeed = 5; // How fast you walk
        public float jumpSpeed = 5; // How fast you jump
        public float forceMagnitude=10; // Used when pushing against an object
        public int allowableJumps = 1; // Determine how many jumps allowed at a time
        private int jumpCount=0;
        private CharacterController controller;
        private float ySpeed;   // Stores the y speed for jumping and falling

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 inputMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move;

        // TransformDirection transforms the input movement vector into a vector
        // oriented in the direction of the object is facing.
        // In otherwords, the object moves forward in whichever direction it is facing.
        move = gameObject.transform.TransformDirection(inputMove) * walkSpeed;

        // Calculate y speed based on Unity's value of gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // If SPACEbar pressed, initiate a jump depending on how many jumps you
        // are allowed at a time.
        if (jumpCount < allowableJumps){
            if (Input.GetButtonDown("Jump")){
                // Jumping now
                ySpeed = jumpSpeed;

                // Keep track of how many consecutive jumps have occurred
                jumpCount++;
            }          
        } else if (controller.isGrounded){
            jumpCount=0;
        }

        // Insert ySpeed into the y component of move vector
        move.y = ySpeed;
        
        // Move the character controller
        controller.Move(move * Time.deltaTime);
    }
    // If you collide with something, push it with a force.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Extract the rigidbody of the object you collided with
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb !=null) {

            // calculate the force direction as the difference in position between you
            // and the object you collided with
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;

            // We assume there is no vertical force applied
            forceDirection.y=0;
            forceDirection.Normalize(); // Make the forceDirection a vector with values from 0 to 1

            // Apply the force
            rb.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
}