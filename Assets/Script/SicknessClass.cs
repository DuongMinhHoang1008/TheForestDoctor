using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sickness", menuName = "Sickness")]
public class SicknessClass : ScriptableObject
{
    [SerializeField] public int level;
    [SerializeField] int yellow;
    [SerializeField] int orange;
    [SerializeField] int red;
    [SerializeField] int purple;
    [SerializeField] int blue;
    [SerializeField] int green;

    public bool CheckIfRightPotion(CurePotionClass potion) {
        if (yellow != potion.elementValue[Element.Yellow]) return false;
        if (orange != potion.elementValue[Element.Orange]) return false;
        if (red != potion.elementValue[Element.Red]) return false;
        if (purple != potion.elementValue[Element.Purple]) return false;
        if (blue != potion.elementValue[Element.Blue]) return false;
        if (green != potion.elementValue[Element.Green]) return false;
        return true;
    }

    public string GetDescription() {
        string str = "Cấp độ: " + level + "\n"
                    + "Vàng: " + yellow + "\n"
                    + "Cam: " + orange + "\n"
                    + "Đỏ: " + red + "\n"
                    + "Tím: " + purple + "\n"
                    + "Xanh dương: " + blue + "\n"
                    + "Xanh lục: " + green;
        return str;
    }
}
