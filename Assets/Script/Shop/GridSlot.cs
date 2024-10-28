using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class GridSlot : MonoBehaviour
{
    [SerializeField] public HerbClass herb;
    [SerializeField] Image image;
    [SerializeField] public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = herb.itemIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlusOne() {
        if (int.Parse(inputField.text) < 99) {
            int value = int.Parse(inputField.text) + 1;
            inputField.text = value.ToString();
        }
    }
    public void MinusOne() {
        if (int.Parse(inputField.text) > 0) {
            int value = int.Parse(inputField.text) - 1;
            inputField.text = value.ToString();
        }
    }
    public void InputFieldChange(string str) {
        if (int.Parse(str) < 0) {
            inputField.text = "0";
            return;
        }
        Shop.instance.SummarizeNumber();
        if (!Shop.instance.IfCurrentNumberIsOk()) {
            inputField.text = (Shop.instance.currentNumber - Shop.instance.maxNumber + int.Parse(inputField.text)).ToString();
        }
    }
}
