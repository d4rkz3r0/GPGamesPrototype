using UnityEngine;
using System.Collections;

public class MobRotate : MonoBehaviour
{
    public float rotationSpeed = 0.85f;
    public int axis = 0;
    private Vector3 rotationAxis;

    void Start ()
    {
        if(axis == 0)
            rotationAxis = transform.up;
        if (axis == 1)
            rotationAxis = transform.right;
        if (axis == 2)
            rotationAxis = transform.forward;


        StartCoroutine(SpinMob());
    }
	

	void Update ()
    {
	
	}

    private IEnumerator SpinMob()
    {
        while (true)
        {
            transform.Rotate(rotationAxis, rotationSpeed * 360.0f * Time.deltaTime);
            yield return null;
        }
    }
}
