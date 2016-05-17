using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TestModalWindow : MonoBehaviour
{
    private ModalPanel modalPanel;
    private UnityAction myYesAction;
    private UnityAction myNoAction;
    public bool GO = false;

    void Awake()
    {
        modalPanel = ModalPanel.Instance();

        myYesAction = new UnityAction(TestYesFunc);
        myNoAction = new UnityAction(TestNoFunc);
    }

    void Update()
    {
        if (GO)
        {
            TestWindow();
        }
    }

    public void TestWindow()
    {
        modalPanel.dialougeChoice("Would you like to leave the area?", myYesAction, myNoAction);
    }

    void TestYesFunc()
    {
        
    }

    void TestNoFunc()
    {

    }

}
