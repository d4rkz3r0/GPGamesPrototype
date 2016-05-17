using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour
{


	void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}

    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
