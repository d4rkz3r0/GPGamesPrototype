using UnityEngine;
using System.Collections;

public class GateTrigger : MonoBehaviour
{
    public GameObject GateGameObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (UpdateSpawnerKillCount.currentArea == 1)
            {
                if (UpdateSpawnerKillCount.area1SpawnersRemaining <= 0)
                {
                    UpdateSpawnerKillCount.currentArea = 2;
                    SFXManager.Instance.PlaySFX("warpPortalSFX");
                    GateGameObject.GetComponent<GateScript>().openGate = true;
                }
                else
                {
                    SFXManager.Instance.PlaySFX("gateDenialSFX");
                    MessageController.textSelection = 9;
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
}
