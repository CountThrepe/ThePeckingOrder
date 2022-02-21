using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
    private Animator anim;
    void Start() {
        anim = GetComponent<Animator>();

        float start = Random.Range(0f, 1f);
        anim.Play("LightFlicker", 0, start);
    }
}
