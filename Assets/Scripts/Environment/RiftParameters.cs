using UnityEngine;

public class RiftParameters : MonoBehaviour {
    public Texture2D mainTexture;
    public Color mainColor = Color.white;
    public Texture2D alternateTexture;
    public Color alternateColor = Color.white;

    public bool animated = false;
    public float optimizeDist = -1;
    
    private Material riftMat, regMat;
    private Texture2D lastMTex, lastATex;

    public float speed = 0.5f;
    public float scale = 1.5f;

    private MaterialPropertyBlock mpb;
    private bool optimized = false;

    public bool bug = false;

    public void Start() {
        riftMat = Resources.Load("Pulse Material", typeof(Material)) as Material;
        regMat = Resources.Load("Sprite-Lit-Default", typeof(Material)) as Material;

        lastMTex = mainTexture;
        lastATex = alternateTexture;
        UpdateParameters();
        GetComponent<Renderer>().material = riftMat;
    }

    public void OnValidate() {
        UpdateParameters();
    }

    public void Update() {
        if (optimizeDist > 0) { // If we're optimizing
            if (Vector2.Distance(LevelManager.GetInstance().GetCameraPosition(), transform.position) > optimizeDist) { // If we should optimize right now
                if (!optimized) {
                    GetComponent<Renderer>().material = regMat;
                    optimized = true;
                }
            } else { // If we should not be optimizing right now
                if (optimized) {
                    GetComponent<Renderer>().material = riftMat;
                    optimized = false;
                }
            }
        }

        if (animated) {
            bool updates = false;
            if (lastMTex != mainTexture) {
                updates = true;
                mpb.SetTexture("_MainTex", mainTexture);
                lastMTex = mainTexture;
            }

            if (lastATex != alternateTexture) {
                updates = true;
                mpb.SetTexture("_AltTex", alternateTexture);
                lastATex = alternateTexture;
            }
            
            if (updates) GetComponent<Renderer>().SetPropertyBlock(mpb);
        }
    }

    public void UpdateRadius(float radius) {
        if(mpb == null) mpb = new MaterialPropertyBlock();

        mpb.SetFloat("_Rad", radius);

        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }

    public void UpdateOrigin(Vector3 origin) {
        Debug.Log(origin);
        if(mpb == null) mpb = new MaterialPropertyBlock();

        mpb.SetVector("_Origin", origin);

        GetComponent<Renderer>().SetPropertyBlock(mpb);

    }

    private void UpdateParameters() {
        if(mpb == null) mpb = new MaterialPropertyBlock();

        if (mainTexture != null) mpb.SetTexture("_MainTex", mainTexture);
        if (mainColor != null) mpb.SetColor("_MainColor", mainColor);
        if (alternateTexture != null) mpb.SetTexture("_AltTex", alternateTexture);
        if (alternateColor != null) mpb.SetColor("_AltColor", alternateColor);

        mpb.SetFloat("_Speed", speed);
        mpb.SetFloat("_Scale", scale);

        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }
}
