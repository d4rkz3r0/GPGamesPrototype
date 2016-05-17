using UnityEngine;
using System.Collections;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{

    // Use this for initialization
    public Canvas m_pauseMenu;
    public Canvas OptionsMenus;
    public Canvas InfoMenu;
    public Image SelectorImage;
    public List<Text> Options = new List<Text>();
    public int selector = 0;
    int buffer = 10;
    public int bufferCC = 10;
    int BuffOut = 5;
    bool PasueMenuOpen;
    bool NoStatAgain = false;
    public static bool InpauseMenu = false;
    void Start()
    {
        RectTransform temp = Options[selector].gameObject.GetComponent<RectTransform>();
        SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
        buffer = 10;
        PasueMenuOpen = false;
        m_pauseMenu.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (m_pauseMenu.enabled)
        {
            PasueMenuOpen = true;
            OptionsMenus.enabled = false;
        }
        if (Input.GetButton("StartButton") && !NoStatAgain)
        {
            NoStatAgain = true;
            PasueMenuOpen = true;
        }
           



        if (PasueMenuOpen)
        {
            InpauseMenu = true;
            m_pauseMenu.enabled = true;


            if (Input.GetAxis("Vertical") == 1 && buffer <= 0)
            {

                if (selector == 3)
                    selector = 2;
                else if (selector == 2)
                    selector = 1;
                else if (selector == 1)
                    selector = 0;
                else if (selector == 0)
                    selector = 3;

                RectTransform temp = Options[selector].gameObject.GetComponent<RectTransform>();
                SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
                SelectorImage.transform.position = Options[selector].transform.position;

                buffer = 10;

            }
            else if (Input.GetAxis("Vertical") == -1 && buffer <= 0)
            {
                if (selector == 0)
                    selector = 1;

                else if (selector == 1)
                    selector = 2;
                else if (selector == 2)
                    selector = 3;
                else if (selector == 3)
                    selector = 0;

                RectTransform temp = Options[selector].gameObject.GetComponent<RectTransform>();
                SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
                SelectorImage.transform.position = Options[selector].transform.position;

                buffer = 10;
            }
            if (Input.GetButton("A Button") && SelectorImage.transform.position == Options[selector].transform.position)
            {
                if (Options[selector].gameObject.name == "Options")
                {
                    Debug.Log("YOYOYOYOYOY I GOT THIS!");
                    OptionsMenus.enabled = true;
                    m_pauseMenu.enabled = false;
                    //m_pauseMenu.enabled = false;
                    PasueMenuOpen = false;
                }
                else if (Options[selector].gameObject.name == "Resume")
                {
                    m_pauseMenu.enabled = false;
                    InpauseMenu = false;
                    m_pauseMenu.enabled = false;
                    PasueMenuOpen = false;
                    NoStatAgain = false;
                }
                else if (Options[selector].gameObject.name == "Info")
                {
                    InfoMenu.enabled = true;
                   // InpauseMenu = false;
                    m_pauseMenu.enabled = false;
                    PasueMenuOpen = false;
                  //  PasueMenuOpen = false;
                }

            }
        }





        buffer--;
    }

}
