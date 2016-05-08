using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{

    public Canvas WinScreen;
    void Start()
    {
        WinScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerHealth>().CurHealth <= 0)
        {
            WinScreen.enabled = true;


        }
    }
}
