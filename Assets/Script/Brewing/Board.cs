using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    BoardTile[] boardTileArr;
    Dictionary<Element, int> elementNumber;
    [SerializeField] BrewingInventoryManager brewingInventoryManager;
    [SerializeField] Sprite curePotionIcon;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        boardTileArr = new BoardTile[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            boardTileArr[i] = transform.GetChild(i).GetComponent<BoardTile>();
        }
        elementNumber = new Dictionary<Element, int>() {
            {Element.None, 0},
            {Element.Yellow, 0},
            {Element.Orange, 0},
            {Element.Red, 0},
            {Element.Purple, 0},
            {Element.Blue, 0},
            {Element.Green, 0}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateElement() {
        if (brewingInventoryManager.CheckIfPotionIsFull()) return;
        foreach (BoardTile tile in boardTileArr) {
            if (tile.element != Element.None) {
                elementNumber[tile.element] += tile.value;
            }
        }
        MusicController.instance.PlaySound(5, 0.5f);
        BrewNewPotion();
        elementNumber[Element.Yellow] = 0;
        elementNumber[Element.Orange] = 0;
        elementNumber[Element.Red] = 0;
        elementNumber[Element.Purple] = 0;
        elementNumber[Element.Blue] = 0;
        elementNumber[Element.Green] = 0;
        ClearBoard();
    }
    public void BrewNewPotion() {

            CurePotionClass curePotionClass = ScriptableObject.CreateInstance<CurePotionClass>();
            string name = "CurePotionEl" 
                            + "Y" + elementNumber[Element.Yellow]
                            + "O" + elementNumber[Element.Orange]
                            + "R" + elementNumber[Element.Red]
                            + "P" + elementNumber[Element.Purple]
                            + "B" + elementNumber[Element.Blue]
                            + "G" + elementNumber[Element.Green]; 
            if (!GlobalGameVar.Instance().curePotionDic.ContainsKey(name)) {
                curePotionClass.SetElementValue(Element.Yellow, elementNumber[Element.Yellow]);
                curePotionClass.SetElementValue(Element.Orange, elementNumber[Element.Orange]);
                curePotionClass.SetElementValue(Element.Red, elementNumber[Element.Red]);
                curePotionClass.SetElementValue(Element.Purple, elementNumber[Element.Purple]);
                curePotionClass.SetElementValue(Element.Blue, elementNumber[Element.Blue]);
                curePotionClass.SetElementValue(Element.Green, elementNumber[Element.Green]);
                curePotionClass.SetSprite(curePotionIcon);
                curePotionClass.SetName(name);
                SaveToDic(curePotionClass, name);
            } else {
                curePotionClass = LoadFromDic(name);
            }

            brewingInventoryManager.AddItem(curePotionClass, 1);
            brewingInventoryManager.CopyInventoryToTemp();
        
    }
    public void ClearBoard() {
        if (boardTileArr != null) {
            foreach (BoardTile boardTile in boardTileArr) {
                boardTile.ClearAll();
            }
        }
    }
    public void SaveToDic(CurePotionClass data, string name)
    {
        GlobalGameVar.Instance().curePotionDic.Add(name, data);
        Debug.Log("Saved " + name +" to Dictionary");
    }
    CurePotionClass LoadFromDic(string name)
    {
        CurePotionClass data = GlobalGameVar.Instance().curePotionDic[name];
        Debug.Log("Get from Dictionary successfully");
        return data;
    }

}
