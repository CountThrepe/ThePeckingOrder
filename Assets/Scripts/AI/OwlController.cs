using UnityEngine;

public class OwlController : MonoBehaviour {
    public Transform player;
    public float rotateSpeed = 3;
    public Transform mirror;
    public float biteStartDist = 20;

    public float biteBreak = 3;

    private bool locked;
    private float lastBite;

    // Start is called before the first frame update
    void Start() {
        lastBite = -1;
    }

    void OnEnable() {
        Debug.Log("Test");
        locked = false;
        lastBite = -1;
    }

    // Update is called once per frame
    void Update() {
        if (lastBite == -1 && Vector2.Distance(player.position, mirror.position) < biteStartDist) {
            lastBite = Time.time - biteBreak;
            Debug.Log(lastBite);
        }

        if (!locked) {
            RotateTowards();
            if (lastBite > 0 && Time.time > lastBite + biteBreak) Bite();
        }
    }

    public void Bite() {
        GetComponent<Animator>().SetTrigger("Bite");
        locked = true;
        lastBite = Time.time;
    }

    public void ResetBite() {
        GetComponent<Animator>().ResetTrigger("Bite");
        locked = false;
    }

    private void RotateTowards() {
        Vector3 vectorToTarget = player.position - transform.position;
        Debug.Log(vectorToTarget.x);
        if(vectorToTarget.x < -4) vectorToTarget = Vector3.right;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
    }
}
