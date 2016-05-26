using UnityEngine;
using System.Collections;

public class LargeBlocks03Scaling : MonoBehaviour {

    MeshRenderer mRenderer;
    public float xScale, yScale;

	// Use this for initialization
	void Start () {
        mRenderer = GetComponent<MeshRenderer>();
        xScale = 5.0f / 6.0f;
        yScale = 3.0f / 4.5f;
	}
	
	// Update is called once per frame
	void Update () {
        mRenderer.material.SetTextureScale("_MainTex", new Vector2(transform.localScale.x * xScale, transform.localScale.y * yScale));
	}
}
