using UnityEngine;

public class ChaseCam : MonoBehaviour {
    public float speed = 5;
    public Vector2 finalSpot;
    public GameObject chasers;

    private Vector2 origPos;
    private bool moving = false;


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
            StartChase();
        }
    }

    public bool ResetChase() {
        if (moving) {
            transform.position = origPos;
            chasers.SetActive(false);
            chasers.SetActive(true);
        }

        return moving;
    }

    private void StartChase() {
        moving = true;
        chasers.SetActive(true);
    }
}
