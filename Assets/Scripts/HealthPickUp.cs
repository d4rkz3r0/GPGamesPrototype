using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class HealthPickUp : MonoBehaviour
{
    SphereCollider myCol;

    //Inspector
    public float rotationSpeed = 0.85f;
    public float bounceSpeed = 0.75f;
    public float bounceDistance = 0.5f;

    //Internal
    private float startingYPosition;
    private bool movingUP = true;
    private float totalBounceDistance;

    //Total Hack
    private bool isGoldDrop = false;
    private float destroyTimer = 0.0f;
    public float destroyDuration = 4.5f;


    void Start()
    {
        //Init
        startingYPosition = transform.position.y;
        totalBounceDistance = bounceDistance * 2.0f;

        //Fire
        StartCoroutine(SpinPickup());
        StartCoroutine(BouncePickup());

        //Spaghetti
        if (name[0] == 'G')
        {
            isGoldDrop = true;
            destroyTimer = destroyDuration;
        }

        myCol = GetComponent<SphereCollider>();
        if (myCol)
            myCol.enabled = false;
        Invoke("TurnOn", 1.0f);
    }


    void Update()
    {
        if (isGoldDrop)
        {
            if (destroyTimer > 0.0f)
            {
                destroyTimer -= Time.deltaTime;
            }
            if (destroyTimer <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator SpinPickup()
    {
        while (true)
        {
            transform.Rotate(transform.up, rotationSpeed * 360.0f * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator BouncePickup()
    {
        while (true)
        {
            float newYPosition = transform.position.y + (movingUP ? 1.0f : -1.0f) * bounceSpeed * totalBounceDistance * Time.deltaTime;

            if (newYPosition > startingYPosition + bounceDistance)
            {
                newYPosition = startingYPosition + bounceDistance;
                movingUP = false;
            }
            else if (newYPosition < startingYPosition)
            {
                newYPosition = startingYPosition;
                movingUP = true;
            }

            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
            yield return null;
        }
    }

    void TurnOn()
    {
        if (myCol)
            myCol.enabled = true;
    }
}