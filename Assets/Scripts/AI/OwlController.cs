using UnityEngine;

public class OwlController : MonoBehaviour {
    public Transform player;
    public float rotateSpeed = 3;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        RotateTowards();
    }

    private void RotateTowards() {
        Vector3 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
    }
}
