using UnityEngine;
using System.Collections;

public class GameMessage : MonoBehaviour
{
    public int textSelection;


    void OnTriggerStay(Collider other)
    {
           if(other.tag == "Player")
           {
               MessageController.textSelection = textSelection;
           }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            MessageController.textSelection = 0;
        }
    }
}
