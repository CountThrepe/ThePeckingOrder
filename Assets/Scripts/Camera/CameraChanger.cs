using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour {

    [SerializeField]
    private CinemachineVirtualCamera followCam, roomCam;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            roomCam.Priority = followCam.Priority + 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            roomCam.Priority = followCam.Priority - 1;
        }
    }
}
