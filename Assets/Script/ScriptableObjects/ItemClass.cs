using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item")]
public abstract class ItemClass :ScriptableObject
{
    public string ItemName;
    public Sprite itemIcon;
    public bool isStackable = true;
    public abstract ItemClass GetItems(SlotClass slotClass);
    public abstract PotionClass GetPotion();
    public abstract HerbClass GetHerb();

    public virtual string GetDescription() {
        return "";
    }

    
}
