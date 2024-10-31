using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int maxNumber {get; private set;} = 5;
    [SerializeField] TextMeshProUGUI showNumber;
    [SerializeField] GameObject ingredients;
    [SerializeField] TextMeshProUGUI showSumMoney;
    [SerializeField] Slider shippingProgress;
    [SerializeField] Animator shippingGuy;
    List<GameObject> ingredientsList;
    public int currentNumber {get; private set;} = 0;
    int currentMoney = 0;
    public static Shop instance { get; private set; }
    Dictionary<HerbClass, int> ingredientNum;

    float maxShipTime;
    float currentShipTime;
    public bool isShipping { get; private set; }
    bool gobackward = false;
    private void Awake() {
        if (instance == null) {
            instance = this;
            ingredientsList = new List<GameObject>();
            for (int i = 0; i < ingredients.transform.childCount; i++) {
                ingredientsList.Add(ingredients.transform.GetChild(i).gameObject);
            }
            ingredientNum = new Dictionary<HerbClass, int>();
            foreach (GameObject ingre in ingredientsList) {
                ingredientNum.Add(ingre.GetComponent<GridSlot>().herb, 0);
            }
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        showNumber.text = currentNumber.ToString() + "/" + maxNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        showNumber.text = currentNumber.ToString() + "/" + maxNumber.ToString();
        showSumMoney.text = currentMoney.ToString();
        if (isShipping) {
            currentShipTime += Time.deltaTime;
            if (currentShipTime < maxShipTime / 2) {
                shippingProgress.value = currentShipTime / (maxShipTime / 2);
            } else if (currentShipTime >= maxShipTime / 2 && currentShipTime < maxShipTime) {
                shippingProgress.value = (maxShipTime - currentShipTime) / (maxShipTime / 2);
                if (!gobackward) {
                    gobackward = true;
                    shippingGuy.SetTrigger("Back");
                }
            } else if (currentShipTime >= maxShipTime) {
                GetIngredients();
                isShipping = false;
                gobackward = false;
                shippingGuy.SetTrigger("Stay");
                GameManager.instance.NewNotification("Hàng đã về");
                currentMoney = 0;
                currentNumber = 0;
                foreach (GameObject g in ingredientsList) {
                    ingredientNum[g.GetComponent<GridSlot>().herb] = 0;
                }
                RefreshShop();
            }
        }
    }

    public void AddNumber(int num) {
        currentNumber += num;
        if (currentNumber > maxNumber) currentNumber = maxNumber;
        if (currentNumber < 0) currentNumber = 0;
        showNumber.text = currentNumber.ToString() + "/" + maxNumber.ToString();
    }
    public void SummarizeNumber() {
        currentNumber = 0;
        currentMoney = 0;
        foreach (GameObject g in ingredientsList) {
            currentNumber += int.Parse(g.GetComponent<GridSlot>().inputField.text);
            currentMoney += g.GetComponent<GridSlot>().herb.level * 10 * int.Parse(g.GetComponent<GridSlot>().inputField.text);
            ingredientNum[g.GetComponent<GridSlot>().herb] = int.Parse(g.GetComponent<GridSlot>().inputField.text);
        }
        showNumber.text = currentNumber.ToString() + "/" + maxNumber.ToString();
    }
    public bool IfCurrentNumberIsOk() {
        return currentNumber <= maxNumber && currentNumber >= 0;
    }
    public void ShipIngredients() {
        if (GlobalGameVar.Instance().money >= currentMoney && !isShipping) {
            float shipTime = 30 * Mathf.Pow(0.75f, GlobalGameVar.Instance().upgradeDic[Upgrade.Speed]);
            StartShipping(shipTime);
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
    public void RefreshShop() {
        showNumber.text = currentNumber.ToString() + "/" + maxNumber.ToString();
        foreach (GameObject g in ingredientsList) {
            g.GetComponent<GridSlot>().inputField.text = ingredientNum[g.GetComponent<GridSlot>().herb].ToString();
        }
    }
    public void IncreaseMaxShipNumber() {
        maxNumber = (int) (5 * Mathf.Pow(1.5f, GlobalGameVar.Instance().upgradeDic[Upgrade.Number]));
    }
    void StartShipping(float shipTime) {
        maxShipTime = shipTime;
        isShipping = true;
        currentShipTime = 0;
        shippingGuy.SetTrigger("Go");
    }
    
}
