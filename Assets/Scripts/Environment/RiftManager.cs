using UnityEngine;

public class RiftManager : MonoBehaviour {
    public GameObject environment;
    public AudioSource music, fx;

    [HideInInspector]
    public float radius;
    private float lastRadius;
    public Transform follow;

    private Animator animator;
    private bool following = false;
    private Vector2 lastPos;
    private RiftParameters[] shaders;

    void Start() {
        animator = GetComponent<Animator>();

        shaders = environment.GetComponentsInChildren<RiftParameters>();
        Debug.Log(shaders.Length);
        lastRadius = radius = 0;

        SetRiftRadius(0);
    }

    void Update() {
        if (lastRadius != radius) {
            SetRiftRadius(radius);
            lastRadius = radius;
        }

        if (following && lastPos != (Vector2) follow.position) {
            SetRiftOrigin(follow.position);
            lastPos = (Vector2) follow.position;
        }
    }

    public void SetFollowing(int val) {
        following = val > 0;
        if (following) {
            SetRiftOrigin(follow.position);
            lastPos = (Vector2) follow.position;
        }
    }


    public void Follow() {
        if (!following) {
            animator.SetInteger("Open", 2);
        }
    }

    public void StopFollowing() {
        if (following) {
            CloseRift();
        }
    }

    public void OpenRift(Vector2 origin) {
        SetRiftOrigin(origin);
        animator.SetInteger("Open", 3);
    }

    public void CloseRift() {
        if(animator.GetInteger("Open") != 1) {
            animator.Play("RiftClose", 0, (6 - radius) / 6);
            animator.SetInteger("Open", 1);
        }
    }

    public void StartRiftSounds() {
        fx.Play();
    }

    public void StopRiftSounds() {
        fx.Pause();
    }

    private void SetRiftRadius(float rad) {
        foreach (RiftParameters shader in shaders) {
            shader.UpdateRadius(rad);
        }
    }

    private void SetRiftOrigin(Vector2 origin) {
        foreach (RiftParameters shader in shaders) {
            shader.UpdateOrigin(origin);
        }
    }
}
