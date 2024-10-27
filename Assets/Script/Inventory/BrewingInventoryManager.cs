using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BrewingInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotsHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;
    [SerializeField] private SlotClass[] herb;
    [SerializeField] private SlotClass[] potion;

    [SerializeField] private SlotClass movingSlot;//di chuyển các slot khác cho cái item
    [SerializeField] private SlotClass originalSlot;
    [SerializeField] private SlotClass tempSlot;
    [SerializeField] Board brewingBoard;
    [SerializeField] GameObject itemInfo;
    GameObject ingredientShape;
    public Image itemCursor;
    SlotClass[] tempItems;
    [SerializeField] public GameObject[] slots;
    public bool isMoving;
    bool isShowingIngredient = true;
    bool isBrewing = false;
    public static BrewingInventoryManager instance {get; private set;}
    //[SerializeField] private List<SlotClass> items = new List<SlotClass>();
    private void Start()
    {
        if (instance == null) {
            slots = new GameObject[slotsHolder.transform.childCount];
            // items = new SlotClass[slots.Length];
            potion = new SlotClass[slots.Length];
            // herb = new SlotClass[slots.Length];
        
            for (int i = 0;i < slots.Length;i++)
            {
                slots[i] = slotsHolder.transform.GetChild(i).gameObject;
                potion[i] = new SlotClass();
            }
            tempItems = new SlotClass[herb.Length];
            CopyInventory(herb, ref tempItems);
            instance = this;
        }
        RefreshUI();
    }
    public void ClassifyPotion()
    {
        isShowingIngredient = false;
        RefreshPotion();
    }
    public void ClassifyHerb()
    {
        isShowingIngredient = true;
        RefreshHerb();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBrewing)
        {
            if (isMoving)
            {
                EndMove();
            }
            else
            {
                GetItemToBrew();
            }
            
        }
        if (isMoving)
        {
            itemCursor.enabled = true;
            itemCursor.transform.position = Input.mousePosition;
            itemCursor.sprite = movingSlot.GetItem().itemIcon;
        }
        else
        {
            itemCursor.enabled = false;
            itemCursor.sprite = null;
        }
    }
    private void RefreshHerb()
    {
            for (int i = 0; i < slots.Length; i++)
            {
                try
                {
                    slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = herb[i].GetItem().itemIcon;
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = herb[i].GetQuantity() + "";
                }
                catch
                {
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
    }
    private void RefreshPotion()
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = potion[i].GetItem().itemIcon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = potion[i].GetQuantity() + "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void RefreshUI()
    {
        // Classify();
        if (isShowingIngredient) {
            RefreshHerb();
        } else {
            RefreshPotion();
        }
    }

    // Update is called once per frame
    public void AddItem(ItemClass item,int quantity)
    {
        SlotClass slot = ContainItem(item);
        if(slot != null)
        {
            slot.AddQuantity(quantity);
        }
        else
        {
            if (item is HerbClass) {
                for(int i=0; i < herb.Length; i++)
                {
                    if(herb[i].GetItem() == null)
                    {
                        herb[i].AddItem(item, quantity);
                        break;
                    }
                }
            } else if (item is CurePotionClass) {
                for(int i=0; i < potion.Length; i++)
                {
                    if(potion[i].GetItem() == null)
                    {
                        potion[i].AddItem(item, quantity);
                        break;
                    }
                }
            }
        }
        RefreshUI();
    }
    public SlotClass ContainItem(ItemClass item)
    {
        if (item is HerbClass) {
            foreach(SlotClass slot in herb)
            {
                if(slot != null && slot.GetItem() == item)
                {
                    return slot;
                    
                }
            }
        } else if (item is CurePotionClass) {
            foreach(SlotClass slot in potion)
            {
                if(slot != null && slot.GetItem() == item)
                {
                    return slot;
                    
                }
            }
        }
        
        return null;
    }

    private SlotClass GetCloseSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            
            if(Vector2.Distance(slots[i].transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= 0.5f)
            {
                if (isShowingIngredient && i < herb.Length) return herb[i];
                else return potion[i];
            }
            else {}
        }
        return null;
    }
    private void EndMove()
    {
        if(ingredientShape != null) {
            if(!ingredientShape.GetComponent<BlockGroup>().IsPlacable()) {
                Destroy(ingredientShape);
                AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                RefreshUI();
            }
            isMoving = false;
        }
    }
   
    public void GetItemToBrew() {
        SlotClass currentSlot = GetCloseSlot();
        if(currentSlot == null || currentSlot.GetItem() == null)
        {
            CloseItemInfo();
            return;
        }
        HerbClass herbClass = currentSlot.GetItem().GetHerb();
        if (herbClass != null && isShowingIngredient && currentSlot.GetQuantity() > 0) {
            currentSlot.SubQuantity(1);
            isMoving = true;
            movingSlot.AddItem(currentSlot.GetItem(), 1);
            RefreshUI();
            ingredientShape = Instantiate(herbClass.GetIngredientShape(), Vector3.zero, Quaternion.identity);
            ingredientShape.GetComponent<BlockGroup>().SetElement(herbClass.GetElement());
        } else if (currentSlot.GetItem() is CurePotionClass) {
            ShowItemInfo((CurePotionClass) currentSlot.GetItem());
        }
        return;
    }  
    public void UndoBrewing() {
        CopyInventory(tempItems, ref herb);
        RefreshUI();
        brewingBoard.ClearBoard();
    }
    void CopyInventory(SlotClass[] from, ref SlotClass[] to) {
        for (int i = 0; i < from.Length; i++) {
            to[i] = new SlotClass(from[i].GetItem(), from[i].GetQuantity());
        }
    }
    public void CopyInventoryToTemp() {
        CopyInventory(herb, ref tempItems);
    }
    void ShowItemInfo(CurePotionClass curePotion) {
        itemInfo.active = true;
        itemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curePotion.GetPotionName();
        itemInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = curePotion.GetInfoPotion();
    }
    void CloseItemInfo() {
        itemInfo.active = false;
    }
    public bool CheckIfPotionIsFull() {
        for(int i = 0; i < potion.Length; i++)
        {
            if(potion[i] == null || potion[i].GetItem() == null)
            {
                return false;
            }
        }
        return true;
    }
    public void SetIsBrewing(bool b) {
        isBrewing = b;
    }
    public bool ContainPotionCanCure(SicknessClass sickness) {
        foreach(SlotClass slot in potion)
        {
            if(slot != null && slot.GetItem() is CurePotionClass && sickness.CheckIfRightPotion((CurePotionClass)slot.GetItem()))
            {
                return true;
            }
        }
        return false;
    }
}