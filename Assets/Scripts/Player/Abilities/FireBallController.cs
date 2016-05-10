using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour
{
    //Public Globals
    public float abilitySpeed;
    public int abilityDamage;

    //Private References
    private Rigidbody rb;
    private PlayerController player;
    private ZombieHealth enemy;


	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        enemy = null;
    

        if(player.transform.localScale.x < 0.0f)
        {
            abilitySpeed = -abilitySpeed;
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        if (player.transform.localScale.x > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
	}
	
	void Update () 
    {
        transform.Translate((Vector3.forward) * abilitySpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
		//Fireball->Enemy
		if (other.tag == "Enemy")
        {
			other.GetComponent<EnemyHealth>().CurHealth -= abilityDamage;
        }
    }
}
