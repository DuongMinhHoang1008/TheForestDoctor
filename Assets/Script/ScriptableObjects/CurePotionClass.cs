using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cure Potion", menuName = "Item/CurePotion")]
public class CurePotionClass : ItemClass
{
    public Dictionary<Element, int> elementValue {get; private set;} = new Dictionary<Element, int>();
    
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() {return null;}
    public void SetElementValue(Element element, int value) {
        elementValue.Add(element, value);
    }
    public void SetSprite(Sprite s) {
        itemIcon = s;
    }
    virtual public string GetPotionName() {
        return ItemName;
    }
    virtual public string GetInfoPotion() {
        string info = "";
        return info;
    }
    public override string GetDescription()
    {
        return GetInfoPotion();
    }
}
