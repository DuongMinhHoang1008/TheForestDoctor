using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    // Start is called before the first frame update
    public Element element {get; private set;}
    public int value {get; private set;}
    private void Awake() {
        element = Element.None;
    }
    void Start()
    {
        value = 0;
        InvokeRepeating("BlinkBlockByValue", 0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor(Color elementColor) {
        float alphaCol = 1;
        if (value == 0) {
            alphaCol = 65f/255f;
        } 
        GetComponent<SpriteRenderer>().color = new Color(elementColor.r, elementColor.g, elementColor.b, alphaCol);
    }
    public void ChangeElement(Element el) {
        if (element == Element.None) {
            value = 1;
            element = el;
            GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[el].color;
        } else {
            if (element == GlobalGameVar.Instance().elementDic[el].plus) {
                value = 2;
                GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[element].color;
            } else if (el == GlobalGameVar.Instance().elementDic[element].plus) {
                value = 2;
                element = el;
                GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[el].color;
            } else if (GlobalGameVar.CheckIfIsMinus(el, element)) {
                if (value == 2) {
                    value = 1;
                    GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[element].color;
                } else {
                    value = 0;
                    element = Element.None;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
    public bool CanBePlacedOn(Element blockEl) {
        if ((blockEl != element && value != 2) 
            || (value == 2 
                && GlobalGameVar.CheckIfIsMinus(blockEl, element)
                )
        ) {
            return true;
        } else {
            return false;
        }
    }
    public void PlaceOn(Element el) {
        ChangeElement(el);
    }
    void BlinkBlockByValue() {
        Color color = GetComponent<SpriteRenderer>().color;
        if (value == 2) {
            if (GetComponent<SpriteRenderer>().color.a <= 0.85f) {
                GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
            } else {
                GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.8f);
            }
        } else if (element == Element.None){
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 65f/255f);
        } else {
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
        }
    }
    public void ClearAll() {
        element = Element.None;
        GetComponent<SpriteRenderer>().color = Color.white;
        value = 0;
    }
}
