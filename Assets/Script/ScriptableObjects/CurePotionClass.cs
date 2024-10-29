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
        string info = "Thuốc"
                        + "\nVàng: " + elementValue[Element.Yellow]
                        + "\nCam: " + elementValue[Element.Orange]
                        + "\nĐỏ: " + elementValue[Element.Red]
                        + "\nTím: " + elementValue[Element.Purple]
                        + "\nXanh dương: " + elementValue[Element.Blue]
                        + "\nxanh lục: " + elementValue[Element.Green];
        info += "\nGiá bán: " + GetPotionValue();
        return info;
    }
    public override string GetDescription()
    {
        return GetInfoPotion();
    }
    public void SetName(string str) {
        ItemName = str;
    }
    public int GetPotionValue() {
        int countDiff = 0;
        int sumVal = 0;
        foreach (Element el in elementValue.Keys) {
            if (elementValue[el] > 0) countDiff++;
            sumVal += elementValue[el];
        }
        return sumVal * countDiff * 10;
    }
}
