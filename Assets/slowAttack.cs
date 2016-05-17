using UnityEngine;
using System.Collections;

public class slowAttack : MonoBehaviour
{

    // Use this for initialization
    public float moveTimer = 3.5f;
    public int speed = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer > 0 && !PauseMenu.InpauseMenu)
        {
            moveTimer -= Time.deltaTime;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {

            Destroy(gameObject, 2.0f);
        }
    }
}
