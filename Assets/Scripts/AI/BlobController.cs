using UnityEngine;

public class BlobController : MonoBehaviour {

    public float avgVel = 3f;
    public float range = 20;
    public float patience = 5;
    public float waitRange = 4;
    public Transform player = null;
    public float lowFreq = 1000;
    public float highFreq = 22000;

    public ChaseCam chase;

    [HideInInspector]
    public float velMult = 1;
    private Vector2 home;
    private Vector2? area = null;
    private Vector2 wanderPoint;
    private bool targeting = false;
    private bool waiting = false;
    private float lastSpotted;
    private AudioLowPassFilter filter;


    // Start is called before the first frame update
    void Start() {
        home = transform.position;
        wanderPoint = home + (Random.insideUnitCircle * range);

        filter = GetComponent<AudioLowPassFilter>();
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
        UpdateWanderPoint(true);
        filter.cutoffFrequency = highFreq;
    }

    public void OnRiftClose() {
        area = null;
        targeting = false;
        if (waiting) lastSpotted = Time.time;
        filter.cutoffFrequency = lowFreq;
    }

    public void OnPlayerEnterRift() {
        targeting = area.HasValue;
    }

    public void OnPlayerExitRift() {
        targeting = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(targeting && other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerMovement>().Die();
            TeleportToEdge();
        }
    }

    private void MoveTowards(Vector2 point) {
        if (Vector2.Distance(transform.position, point) < waitRange) waiting |= true;

        float step = avgVel * velMult * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, point, step);
    }

    private void Wander(bool inRift = false) {
        if ((Vector2) transform.position == wanderPoint) UpdateWanderPoint(inRift);

        float step = avgVel * velMult * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, wanderPoint, step);
    }

    private void UpdateWanderPoint(bool inRift) {
        Vector2 center = inRift && area.HasValue ? area.Value : home;
        float radius = inRift ? waitRange : range;
        float avoidRadius = inRift ? 0 : waitRange * 1.5f;

        Vector2 point = center + (Random.insideUnitCircle * radius);
        // while (Vector2.Distance(point, player.position) < avoidRadius) {
        //     point = home + (Random.insideUnitCircle * range);
        // }

        wanderPoint = point;
    }

    private void TeleportToEdge() {
        transform.position = Random.insideUnitCircle.normalized * range;
    }
}
