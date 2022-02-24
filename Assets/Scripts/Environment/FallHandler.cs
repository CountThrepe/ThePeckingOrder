using UnityEngine;

public class FallHandler : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerMovement>().Respawn();
        }
    }
}
