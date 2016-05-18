using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour
{
    //Public Globals
    public float abilitySpeed;
    public int abilityDamage;

    //Private References
    private PlayerController player;


	void Start () 
    {
        player = FindObjectOfType<PlayerController>();
    
        if(player.transform.localScale.x < 0.0f)
        {
            abilitySpeed = -abilitySpeed;
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        if (player.transform.localScale.x > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        //Prototype Hack
	    SFXManager.Instance.PlaySFX(gameObject.name[0] == 'F' ? "rangedFireballSFX" : "rangedLightningSFX");
    }
	
	void Update () 
    {
        transform.Translate((Vector3.forward) * abilitySpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
	
    }
}
