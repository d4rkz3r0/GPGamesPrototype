using UnityEngine;
using System.Collections;

public class PickUpPulse : MonoBehaviour {

    [SerializeField]
    float pulseTime = 1.0f;
    [SerializeField]
    float RangeMax = 1.0f;
    [SerializeField]
    float RangeMin = .25f;
    float timer = 0;
    float curSize = 0;
    bool down = true;

	// Use this for initialization
	void Start () {

        timer = pulseTime;
	}
	
	// Update is called once per frame
	void Update () {

        if (pulseTime == 0)
        {
            pulseTime = .00000001f;
        }

        if (timer <= 0)
        {
            down = false;
        }
        if (timer >= pulseTime)
        {
            down = true;
        }


        if (down)
        {
            timer -= Time.deltaTime; 
        }
        else
        {
            timer += Time.deltaTime;
        }

        curSize = (timer / pulseTime);
        curSize = Mathf.Lerp(RangeMin, RangeMax, curSize);

        float test = RenderSettings.haloStrength;

        RenderSettings.haloStrength = curSize;
        

	}
}
