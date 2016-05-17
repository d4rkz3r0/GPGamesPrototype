using UnityEngine;
using System.Collections;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine.UI;
public class OptionMenu : MonoBehaviour {

    // Use this for initialization
    public Canvas m_OptionMenu;
    public Canvas m_pauseMenu;
    public Text Inverted;
    public Text Sensitivity;
    public Image SelectorImage;
    public List<Text> Options = new List<Text>();
    public int selector = 0;
    int buffer = 10;
    public int bufferCC = 10;
    int BuffOut = 5;
    bool PasueMenuOpen;
  //  public static bool InpauseMenu = false;
    void Start()
    {
        RectTransform temp = Options[selector].gameObject.GetComponent<RectTransform>();
        SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
        Inverted.text = GetComponent<CameraOrbit>().GetInverted().ToString();
        buffer = 10;
        PasueMenuOpen = false;
        m_OptionMenu.enabled = false;
        m_pauseMenu.enabled = false;
        SelectorImage.color = new Color(1, 1, 1, 0.3f);
        Sensitivity.text = GetComponent<CameraOrbit>().GetSensitivity().ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_OptionMenu.enabled)
            PasueMenuOpen = true;

        if (PasueMenuOpen)
        {
            //InpauseMenu = true;
            Inverted.text = GetComponent<CameraOrbit>().GetInverted().ToString();
            Sensitivity.text = GetComponent<CameraOrbit>().GetSensitivity().ToString();
            //m_pauseMenu.enabled = true;


            if (Input.GetAxis("Vertical") == 1 && buffer <= 0)
            {
                if (selector == 1)
                    selector = 0;

                else if (selector == 0)
                    selector = 1;

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
                    selector = 0;

                RectTransform temp = Options[selector].gameObject.GetComponent<RectTransform>();
                SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
                SelectorImage.transform.position = Options[selector].transform.position;

                buffer = 10;
            }


            if (Options[selector].gameObject.name == "ICC")
            {

                if (Input.GetAxis("Horizontal") == 1 && bufferCC <= 0)
                {
                    GetComponent<CameraOrbit>().SetInvertedControls(1);
                    bufferCC = 10;
                }

                else if (Input.GetAxis("Horizontal") == -1 && bufferCC <= 0)
                {
                    GetComponent<CameraOrbit>().SetInvertedControls(-1);
                    bufferCC = 10;
                }



            }


            if (Options[selector].gameObject.name == "Sensitivity")
            {

                if (Input.GetAxis("Horizontal") == 1 && bufferCC <= 0)
                {
                    GetComponent<CameraOrbit>().InceaseSensitivity(1);
                    bufferCC = 10;
                }

                else if (Input.GetAxis("Horizontal") == -1 && bufferCC <= 0)
                {
                    GetComponent<CameraOrbit>().DecreaseSensitivity(1);
                    bufferCC = 10;
                }



            }


        }
        if (Input.GetButton("B Button") && BuffOut <= 0 && m_OptionMenu.enabled)
        {
            Debug.Log("I GOT THIS! Options");
            bufferCC = 10;
            m_pauseMenu.enabled = true;
            PasueMenuOpen = false;
            m_OptionMenu.enabled = false;
           // InpauseMenu = false;
        }
        buffer--;
        bufferCC--;
        BuffOut--;
    }
}
