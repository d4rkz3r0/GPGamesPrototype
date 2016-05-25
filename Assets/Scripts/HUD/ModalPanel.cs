using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour
{
    //Vars
    public Text questionText;
    public Image dialogueImageIcon;
    public Button yesButton;
    public Button noButton;

    public GameObject modalPanelObject;

    //Singleton Instance Style
    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType<ModalPanel>();
        }

        if (!modalPanel)
        {
            Debug.Log("ModalPanel Script not attached to a UI Panel in the level.");
        }

        return modalPanel;
    }

    //UnityAction == Function Pointer
    public void dialougeChoice(string questionToAsk, UnityAction yesEvent, UnityAction noEvent)
    {
        //Freeze Player
        FindObjectOfType<PlayerController>().getInput = false;
        FindObjectOfType<FuryMeter>().Currentmeter = 0.0f;
        Time.timeScale = 0.0f;


        //Display Dialouge Box
        modalPanelObject.SetActive(true);

        //Initialize Events
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        
        //Setup Listeners - Yes
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        //Setup Listeners - No
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        //Update Text
        questionText.text = questionToAsk;

        //No Icon
        dialogueImageIcon.gameObject.SetActive(false);

        //Show Buttons
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        //Free Player
        FindObjectOfType<PlayerController>().getInput = true;
        Time.timeScale = 1.0f;
        
        SFXManager.Instance.PlaySFX("dialogueSelectOptionSFX");
        modalPanelObject.SetActive(false);
    }
}
