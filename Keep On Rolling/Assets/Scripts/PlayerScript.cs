using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Script references
    public MainScript mainScriptRef;
    public Camera gameCamera;

    //General movement variables
    public Vector3 velocity;
    public bool wallRiding = false;

    //Input movement variables
    Vector3 movementVelocity; //WASD
    //float touchScreenMovementInput; //Touch
    
    // Update is called once per frame
    void Update() {
        gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z + -2.8f);

        // Shows the velocity in debug menu
        velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
    }

    public void Movement() {
        //WASD input
        movementVelocity = new Vector3(Input.GetAxis("Horizontal") * mainScriptRef.movementSpeed * Time.deltaTime, -1 * Time.deltaTime, 0);

        //Touch input
        /*
        if ((touchScreenMovementInput < -1) && (touchScreenMovementInput > 1)) {
            if (touchScreenMovementInput > 0) {
                touchScreenMovementInput = touchScreenMovementInput - mainScriptRef.touchSlowDownMovementSpeed;
            } else if (touchScreenMovementInput < 0) {
                touchScreenMovementInput = touchScreenMovementInput + mainScriptRef.touchSlowDownMovementSpeed;
            }
        } else {
            touchScreenMovementInput = 0;
        }
        if (mainScriptRef.touchScreenModeEnabled == true) {
            movementVelocity = new Vector3(movementVelocity.x + touchScreenMovementInput, movementVelocity.y, movementVelocity.z);
        }*/

        //Change player velocity
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x + movementVelocity.x, -mainScriptRef.maxMovementSpeed, mainScriptRef.maxMovementSpeed), GetComponent<Rigidbody>().velocity.y + movementVelocity.y, Mathf.Clamp(GetComponent<Rigidbody>().velocity.z + (mainScriptRef.rollSpeed * mainScriptRef.platformSpeed), 0, mainScriptRef.maxRollSpeed * mainScriptRef.platformSpeed));
        //player.GetComponent<Rigidbody>().MovePosition(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + new Vector3(movementVelocity.x,0,0));
        
        //Slow the player's movement to a stop if it's too slow
        if ((GetComponent<Rigidbody>().velocity.x < -1) || (GetComponent<Rigidbody>().velocity.x > 1)) {
            if (GetComponent<Rigidbody>().velocity.x > 0) {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x - mainScriptRef.slowDownMovementSpeed * Time.deltaTime, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            } else if (GetComponent<Rigidbody>().velocity.x < 0) {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x + mainScriptRef.slowDownMovementSpeed * Time.deltaTime, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            }
        }

        //Allows wall riding when touching a wall
        if (wallRiding == true) {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
    }

#pragma warning disable IDE0060 // Remove unused parameter  REMOVE WHEN TOUCH INPUT IS ADDED
    public void TouchScreenModeInput(string direction) {
        /*if (direction == "left") {
            touchScreenMovementInput = -mainScriptRef.movementSpeed * Time.deltaTime;
        } else if (direction == "right") {
            touchScreenMovementInput = mainScriptRef.movementSpeed * Time.deltaTime;
        }*/
    }
#pragma warning restore IDE0060 // Remove unused parameter

    void OnCollisionEnter(Collision collision) {
        mainScriptRef.CollisionEnter(collision);
        //Wall riding start collision check
        if (collision.gameObject.CompareTag(mainScriptRef.wallRideTag)) {
            wallRiding = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        //Wall riding end collision check
        if (collision.gameObject.CompareTag(mainScriptRef.wallRideTag)) {
            wallRiding = false;
        }
    }

    //Trigger enter used for the main script
    void OnTriggerEnter(Collider other) {
        mainScriptRef.TriggerEnter(other);
    }
}
