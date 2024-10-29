using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Item/Herb")]

public class HerbClass : ItemClass
{
    [Header("Herb")]
    public string itemDescription;
    [SerializeField] protected Element element;
    [SerializeField] protected GameObject ingredientShape;
    [SerializeField] public int level;
    
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() {return this;}
    public Element GetElement() { return element; }
    public GameObject GetIngredientShape() { return ingredientShape; }
    public override string GetDescription() { 
        string info = "Nguyên tố:\n";
        switch(element) {
            case Element.Yellow:
                info += "Vàng";
                break;
            case Element.Orange:
                info += "cam";
                break;
            case Element.Red:
                info += "Đỏ";
                break;
            case Element.Purple:
                info += "Tím";
                break;
            case Element.Blue:
                info += "Xanh dương";
                break;
            case Element.Green:
                info += "Xanh lục";
                break;
        }
        info += "\nCấp độ: " + level;
        return info;
    }
}
