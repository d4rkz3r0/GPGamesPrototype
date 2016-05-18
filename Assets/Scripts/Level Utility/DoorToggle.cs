using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DoorToggle : MonoBehaviour
{
    public GameObject doorToToggle;
    private ModalPanel modalPanel;
    private UnityAction yesAction;
    private UnityAction noAction;

    public bool hasQuestionComponent = false;
    public int functionSelect = 0;

    public string questionToAsk;

    public GameObject warpPosition;

    /* 0 - Can Toggle Door with or without a dialogue box, make sure to check the bool
     * 1 - Toggles Door with or without a dialogue box, must also set positionToWarpTo.
     * 
     * 
     * 
     */

    void Awake()
    {
        modalPanel = ModalPanel.Instance();

        switch (functionSelect)
        {
            case 0:
            {
                    yesAction = ToggleDoor;
                    noAction = DoNothing;
                    break;
            }
            case 1:
            {
                    yesAction = ToggleDoorAndWarp;
                    noAction = DoNothing;
                    break;
            }
            default:
            {
                break;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasQuestionComponent)
            {
                ToggleDoor();
            }
            else
            {
                modalPanel.dialougeChoice(questionToAsk, yesAction, noAction);
            }
        }
    }

    void ToggleDoor()
    {
        if (hasQuestionComponent)
        {
            gameObject.SetActive(false);
        }
        doorToToggle.GetComponent<AnimatedDoor>().openDoor = true;
    }

    void ToggleDoorAndWarp()
    {
        if (hasQuestionComponent)
        {
            gameObject.SetActive(false);
        }
        SFXManager.Instance.PlaySFX("warpPortalSFX");
        doorToToggle.GetComponent<AnimatedDoor>().openDoor = true;
        FindObjectOfType<PlayerController>().transform.position = warpPosition.transform.position;
    }

    void DoNothing()
    {
        
    }
}
