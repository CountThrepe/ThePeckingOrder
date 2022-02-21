using UnityEngine;

public class RiftParameters : MonoBehaviour {
    public Texture2D mainTexture;
    public Color mainColor = Color.white;
    public Texture2D alternateTexture;
    public Color alternateColor = Color.white;

    public float speed = 0.5f;
    public float scale = 1.5f;

    private MaterialPropertyBlock mpb;

    public void Start() {
        UpdateParameters();
    }

    public void OnValidate() {
        UpdateParameters();
    }

    public void UpdateRadius(float radius) {
        if(mpb == null) mpb = new MaterialPropertyBlock();

        mpb.SetFloat("_Rad", radius);

        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }

    public void UpdateOrigin(Vector3 origin) {
        if(mpb == null) mpb = new MaterialPropertyBlock();

        mpb.SetVector("_Origin", origin);

        GetComponent<Renderer>().SetPropertyBlock(mpb);

    }

    private void UpdateParameters() {
        if(mpb == null) mpb = new MaterialPropertyBlock();

        mpb.SetTexture("_MainTex", mainTexture);
        mpb.SetColor("_MainColor", mainColor);
        mpb.SetTexture("_AltTex", alternateTexture);
        mpb.SetColor("_AltColor", alternateColor);

        mpb.SetFloat("_Speed", speed);
        mpb.SetFloat("_Scale", scale);

        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }
}
