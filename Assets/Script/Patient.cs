using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] SicknessClass sickness;
    [SerializeField] GameObject description;
    bool canBeCured = false;
    bool canBeClick = false;
    BrewingInventoryManager inventory;
    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = sickness.GetDescription();
        inventory = BrewingInventoryManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cure() {
        if (canBeCured) {
            textMesh.text = "Hurray";
        }
    }
    private void OnMouseOver() {
        description.SetActive(true);
        canBeClick = true;
        if (BrewingInventoryManager.instance.ContainPotionCanCure(sickness)) {
            canBeCured = true;
        }
    }
    private void OnMouseExit() {
        description.SetActive(false);
        canBeClick = false;
    }
    private void OnMouseDown() {
        if (canBeClick) {
            Cure();
        }
    }
}
