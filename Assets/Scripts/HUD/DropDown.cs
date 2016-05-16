using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

	// Use this for initialization
    public RectTransform[] container;
    public bool isOpen;
    public RectTransform NextItem;
    public Canvas Menu;

    
	void Start () 
    {
        for (int i = 0; i < 5; i++)
        {
            container[i] = transform.FindChild("container").GetComponent<RectTransform>();  
        }
        
        isOpen = false;
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("A Button") && Menu.GetComponent<MenuScript>().selector == 1)
            isOpen = true;

        if (Input.GetButton("B Button"))
            isOpen = false;

       
        for (int i = 0; i < 5; i++)
        {
            Vector3 Scale = container[i].localScale;
            Scale.y = Mathf.Lerp(Scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
            container[i].localScale = Scale;
        }
       
        
           

            


           
        
        
	}
}
