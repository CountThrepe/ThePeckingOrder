using UnityEngine;

public class BlobController : MonoBehaviour {

    public float avgVel = 3f;
    public float range = 20;
    public float patience = 5;
    public float waitRange = 4;
    public Transform player = null;

    [HideInInspector]
    public float velMult = 1;
    private Vector2 home;
    private Vector2? area = null;
    private Vector2 wanderPoint;
    private bool targeting = false;
    private bool waiting = false;
    private float lastSpotted;


    // Start is called before the first frame update
    void Start() {
        home = transform.position;
        wanderPoint = home + (Random.insideUnitCircle * range);
    }

    void FixedUpdate() {
        if (targeting) MoveTowards(player.position);
        else if (area.HasValue) Wander(true);
        else if (waiting) waiting = lastSpotted + patience > Time.time;
        else Wander();
    }

    public void OnRiftOpen(Vector2 point) {
        if (Vector2.Distance(home, point) < range) area = point;
        waiting = false;
    }

    public void OnRiftClose() {
        area = null;
        targeting = false;
        if (waiting) lastSpotted = Time.time;
    }

    public void OnPlayerEnterRift() {
        targeting = area.HasValue;
    }

    public void OnPlayerExitRift() {
        targeting = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(targeting && other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerMovement>().Respawn();
        }
    }

    private void MoveTowards(Vector2 point) {
        if (Vector2.Distance(transform.position, point) < waitRange) waiting |= true;

        float step = avgVel * velMult * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, point, step);
    }

    private void Wander(bool inRift = false) {
        Vector2 center = inRift ? area.Value : home;
        float radius = inRift ? waitRange : range;

        if ((Vector2) transform.position == wanderPoint || Vector2.Distance(wanderPoint, center) > radius)
            wanderPoint = home + (Random.insideUnitCircle * range);

        float step = avgVel * velMult * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, wanderPoint, step);
    }
}
