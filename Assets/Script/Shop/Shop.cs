using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] public int maxNumber = 6;
    [SerializeField] TextMeshProUGUI showNumber;
    [SerializeField] GameObject ingredients;
    List<GameObject> ingredientsList;
    public int currentNumber {get; private set;} = 0;
    int currentMoney = 0;
    public static Shop instance { get; private set; }
    private void Awake() {
        if (instance == null) {
            instance = this;
        } 
        ingredientsList = new List<GameObject>();
        for (int i = 0; i < ingredients.transform.childCount; i++) {
            ingredientsList.Add(ingredients.transform.GetChild(i).gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        showNumber.text = currentNumber.ToString() + " / " + maxNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNumber(int num) {
        Debug.Log(num + " " + currentNumber);
        currentNumber += num;
        if (currentNumber > maxNumber) currentNumber = maxNumber;
        if (currentNumber < 0) currentNumber = 0;
        showNumber.text = currentNumber.ToString() + " / " + maxNumber.ToString();
       
    }
    public void SummarizeNumber() {
        currentNumber = 0;
        currentMoney = 0;
        foreach (GameObject g in ingredientsList) {
            currentNumber += int.Parse(g.GetComponent<GridSlot>().inputField.text);
            currentMoney += g.GetComponent<GridSlot>().herb.level * 10 * int.Parse(g.GetComponent<GridSlot>().inputField.text);
        }
        showNumber.text = currentNumber.ToString() + " / " + maxNumber.ToString();
    }
    public bool IfCurrentNumberIsOk() {
        return currentNumber <= maxNumber && currentNumber >= 0;
    }
    public void ShipIngredients() {
        if (GlobalGameVar.Instance().money >= currentMoney) {
            Invoke("GetIngredients", 3);
            GlobalGameVar.Instance().ChangeMoney(GlobalGameVar.Instance().money - currentMoney);
        }
    }
    void GetIngredients() {
        foreach (GameObject g in ingredientsList) {
            GridSlot slot = g.GetComponent<GridSlot>();
            if (int.Parse(slot.inputField.text) > 0) {
                BrewingInventoryManager.instance.AddItem(slot.herb, int.Parse(slot.inputField.text));
            }
        }
        foreach (GameObject g in ingredientsList) {
            GridSlot slot = g.GetComponent<GridSlot>();
            slot.inputField.text = "0";
        }
        currentMoney = 0;
    }
}
