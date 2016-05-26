using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class GateTrigger : MonoBehaviour
{
    public GameObject GateGameObject;
    private ModalPanel modalPanel;
    private UnityAction yesAction;
    private UnityAction noAction;

    public bool hasQuestionComponent;
    public string questionToAsk;
    public GameObject vendorWarpLocation;
    public GameObject dungeonStartLocation;
    private CameraOrbit mainCamera;
    private UpdateSpawnerKillCount spawnerUIElement;

    //Save player's fury
    private float furyAmountUponEnter;

    void Awake()
    {
        //Init
        modalPanel = ModalPanel.Instance();
        yesAction = MoveToVendor;
        noAction = JustOpenDoor;

        //Hook
        mainCamera = FindObjectOfType<CameraOrbit>();
        spawnerUIElement = FindObjectOfType<UpdateSpawnerKillCount>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            furyAmountUponEnter = FindObjectOfType<FuryMeter>().Currentmeter;
            if (hasQuestionComponent)
            {
                if (spawnerUIElement.currentArea == 1)
                {
                    if (spawnerUIElement.area1SpawnersRemaining <= 0)
                    {
                        modalPanel.dialougeChoice(questionToAsk, yesAction, noAction);
                        spawnerUIElement.currentArea = 2;
                        GetComponent<BoxCollider>().enabled = false;
                    }
                    else
                    {
                        SFXManager.Instance.PlaySFX("gateDenialSFX");
                        MessageController.textSelection = 9;
                    }
                }
            }
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MessageController.textSelection = 0;
            
        }
    }

    void MoveToVendor()
    {
        SFXManager.Instance.PlaySFX("warpPortalSFX");

        GateGameObject.GetComponent<GateScript>().openGate = true;
        if (vendorWarpLocation.gameObject.name != "Null")
        {
            FindObjectOfType<PlayerController>().transform.position = vendorWarpLocation.transform.position;
            FindObjectOfType<PlayerController>().transform.localRotation = vendorWarpLocation.transform.localRotation;
            Invoke("RestoreFury", 0.25f);
            mainCamera.cameraSnap();
        }

        else if (dungeonStartLocation.gameObject.name != "Null")
        {
            FindObjectOfType<PlayerController>().transform.position = dungeonStartLocation.transform.position;
        }
    }

    void JustOpenDoor()
    {
        //if (spawnerUIElement.currentArea == 1)
        //{
        //    if (spawnerUIElement.area1SpawnersRemaining <= 0)
        //    {
                SFXManager.Instance.PlaySFX("warpPortalSFX");
                GateGameObject.GetComponent<GateScript>().openGate = true;
                Invoke("RestoreFury", 0.25f);
                gameObject.SetActive(false);
          //  }
       // }
    }

    void RestoreFury()
    {
        FindObjectOfType<FuryMeter>().Currentmeter = furyAmountUponEnter;
    }
}
