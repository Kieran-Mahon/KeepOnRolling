using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public MainScript mainScriptRef;
    public Camera gameCamera;

    public Vector3 velocity;
    public bool wallRiding = false;

    Vector3 movementVelocity;
    float touchScreenMovementInput;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z + -2.8f);

        // Shows the velocity in debug menu
        velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
    }

    public void Movement() {
        movementVelocity = new Vector3(Input.GetAxis("Horizontal") * mainScriptRef.movementSpeed * Time.deltaTime, -1 * Time.deltaTime, 0);
        if (mainScriptRef.touchScreenModeEnabled == true) {
            //movementVelocity = new Vector3(movementVelocity.x + touchScreenMovementInput, movementVelocity.y, movementVelocity.z);
        }
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x + movementVelocity.x, -mainScriptRef.maxMovementSpeed, mainScriptRef.maxMovementSpeed), GetComponent<Rigidbody>().velocity.y + movementVelocity.y, Mathf.Clamp(GetComponent<Rigidbody>().velocity.z + (mainScriptRef.rollSpeed * mainScriptRef.platformSpeed), 0, mainScriptRef.maxRollSpeed * mainScriptRef.platformSpeed));
        //player.GetComponent<Rigidbody>().MovePosition(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + new Vector3(movementVelocity.x,0,0));
        if ((GetComponent<Rigidbody>().velocity.x < -1) || (GetComponent<Rigidbody>().velocity.x > 1)) {
            if (GetComponent<Rigidbody>().velocity.x > 0) {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x - mainScriptRef.slowDownMovementSpeed * Time.deltaTime, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            } else if (GetComponent<Rigidbody>().velocity.x < 0) {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x + mainScriptRef.slowDownMovementSpeed * Time.deltaTime, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            }
        }

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
        */

        if (wallRiding == true) {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
    }

    public void TouchScreenModeInput(string direction) {
        if (direction == "left") {
            touchScreenMovementInput = -mainScriptRef.movementSpeed * Time.deltaTime;
        } else if (direction == "right") {
            touchScreenMovementInput = mainScriptRef.movementSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision) {
        mainScriptRef.CollisionEnter(collision);
        if (collision.gameObject.tag == mainScriptRef.wallRideTag) {
            wallRiding = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == mainScriptRef.wallRideTag) {
            wallRiding = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        mainScriptRef.TriggerEnter(other);
    }
}
