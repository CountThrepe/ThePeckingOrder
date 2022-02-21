using UnityEngine;

public class RiftManager : MonoBehaviour {
    public GameObject environment;
    public AudioSource music, fx;

    [HideInInspector]
    public float radius;
    private float lastRadius;

    private Animator animator;
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
    }

    public void OpenRift(Vector2 origin) {
        SetRiftOrigin(origin);
        animator.SetBool("Open", true);
    }

    public void CloseRift() {
        animator.SetBool("Open", false);
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
