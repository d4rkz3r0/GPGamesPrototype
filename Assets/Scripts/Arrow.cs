using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject hitEfftect;
    [SerializeField]
    GameObject trailEffect;
    [SerializeField]
    MeshRenderer myMesh;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            SFXManager.Instance.PlaySFX("arrowExplosionSFX");
            myMesh.enabled = false;
            trailEffect.SetActive(false);   
            hitEfftect.SetActive(true);
            Destroy(gameObject, 0.5f);
        }
    }
}
