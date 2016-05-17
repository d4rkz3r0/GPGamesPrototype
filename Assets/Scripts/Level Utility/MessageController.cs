using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour 
{
    //Debug Inspector Vars
    public static int textSelection;

    //Interal References
    private Text displayedText;

    //External
    private PlayerController player;

    private bool doorUnlock = false;

	private bool pressedUp;
    
	void Start ()
    {
        displayedText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();

	}
	
	void Update () 
    {
        switch (textSelection)
        {
            case 0:
                {
                    displayedText.text =
                        "";
                    break;
                }
            case 1:
                {
                    displayedText.text =
                         "Welcome to INSERT_NAME" +
                         "_HERE,\nHero!";
                    break;
                }
            case 2:
                {

                    displayedText.text =
                        "Press X to perform your\n" +
                        "basic attack chain.";
                    break;
                }
            case 3:
            {
                displayedText.text =
                    "";
                    break;
                }
            case 4:
                {
                    displayedText.text =
                         "AMBUSH!!";
                    break;
                }
            case 5:
                {
                    displayedText.text =
                        "Good work button mashing.\n" +
                        "Continue to the next room.";
                    break;
                }
            case 6:
                {
                    displayedText.text =
                        "Welcome to the Enemy\n" +
                        "Beastiary. Ignore the cries\n" +
                        "for help, it's too late for them.";
                    break;
                }

            default:
                {
                    break;
                }


        }

	}
}
