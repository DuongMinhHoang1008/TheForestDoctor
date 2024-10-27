using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StatusEffect {
    None,
    Burn,
    Poison,
    Stun
}

public enum Element {
    None,
    Yellow,
    Orange,
    Red,
    Purple,
    Blue,
    Green
}


public class ElementInfo {
    public Color color {get; private set;}
    //plus = sinh; element sinh plus
    public Element plus {get; private set;}
    //minus = khac; element khac minus
    public Element minus1 {get; private set;}
    public Element minus2 {get; private set;}
    public ElementInfo(Color co, Element p, Element m1, Element m2) {
        color = co;
        plus = p;
        minus1 = m1;
        minus2 = m2;
    }
}

public class GlobalGameVar
{
    private static GlobalGameVar instance;
    public float blockWidth {get; private set;}
    public int money {get; private set;}
    public Dictionary<Element, ElementInfo> elementDic {get; private set;}
    public Dictionary<string, CurePotionClass> curePotionDic {get; private set;}
    private GlobalGameVar() {}
    public static GlobalGameVar Instance() {
        if (instance == null) {
            instance = new GlobalGameVar{
                blockWidth = 2.0f,
                curePotionDic = new Dictionary<string, CurePotionClass>(),
                elementDic = new Dictionary<Element, ElementInfo>()
                {
                    { Element.None, new ElementInfo(Color.white, Element.None, Element.None, Element.None) },
                    { Element.Yellow, new ElementInfo(Color.yellow, Element.Orange, Element.Red, Element.Purple) },
                    { Element.Orange, new ElementInfo(new Color(1f, 0.5f, 0), Element.Red, Element.Purple, Element.Blue) },
                    { Element.Red, new ElementInfo(Color.red, Element.Purple, Element.Blue, Element.Green) },
                    { Element.Purple, new ElementInfo(new Color(0.5f, 0, 0.5f), Element.Blue, Element.Green, Element.Yellow) },
                    { Element.Blue, new ElementInfo(Color.blue, Element.Green, Element.Yellow, Element.Orange) },
                    { Element.Green, new ElementInfo(Color.green, Element.Yellow, Element.Orange, Element.Red) },
                }
            };
        }
        return instance;
    }
    public static bool CheckIfIsMinus(Element e1, Element e2) {
        if (Instance().elementDic[e1].minus1 == e2
            || Instance().elementDic[e1].minus2 == e2
            || Instance().elementDic[e2].minus1 == e1
            || Instance().elementDic[e2].minus2 == e1) {
                return true;
            }
        return false;
    }
}