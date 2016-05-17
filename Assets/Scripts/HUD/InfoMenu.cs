using UnityEngine;
using System.Collections;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine.UI;
public class InfoMenu : MonoBehaviour {
    public Canvas m_InfoMenu;
    public Canvas m_pauseMenu;
    int buffer = 10;
    bool PasueMenuOpen;
	// Use this for initialization
	void Start () {
        m_InfoMenu.enabled = false;
        m_pauseMenu.enabled = false;
        buffer = 10;
	}
	
	// Update is called once per frame
	void Update () 
    {

        //if (m_InfoMenu.enabled)
            //PasueMenuOpen = true;



        if (Input.GetButton("B Button") && buffer <= 0 && m_InfoMenu.enabled)
        {
            Debug.Log(" I GOT THIS! Info");
            buffer = 10;
            m_pauseMenu.enabled = true;
            PasueMenuOpen = false;
            m_InfoMenu.enabled = false;

            // InpauseMenu = false;
        }
        buffer--;
	}
}
