using UnityEngine;

public class ParticlesController : MonoBehaviour {
    //This script is used on any object with particles and will disable
    //the particles if the particle settings is disable

    public MainScript mainScriptRef;

    // Start is called before the first frame update
    void Start() {
        mainScriptRef = GameObject.FindGameObjectWithTag("Player").GetComponent<MainScript>();
    }

    // Update is called once per frame
    void Update() {
        if (mainScriptRef.particlesEnabled == true) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
