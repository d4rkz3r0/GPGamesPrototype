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


    #region Setting lists
    public GameObject[] ItemMenu;
    public List<GameObject> SubItemMenu = new List<GameObject>();
   
    public List<Text> BossSouls = new List<Text>();

    public List<int> WeaponUpgradePricing = new List<int>();
    public List<Text> WUGoldPrices = new List<Text>();

    public List<int> PlayerStatsPricing = new List<int>();
    public List<Text> PSGoldPrices = new List<Text>();

    public List<int> ItemPricing = new List<int>();
    public List<Text> ItemGoldPrices = new List<Text>();

    public List<int> AblilityPricing = new List<int>();
    public List<Text> ABGoldPrices = new List<Text>();

    public List<int> BuffPricing = new List<int>();
    public List<Text> BuGoldPrices = new List<Text>();
    #endregion

    //public Text AblilityInfo;
    public Canvas Shop;
    public Image SelectorImage;
    public Text GoldPrice;
    public Text AmountBoughtItems;
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
    public List<Image> PurchaseMeterAB = new List<Image>();
    public List<Image> PurchaseMeterBuff = new List<Image>();
    public List<Image> PurchaseMeterWU = new List<Image>();
    int PriceIncrease = 300;
    int tempDiscount = 50;
    bool LeaveShop = false;
    public Text YourGold;
    public Text PlayerBossSouls;
    public int InputBuffer = 50;
    public int InPutAButton = 15;
   
    public static bool InShopMenu = false;
    public bool DiscountVentdor = false;
    void Start()
    {
        #region Setting all the prices for the store
        SetBossSoulPrices(BuffPricing[0], BuGoldPrices[0]);
        SetBossSoulPrices(BuffPricing[1], BuGoldPrices[1]);
        SetBossSoulPrices(BuffPricing[2], BuGoldPrices[2]);


        SetGoldPrices(ItemPricing[0], ItemGoldPrices[0]);
        SetGoldPrices(ItemPricing[1], ItemGoldPrices[1]);
       
        SetGoldPrices(PlayerStatsPricing[0], PSGoldPrices[0]);
        SetGoldPrices(PlayerStatsPricing[1], PSGoldPrices[1]);
        SetGoldPrices(PlayerStatsPricing[2], PSGoldPrices[2]);


        SetGoldPrices(AblilityPricing[0], ABGoldPrices[0]);
        SetGoldPrices(AblilityPricing[1], ABGoldPrices[1]);
        SetGoldPrices(AblilityPricing[2], ABGoldPrices[2]);



        SetGoldPrices(WeaponUpgradePricing[0], WUGoldPrices[0]);
      //  SetGoldPrices(WeaponUpgradePricing[1], WUGoldPrices[1]);
       // SetGoldPrices(WeaponUpgradePricing[2], WUGoldPrices[2]);
        #endregion
        ExitBuffer = 0;
        selector = 0;
        SubSelector = 0;
        InputBuffer = 15;
        InPutAButton = 20;
        SelectorImage.color = new Color(1, 1, 1, 0.3f);
        YourGold.text = ThePlayer.GetComponent<PlayerGold>().Gold.ToString();
        SelectorImage.transform.position = ItemMenu[0].transform.position;
        PlayerBossSouls.text = ThePlayer.GetComponent<BossSouls>().BossSoul.ToString();
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        AmountBoughtItems.text = "";
    }


    void Update()
    {



        if (!DiscountVentdor)
        {
            SetGoldPrices(ItemPricing[0], ItemGoldPrices[0]);
            SetGoldPrices(ItemPricing[1], ItemGoldPrices[1]);

            SetGoldPrices(PlayerStatsPricing[0], PSGoldPrices[0]);
            SetGoldPrices(PlayerStatsPricing[1], PSGoldPrices[1]);
            SetGoldPrices(PlayerStatsPricing[2], PSGoldPrices[2]);


            SetGoldPrices(AblilityPricing[0], ABGoldPrices[0]);
            SetGoldPrices(AblilityPricing[1], ABGoldPrices[1]);
            SetGoldPrices(AblilityPricing[2], ABGoldPrices[2]);



            SetGoldPrices(WeaponUpgradePricing[0], WUGoldPrices[0]);

        }
        else if(DiscountVentdor)
        {

              SetGoldPrices(ItemPricing[0], ItemGoldPrices[0]);
            SetGoldPrices(ItemPricing[1], ItemGoldPrices[1]);

            SetGoldPrices(PlayerStatsPricing[0], PSGoldPrices[0]);
            SetGoldPrices(PlayerStatsPricing[1], PSGoldPrices[1]);
            SetGoldPrices(PlayerStatsPricing[2], PSGoldPrices[2]);


            SetGoldPrices(AblilityPricing[0], ABGoldPrices[0]);
            SetGoldPrices(AblilityPricing[1], ABGoldPrices[1]);
            SetGoldPrices(AblilityPricing[2], ABGoldPrices[2]);



            SetGoldPrices(WeaponUpgradePricing[0], WUGoldPrices[0]);
        }

        if (ThePlayer.GetComponent<Multiplier>().AmountOfPoitionBought != 0)
            AmountBoughtItems.text = "x" + ThePlayer.GetComponent<Multiplier>().AmountOfPoitionBought;
        PlayerBossSouls.text = ThePlayer.GetComponent<BossSouls>().BossSoul.ToString();
        YourGold.text = ThePlayer.GetComponent<PlayerGold>().Gold.ToString();
        if (Input.GetButton("B Button") && LeaveShop == true && ExitBuffer <= 0)
        {
            Shop.enabled = false;
            FindObjectOfType<PlayerController>().getInput = true;
            InShopMenu = false;
            enabled = false; //You may need the script running after exit, in case, remove this.
        }
            

        if (Input.GetButton("A Button"))
        {
            if(InPutAButton <= 0)
            {

              //  if(ThePlayer.GetComponent<PlayerGold>().Gold )
                if(!InMenu)
                SFXManager.Instance.PlaySFX("ShopSelectAudio");

                InMenu = true;
                LeaveShop = false;
                SelectorImage.transform.position = SubItemMenu[SubSelector].transform.position;
                RectTransform temp = SubItemMenu[SubSelector].gameObject.GetComponent<RectTransform>();
                SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width / 2, temp.rect.height / 2);
                SelectorImage.transform.localScale = SubItemMenu[SubSelector].transform.localScale;
                InPutAButton = 10;
                InputBuffer = 5;
            }
           
            



        }

        if (Input.GetButton("B Button"))
        {
            InMenu = false;
            SubItemMenu.Clear();
            RectTransform temp = ItemMenu[selector].gameObject.GetComponent<RectTransform>();
            SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
            SelectorImage.transform.position = ItemMenu[selector].transform.position;
            SelectorImage.transform.localScale = ItemMenu[selector].transform.localScale;
            ExitBuffer = 10;
           // AblilityInfo.text = "";
            if (InMenu == false)
                LeaveShop = true;
            SubSelector = 0;

        }




        MainShopMenu(ItemMenu, ref selector, ref buffer, InMenu, ref SubItemMenu);
        SubMenus(SubItemMenu, ref SubSelector, ref SubMenuBuffer);
        PurchaseItems(SubItemMenu, ref SubSelector, ref PurchaseBuffer, 0, ref ThePlayer, InMenu, ref PurchaseMeter, ItemMenu);
        PurchaseBuffer--;
        buffer--;
        SubMenuBuffer--;
        ExitBuffer--;
        InputBuffer--;
        InPutAButton--;
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
                    //AblilityInfo.text = " ";
                    SFXManager.Instance.PlaySFX("ShopMenuSelections");
                    SelectorImage.transform.position = _submenu[_subselector].transform.position;
                  //  SelectorImage.transform.right = _submenu[_subselector].transform.right;
                   
                      
                    
                     
                   
                    break;
                }

            }

           

        }
        else if (Input.GetAxis("Vertical") == 1 && InMenu == true && buffer <= 0)
        {
            if (_submenu.Count == 4)
            {
                if (SubSelector == 3)
                    _subselector = 2;
                else if (SubSelector == 2)
                    _subselector = 1;

                else if (SubSelector == 1)
                    _subselector = 0;

                else if (SubSelector == 0)
                    _subselector = 3;



            }
            else if (_submenu.Count == 3)
            {

                if (SubSelector == 2)
                    _subselector = 1;
                else if (SubSelector == 1)
                    _subselector = 0;

                else if (SubSelector == 0)
                    _subselector = 2;

            }
            else if (_submenu.Count == 2)
            {

                if (SubSelector == 2)
                    _subselector = 1;
                else if (SubSelector == 1)
                    _subselector = 0;


            }




            buffer = 10;
            for (int i = 0; i < _submenu.Count; i++)
            {
                if (i == _subselector)
                {
                    //AblilityInfo.text = "";
                    SFXManager.Instance.PlaySFX("ShopMenuSelections");
                    SelectorImage.transform.position = _submenu[_subselector].transform.position;

                    break;
                }

            }


        }


    }

    void IncreasePrice(ref List<int> Price, Text _Gold, ref int _subselector)
    {
        if(DiscountVentdor)
        {

            Price[_subselector] *= 2;
            tempDiscount =  Price[_subselector] / 6;
            _Gold.text = tempDiscount + " " + "Gold";
        }
        else
        {
            Price[_subselector] *= 2;
            _Gold.text = Price[_subselector] + " " + "Gold";
        }
     
    }

    void PurchaseItems(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, GameObject[] ItemMenu)
    {
        //SetInfoText(AblilityInfo, _submenu, ref _subselector, ItemMenu, ref selector);
      
        if (Input.GetButton("A Button") && InSubMenu == true && InputBuffer <= 0)
        {
            InputBuffer = 10;
            if (ItemMenu[selector].gameObject.name == "PlayerStats")
            {

                BuyPlayerStats(_submenu, ref _subselector, ref buffer, Gold, ref thePlayer, InSubMenu, ref PurchaseMeter, ref PlayerStatsPricing);
            }
            else if (ItemMenu[selector].gameObject.name == "BuyItems")
                BuyItems(_submenu, ref _subselector, ref buffer, Gold, ref thePlayer, InSubMenu, ref ItemPricing);
            else if (ItemMenu[selector].gameObject.name == "Ability Upgrades")
                BuyPlayerAblilitys(_submenu, ref _subselector, ref buffer, Gold, ref thePlayer, InSubMenu, ref PurchaseMeterAB, ref AblilityPricing);
            else if (ItemMenu[selector].gameObject.name == "Buff Upgrades")
                BuyPlayeBuffs(_submenu, ref _subselector, ref buffer, Gold, ref ThePlayer, InSubMenu, ref PurchaseMeterBuff, ref BuffPricing);
            else if (ItemMenu[selector].gameObject.name == "WeaponUpgrades")
                BuyWeaponUpUrades(_submenu, ref _subselector, ref buffer, Gold, ref thePlayer, InSubMenu,ref PurchaseMeterWU, ref WeaponUpgradePricing);
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



            buffer = 8;
            _submenu.Clear();
            SelectorImage.transform.position = ItemMenu[selector].transform.position;
            RectTransform temp = ItemMenu[selector].gameObject.GetComponent<RectTransform>();
            SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
            SelectorImage.transform.localScale = ItemMenu[selector].transform.localScale;
            SFXManager.Instance.PlaySFX("ShopMenuSelections");
           // SelectorImage.rectTransform = ItemMenu[selector].transform
            for (int i = 0; i < ItemMenu[selector].transform.GetChildCount(); i++)
            {
                if (ItemMenu[selector].transform.GetChild(i).name != "IceUpGrade" && ItemMenu[selector].transform.GetChild(i).name != "LightiningUpgrade")
                _submenu.Add(ItemMenu[selector].transform.GetChild(i).gameObject);


            }

        }
         else if (Input.GetAxis("Horizontal") == -1 && buffer <= 0 && InSubMenu == false)
        {
            if (selector == 4)
                selector = 3;
            else if (selector == 3)
                selector = 2;

            else if (selector == 2)
                selector = 1;


            else if (selector == 1)
                selector = 0;


            else if (selector == 0)
                selector = 4;



            buffer = 8;
            _submenu.Clear();
            SelectorImage.transform.position = ItemMenu[selector].transform.position;
            RectTransform temp = ItemMenu[selector].gameObject.GetComponent<RectTransform>();
            SelectorImage.rectTransform.sizeDelta = new Vector2(temp.rect.width, temp.rect.height);
            SelectorImage.transform.localScale = ItemMenu[selector].transform.localScale;
            SFXManager.Instance.PlaySFX("ShopMenuSelections");
            for (int i = 0; i < ItemMenu[selector].transform.GetChildCount(); i++)
            {
                _submenu.Add(ItemMenu[selector].transform.GetChild(i).gameObject);


            }

        }
    }

    void IncreaseBossSoulsPrice(ref List<int> Price, Text _Gold, ref int _subselector)
    {
        Price[_subselector] *= 2;
        _Gold.text = Price[_subselector] + " " + "Boss Souls";


    }

    void SetBossSoulPrices(int Price, Text BossSouls)
    {
        BossSouls.text = Price + " " + "Boss Souls";

    }

    void SetGoldPrices(int Price, Text Gold)
    {
        if (DiscountVentdor)
        {
            //tempDiscount *= 2;
            Gold.text =  tempDiscount + " " + "Gold";
        }
            
        else
        Gold.text = Price + " " + "Gold";

    }

    void BuyPlayerStats(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, ref List<int> Pricing)
    {
        Debug.Log("I HIT PLAYER STATS");
       
        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
                _submenu[_subselector].gameObject.name == "Health" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<PlayerHealth>().MaxHealth += 200;
            thePlayer.GetComponent<PlayerHealth>().CurHealth = thePlayer.GetComponent<PlayerHealth>().MaxHealth;
            PurchaseMeter[_subselector].fillAmount += .03f;
            buffer = 20;
           // ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            IncreasePrice(ref Pricing, PSGoldPrices[0], ref _subselector);
            if (DiscountVentdor)
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector] / 6;
            else
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(Pricing[_subselector], PSGoldPrices[0]);

        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
            _submenu[_subselector].gameObject.name == "Fp" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
            thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += .03f;
            if (PurchaseMeter[_subselector].fillAmount == 1)
                thePlayer.GetComponent<FuryMeter>().decayRate = 0;
            buffer = 20;
           // ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            IncreasePrice(ref Pricing,PSGoldPrices[1], ref _subselector);
            if (DiscountVentdor)
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector] / 6;
            else
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(Pricing[_subselector], PSGoldPrices[1]);
        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }



        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
          _submenu[_subselector].gameObject.name == "Attack" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().basicAttkMulitplier += .5f;
            
            PurchaseMeter[_subselector].fillAmount += .03f;
            buffer = 20;
            //ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            IncreasePrice(ref Pricing, PSGoldPrices[2], ref _subselector);
            if (DiscountVentdor)
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector] / 6;
            else
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(Pricing[_subselector], PSGoldPrices[2]);
        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }


        #region Defense Code
        //if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
        //  _submenu[_subselector].gameObject.name == "Defense" && ThePlayer.GetComponent<PlayerGold>().Gold > PriceIncrease)
        //{

        //    thePlayer.GetComponent<FuryMeter>().MaxMeter += 200;
        //    thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
        //    PurchaseMeter[_subselector].fillAmount += .14f;
        //    buffer = 20;
        //    ThePlayer.GetComponent<PlayerGold>().Gold -= PriceIncrease;
        //    //  IncreasePrice(PriceIncrease, GoldPrice);

        //}
        #endregion

    }

    void BuyWeaponUpUrades(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, ref List<int> WeaponUpgradePricing)
    {

        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
                _submenu[_subselector].gameObject.name == "IceUpGrade" && ThePlayer.GetComponent<PlayerGold>().Gold > WeaponUpgradePricing[_subselector])
        {

            thePlayer.GetComponent<PlayerHealth>().MaxHealth += 200;
            thePlayer.GetComponent<PlayerHealth>().CurHealth = thePlayer.GetComponent<PlayerHealth>().MaxHealth;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= WeaponUpgradePricing[_subselector];
            IncreasePrice(ref WeaponUpgradePricing, WUGoldPrices[0], ref _subselector);
        }
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
            _submenu[_subselector].gameObject.name == "FireUpgrade" && ThePlayer.GetComponent<PlayerGold>().Gold > WeaponUpgradePricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().fireDamageThing = 2;
           // thePlayer.GetComponent<FuryMeter>().Currentmeter = thePlayer.GetComponent<FuryMeter>().MaxMeter;
            PurchaseMeter[_subselector].fillAmount += 1f;
            buffer = 20;
           // ThePlayer.GetComponent<PlayerGold>().Gold -= WeaponUpgradePricing[_subselector];
            IncreasePrice(ref WeaponUpgradePricing, WUGoldPrices[0], ref _subselector);
            if (DiscountVentdor)
                ThePlayer.GetComponent<PlayerGold>().Gold -= WeaponUpgradePricing[_subselector] / 6;
            else
                ThePlayer.GetComponent<PlayerGold>().Gold -= WeaponUpgradePricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(WeaponUpgradePricing[_subselector], WUGoldPrices[0]);

        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < WeaponUpgradePricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }



        if (  _submenu[_subselector].gameObject.name == "LightiningUpgrade" && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1
          && ThePlayer.GetComponent<PlayerGold>().Gold > WeaponUpgradePricing[_subselector])
        {

         
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<PlayerGold>().Gold -= WeaponUpgradePricing[_subselector];
            IncreasePrice(ref WeaponUpgradePricing, WUGoldPrices[1], ref _subselector);
            // IncreasePrice(PriceIncrease, GoldPrice);

        }



    }


   
    void BuyPlayerAblilitys(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, ref List<int> Pricing)
    {
        Debug.Log(_subselector);
        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;

        if (_submenu[_subselector].gameObject.name == "WhrilWindAblilityUpgrade" && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().whirlWindMultiplier += .5f;
            PurchaseMeterAB[_subselector].fillAmount += .14f;
            buffer = 20;
           // ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            IncreasePrice(ref Pricing, ABGoldPrices[0], ref _subselector);
            if (DiscountVentdor)
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector] / 6;
            else
                ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(Pricing[_subselector], ABGoldPrices[0]);
        }
        else
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }
        if ( _submenu[_subselector].gameObject.name == "SlamAblilityUpgrade" && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
              ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().groundSlamMultiplier += .5f;
            PurchaseMeterAB[_subselector].fillAmount += .14f;
            buffer = 20;
            IncreasePrice(ref Pricing, ABGoldPrices[1], ref _subselector);
            if(DiscountVentdor)
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector] / 6;
            else
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(Pricing[_subselector], ABGoldPrices[1]);
            

        }
        else
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }



        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 &&
          _submenu[_subselector].gameObject.name == "DashAblilityUpgrade" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().chargeMultiplier += .5f;
            PurchaseMeterAB[_subselector].fillAmount += .14f;
            buffer = 20;
           // ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            IncreasePrice(ref Pricing, ABGoldPrices[2], ref _subselector);
              if(DiscountVentdor)
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector] / 6;
            else
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];

            if (!DiscountVentdor)
                SetGoldPrices(Pricing[_subselector], ABGoldPrices[2]);
        }
        else
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }
    }


    void BuyPlayeBuffs(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<Image> PurchaseMeter, ref List<int> Pricing)
    {

        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (_submenu[_subselector].gameObject.name == "AtkBuffUpgrade" && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 && ThePlayer.GetComponent<BossSouls>().BossSoul > Pricing[_subselector])
               
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().attackBuffMultiplier += .5f;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<BossSouls>().BossSoul -= Pricing[_subselector];
            IncreaseBossSoulsPrice(ref BuffPricing, BuGoldPrices[0], ref _subselector);

        
        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }
        if ( _submenu[_subselector].gameObject.name == "DefBuffUpgrade" && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 && ThePlayer.GetComponent<BossSouls>().BossSoul > Pricing[_subselector])
           
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            thePlayer.GetComponent<Multiplier>().defBuffMultiplier = .5f;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<BossSouls>().BossSoul -= Pricing[_subselector];
            IncreaseBossSoulsPrice(ref BuffPricing, BuGoldPrices[1], ref _subselector);

        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }



        if (_submenu[_subselector].gameObject.name == "VampBuffUpgrade" && buffer <= 0 && PurchaseMeter[_subselector].fillAmount != 1 && ThePlayer.GetComponent<BossSouls>().BossSoul > Pricing[_subselector])
         
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
           thePlayer.GetComponent<Multiplier>().vampMultiplier += .5f;
            PurchaseMeter[_subselector].fillAmount += .14f;
            buffer = 20;
            ThePlayer.GetComponent<BossSouls>().BossSoul -= Pricing[_subselector];
            IncreaseBossSoulsPrice(ref BuffPricing, BuGoldPrices[2], ref _subselector);

        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }
    }

    void BuyItems(List<GameObject> _submenu, ref int _subselector, ref int buffer, int Gold, ref GameObject thePlayer, bool InSubMenu, ref List<int> Pricing)
    {
        SelectorImage.transform.position = SubItemMenu[_subselector].transform.position;
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 &&
            _submenu[_subselector].gameObject.name == "HealthPotionImage" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector] && BuffImage.sprite != ItemFuryPotion.sprite)
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            BuffImage.enabled = true;
            BuffImage.sprite = ItemHealthPotion.sprite;
            ThePlayer.GetComponent<DpadCoolDown>().ForH = 1;
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
           

            if(thePlayer.GetComponent<Multiplier>().AmountOfPoitionBought != 5)
                thePlayer.GetComponent<Multiplier>().AmountOfPoitionBought += 1;


        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
        }
        if (SelectorImage.transform.position == _submenu[_subselector].transform.position && buffer <= 0 &&
            _submenu[_subselector].gameObject.name == "FuryPotionImage" && ThePlayer.GetComponent<PlayerGold>().Gold > Pricing[_subselector] && BuffImage.sprite != ItemHealthPotion.sprite)
        {
            SFXManager.Instance.PlaySFX("ka ching Sound Effect");
            BuffImage.enabled = true;
            BuffImage.sprite = ItemFuryPotion.sprite;
            ThePlayer.GetComponent<DpadCoolDown>().ForH = 2;
            ThePlayer.GetComponent<PlayerGold>().Gold -= Pricing[_subselector];
            if (thePlayer.GetComponent<Multiplier>().AmountOfPoitionBought != 5)
                thePlayer.GetComponent<Multiplier>().AmountOfPoitionBought += 1;
        }
        else if (ThePlayer.GetComponent<PlayerGold>().Gold < Pricing[_subselector])
        {
            SFXManager.Instance.PlaySFX("ShopErorrSound");
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
