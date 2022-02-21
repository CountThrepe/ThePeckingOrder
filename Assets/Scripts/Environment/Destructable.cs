using UnityEngine;

public class Destructable : MonoBehaviour {
    public void Destroy() {
        Destroy(gameObject);
    }
}
