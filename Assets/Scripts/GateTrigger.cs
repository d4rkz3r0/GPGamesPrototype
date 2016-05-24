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

    void Awake()
    {
        modalPanel = ModalPanel.Instance();
        yesAction = MoveToVendor;
        noAction = JustOpenDoor;

        mainCamera = FindObjectOfType<CameraOrbit>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hasQuestionComponent)
            {
                if (UpdateSpawnerKillCount.currentArea == 1)
                {
                    if (UpdateSpawnerKillCount.area1SpawnersRemaining <= 0)
                    {
                        modalPanel.dialougeChoice(questionToAsk, yesAction, noAction);
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
            mainCamera.cameraSnap();
        }

        else if (dungeonStartLocation.gameObject.name != "Null")
        {
            FindObjectOfType<PlayerController>().transform.position = dungeonStartLocation.transform.position;
        }
    }

    void JustOpenDoor()
    {
        if (UpdateSpawnerKillCount.currentArea == 1)
        {
            if (UpdateSpawnerKillCount.area1SpawnersRemaining <= 0)
            {
                UpdateSpawnerKillCount.currentArea = 2;
                SFXManager.Instance.PlaySFX("warpPortalSFX");
                GateGameObject.GetComponent<GateScript>().openGate = true;
            }
        }
    }
}
