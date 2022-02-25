using UnityEngine;

public class ChaseCam : MonoBehaviour {
    public float speed = 5;
    public Vector2 finalSpot;

    private Vector2 origPos;
    private bool moving = false;


    // Start is called before the first frame update
    void Start() {
        origPos = transform.position;
    }

    void FixedUpdate() {
        if (moving) {
            float step = speed * Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(transform.position, finalSpot, step);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            moving = true;
        }
    }

    public bool ResetChase() {
        if (moving) transform.position = origPos;

        return moving;
    }
}
