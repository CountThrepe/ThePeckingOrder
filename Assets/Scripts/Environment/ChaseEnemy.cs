using UnityEngine;

public class ChaseEnemy : MonoBehaviour {
    public ChaseCam chase;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerMovement>().Die();
        }
    }
}
