using UnityEngine;

public class ParticlesController : MonoBehaviour {

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
