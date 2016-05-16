using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

public class MenuScript : MonoBehaviour
{

    // Use this for initialization

    //e//num PlayerStats = {Health,Fury,Attak,Defense};

    public GameObject[] ItemMenu;
    public List<GameObject> SubItemMenu = new List<GameObject>();
    public List<Text> GoldPrices = new List<Text>();
    public List<Text> BossSouls = new List<Text>();
    public List<int> WeaponUpgradePricing = new List<int>();
    public List<int> PlayerStatsPricing = new List<int>();
    public List<int> ItemPricing = new List<int>();
    public List<int> AblilityPricing = new List<int>();
    public List<int> BuffPricing = new List<int>();
    public Text AblilityInfo;
    public Canvas Shop;
    public Image SelectorImage;
    public Text GoldPrice;
    public Image ItemHealthPotion;
    public Image ItemFuryPotion;
    public Image BuffImage;
    bool[] Bought;
    public int selector;
    public int SubSelector;
    public int buffer = 10;
    int ExitBuffer = 10;
    public GameObject ThePlayer;
    bool InMenu = false;
    int PurchaseBuffer = 0;
    public int SubMenuBuffer = 10;
    public List<Image> PurchaseMeter = new List<Image>();
    int PriceIncrease = 300;
    bool LeaveShop = false;
    public Text YourGold;
    int InputBuffer = 10;

    void Start()
    {
        SetBossSoulPrices(1, BossSouls[0]);
        SetBossSoulPrices(1, BossSouls[1]);
        SetBossSoulPrices(1, BossSouls[2]);


        SetGoldPrices(100, GoldPrices[0]);
        SetGoldPrices(100, GoldPrices[1]);
        ExitBuffer = 0;
        selector = 0;
        SubSelector = 0;
        SelectorImage.color = new Color(1, 1, 1, 0.3f);
        GoldPrice.text = PlayerStatsPricing[0] + " " + "Gold";
        YourGold.text = ThePlayer.GetComponent<PlayerGold>().Gold.ToString();
        SelectorImage.transform.position = ItemMenu[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {





        YourGold.text = ThePlayer.GetComponent<PlayerGold>().Gold.ToString();
        if (Input.GetButton("B Button") && LeaveShop == true && ExitBuffer <= 0)
            Shop.enabled = false;

        if (Input.GetButton("A Button"))
        {
            InMenu = true;
            LeaveShop = false;



        }

        if (Input.GetButton("B Button"))
        {
            InMenu = false;
            SubItemMenu.Clear();
            SelectorImage.transform.position = ItemMenu[selector].transform.position;
            ExitBuffer = 10;
            AblilityInfo.text = "";
            if (InMenu == false)
                LeaveShop = true;

        }




        MainShopMenu(ItemMenu, ref selector, ref buffer, InMenu, ref SubItemMenu);
        SubMenus(SubItemMenu, ref SubSelector, ref SubMenuBuffer);
        PurchaseItems(SubItemMenu, ref SubSelector, ref PurchaseBuffer, 0, ref ThePlayer, InMenu, ref PurchaseMeter, ItemMenu);
        PurchaseBuffer--;
        buffer--;
        SubMenuBuffer--;
        ExitBuffer--;
        InputBuffer--;
    }







    void SubMenus(List<GameObject> _submenu, ref int _subselector, ref int buffer)
    {

        if (Input.GetAxis("Vertical") == -1 && InMenu == true && buffer <= 0)
        {
            if (_submenu.Count == 4)
            {
                if (SubSelector == 0)
                    _subselector = 1;
                else if (SubSelector == 1)
                    _subselector = 2;

                else if (SubSelector == 2)
                    _subselector = 3;

                else if (SubSelector == 3)
                    _subselector = 0;



            }
            else if (_submenu.Count == 3)
            {

                if (SubSelector == 0)
                    _subselector = 1;
                else if (SubSelector == 1)
                    _subselector = 2;

                else if (SubSelector == 2)
                    _subselector = 0;


            }
            else if (_submenu.Count == 2)
            {

                if (SubSelector == 0)
                    _subselector = 1;
                else if (SubSelector == 1)
                    _subselector = 0;


            }




            buffer = 10;
            for (int i = 0; i < _submenu.Count; i++)
            {
                if (i == _subselector)
                {
                    
                    SelectorImage.transform.position = _submenu[_subselector].transform.position;
                  
                    break;
                }

            }

           

        }


    }

    void IncreasePrice(ref List<int> Price, Text _Gold, ref int _subselector)
    {
        Price[_subselector] *= 2;
        GoldPrice.text = Price[_subselector] + " " + "Gold";
    }

    void PurchaseItems(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, GameObject[] ItemMenu)
    {
        SetInfoText(AblilityInfo, _submenu, ref _subselector, ItemMenu, ref selector);
        if (Input.GetButton("A Button") && InSubMenu == true && InputBuffer <= 0)
        {
            InputBuffer = 10;
            if (ItemMenu[selector].gameObject.name == "PlayerStats")
            {
               
                BuyPlayerStats(_submenu, ref _subselector, ref buffer, Gold, ref thePlayer, InSubMenu, ref PurchaseMeter, ref PlayerStatsPricing);
            }
            else if (ItemMenu[selector].gameObject.name == "BuyItems")
                BuyItems(_submenu, ref _subselector, ref buffer, Gold, ref thePlayer, InSubMenu,ref ItemPricing);
        }


    }

    void MainShopMenu(GameObject[] ItemMenu, ref int selector, ref int buffer, bool InSubMenu, ref List<GameObject> _submenu)
    {



        if (Input.GetAxis("Horizontal") == 1 && buffer <= 0 && InSubMenu == false)
        {
            if (selector == 0)
                selector = 1;
            else if (selector == 1)
                selector = 2;

            else if (selector == 2)
                selector = 3;


            else if (selector == 3)
                selector = 4;


            else if (selector == 4)
                selector = 0;



            buffer = 5;
            _submenu.Clear();
            SelectorImage.transform.position = ItemMenu[selector].transform.position;
            for (int i = 0; i < ItemMenu[selector].transform.GetChildCount(); i++)
            {
                _submenu.Add(ItemMenu[selector].transform.GetChild(i).gameObject);


            }

        }
    }

    void IncreaseBossSoulsPrice(int Price, Text BossSouls, int amount)
    {
        Price += amount;
        BossSouls.text = Price + " " + "Boss Souls";


    }

    void SetBossSoulPrices(int Price, Text BossSouls)
    {
        BossSouls.text = Price + " " + "Boss Souls";

    }

    void SetGoldPrices(int Price, Text Gold)
    {
        Gold.text = Price + " " + "Gold";

    }

    void BuyPlayerStats(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, ref List<int> Pricing)
    {
        Debug.Log("I HIT PLAYER STATS");
        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
                _submenu[_subselector].gameObject.name == "Health" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {

            thePlayer.GetComponent<PlayerHealth>().MaxHealth += 200;
            thePlayer.GetComponent<PlayerHealth>().CurHealth = thePlayer.GetComponent<PlayerHealth>().MaxHealth;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            IncreasePrice(ref Pricing, GoldPrice, ref _subselector);
        }
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
            _submenu[_subselector].gameObject.name == "Fp" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {

            thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
            thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            IncreasePrice(ref Pricing, GoldPrice, ref _subselector);

        }



        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
          _submenu[_subselector].gameObject.name == "Attack" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        {

            thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
            thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            //IncreasePrice(PriceIncrease, GoldPrice);

        }


        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
          _submenu[_subselector].gameObject.name == "Defense" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        {

            thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
            thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            //  IncreasePrice(PriceIncrease, GoldPrice);

        }


    }

    void BuyWeaponUpUrades(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu)
    {

        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 &&
                _submenu[_subselector].gameObject.name == "IceUpGrade" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        {

            thePlayer.GetComponent<PlayerHealth>().MaxHealth += 200;
            thePlayer.GetComponent<PlayerHealth>().CurHealth = thePlayer.GetComponent<PlayerHealth>().MaxHealth;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            //  IncreasePrice(PriceIncrease, GoldPrice);
        }
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 &&
            _submenu[_subselector].gameObject.name == "FireUpgrade" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        {

            thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
            thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            // IncreasePrice(PriceIncrease, GoldPrice);

        }



        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
          _submenu[_subselector].gameObject.name == "LightingUpgrade" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        {

            thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
            thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
            // IncreasePrice(PriceIncrease, GoldPrice);

        }



    }

    void BuyItems(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<int> Pricing)
    {
        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && 
            _submenu[_subselector].gameObject.name == "HealthPotionImage" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            BuffImage.enabled = true;
            BuffImage.sprite = ItemHealthPotion.sprite;
            ThePlayer.GetComponent<DpadCoolDown>().ForH = 1;
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            
        }
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && 
            _submenu[_subselector].gameObject.name == "FuryPotionImage" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            BuffImage.enabled = true;
            BuffImage.sprite = ItemFuryPotion.sprite;
            ThePlayer.GetComponent<DpadCoolDown>().ForH = 2;
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            Debug.Log("I Hit Here In Furry");
        }

    }


    void SetInfoText(Text info, List<GameObject> _submenu, ref int _subselector, GameObject[] ItemMenu, ref int selector)
    {
        if(ItemMenu[selector].gameObject.name == "PlayerStats")
        {
            if (_submenu[_subselector].gameObject.name == "Health")
                info.text = " This is Your Health Everytime You Upgrade your Health, You Gain More Health For Your Doungon Exploring";
            else if (_submenu[_subselector].gameObject.name == "Fp")
                info.text = " This Upgrade Will Increase Your Fury Meter,So That You Can Do More Ablility Attacks With It. \n Fury Is Essental if You Like to Use Your Ablilitys And Wipe Out The Enemies" +
                    "That Lie In Your Path That Make You A Dungeon Explorer.";
            else
            {
                Debug.Log("I HIT Info Text NULL");
                info.text = "";
            }



        }




    }
}
