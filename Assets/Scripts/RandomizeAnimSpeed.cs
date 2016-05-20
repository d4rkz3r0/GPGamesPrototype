using UnityEngine;
using System.Collections;

public class RandomizeAnimSpeed : MonoBehaviour {

    [SerializeField]
    float min = .2f;
    [SerializeField]
    float max = .3f;
    [SerializeField]
    string animationName = "Light Animation";


	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play(animationName);
        GetComponent<Animator>().speed = Random.Range(min, max);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
