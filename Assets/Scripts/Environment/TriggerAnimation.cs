using UnityEngine;

public class TriggerAnimation : MonoBehaviour {
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D() {
        anim.SetTrigger("Animate");
    }
}
