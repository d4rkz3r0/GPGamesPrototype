using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class TiltLight : MonoBehaviour 
{
	public float intensityPercentage = 0.1f;
	public float frequency = 2.0f;
	public float randPercenteage = 0.1f;

	private float _elapsedTime;
	private float _originalIntensity;
	private Light _light;

	// Use this for initialization
	void Start () 
	{
		_light = GetComponent<Light>();
		_elapsedTime = 0.0f;
		_originalIntensity = _light.intensity;
	}
	
	// Update is called once per frame
	void Update () 
	{
		_elapsedTime += Time.deltaTime;
		float tmp = intensityPercentage * (Mathf.Sin (2.0f * Mathf.PI * frequency * _elapsedTime));
		float curValue = _originalIntensity + (_originalIntensity * tmp);
		_light.intensity = curValue + Random.Range (-randPercenteage * curValue, randPercenteage * curValue);
	}
}
