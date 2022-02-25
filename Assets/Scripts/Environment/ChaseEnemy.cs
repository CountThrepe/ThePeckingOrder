using UnityEngine;

public class ChaseEnemy : MonoBehaviour {
    public ChaseCam chase;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            bool chasing = chase.ResetChase();
            if (chasing) other.gameObject.GetComponent<PlayerMovement>().Respawn();
        }
    }
}
