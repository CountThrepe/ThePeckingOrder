using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour {

    public bool permanent = false;

    [SerializeField]
    private CinemachineVirtualCamera followCam, roomCam;
    [SerializeField]
    private Transform respawn;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            roomCam.Priority = followCam.Priority + 1;
            other.gameObject.GetComponent<PlayerMovement>().SetRespawn(respawn.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!permanent && other.CompareTag("Player")) {
            if (!other.GetComponent<PlayerMovement>().GetFrozen()) roomCam.Priority = followCam.Priority - 1;
        }
    }
}
