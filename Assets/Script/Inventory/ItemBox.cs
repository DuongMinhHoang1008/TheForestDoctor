using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject descriptionBox;
    ItemClass item;
    GameObject oldShape;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetItem(ItemClass i) {
        item = i;
    }
    private void OnMouseEnter() {
        if (item != null) {
            descriptionBox.SetActive(true);
            descriptionBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetDescription();
            if (item is HerbClass) {
                GameObject shape = Instantiate(((HerbClass) item).GetIngredientShape(), descriptionBox.transform.GetChild(1).transform);
                shape.GetComponent<BlockGroup>().SetUnFollow();
                shape.GetComponent<BlockGroup>().SetElement(((HerbClass) item).GetElement());
                oldShape = shape;
            }
        }
    }
    private void OnMouseExit() {
        if (item != null) {
            descriptionBox.SetActive(false);
            if (oldShape != null) {
                Destroy(oldShape);
            }
        }
    }
}
