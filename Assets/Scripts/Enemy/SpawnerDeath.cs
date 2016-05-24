using UnityEngine;
using System.Collections;

public class SpawnerDeath : MonoBehaviour
{

    // Use this for initialization
    float scale = 1.0f;
    float rate = 2.0f;
    [SerializeField]
    Renderer myRenderer;
    void Start()
    {
        myRenderer.material.color = new Color(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        scale -= Time.deltaTime * rate;
        if (scale <= 0)
            Destroy(gameObject);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
