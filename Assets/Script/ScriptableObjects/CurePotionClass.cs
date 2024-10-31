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
        string info = "Thuốc\n"
                        + "\nV: " + elementValue[Element.Yellow]
                        + " C: " + elementValue[Element.Orange]
                        + "\nĐ: " + elementValue[Element.Red]
                        + " T: " + elementValue[Element.Purple]
                        + "\nXd: " + elementValue[Element.Blue]
                        + " Xl: " + elementValue[Element.Green];
        info += "\n\nGiá bán: " + GetPotionValue();
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
        return (int) (sumVal * (int) Mathf.Pow(3, countDiff - 1) * 10 * Mathf.Pow(1.2f, GlobalGameVar.Instance().upgradeDic[Upgrade.Money]));
    }
}
