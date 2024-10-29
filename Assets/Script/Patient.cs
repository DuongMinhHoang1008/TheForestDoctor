using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI timeLeft;
    [SerializeField] SicknessClass sickness;
    [SerializeField] GameObject description;
    [SerializeField] GameObject notOkIcon;
    [SerializeField] float maxTime = 300;
    float currentTime;
    bool canBeCured = false;
    CurePotionClass curePotion;
    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = sickness.GetDescription();
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (sickness != null) {
            curePotion = BrewingInventoryManager.instance.ContainPotionCanCure(sickness);
            if (curePotion != null) {
                canBeCured = true;
                notOkIcon.SetActive(false);
            } else {
                canBeCured = false;
                notOkIcon.SetActive(true);
            }

            if (currentTime >= 0) {
                currentTime -= Time.deltaTime;
            } else {
                textMesh.text = "Oh no";
            }

            int minute = (int) currentTime / 60;
            int second = (int) currentTime % 60;
            timeLeft.text = minute.ToString() + ":" + second.ToString();
        }
    }
    public void Cure() {
        if (canBeCured) {
            textMesh.text = "Hurray";
            GlobalGameVar.Instance().ChangeMoney(GlobalGameVar.Instance().money + sickness.level * 50);
            BrewingInventoryManager.instance.GetPotion(curePotion);
            sickness = null;
            description.SetActive(false);
            Invoke("NewPatient", 10);
        }
    }

    public void NewPatient() {
        SicknessClass[] sicknesses = GameManager.instance.GetSicknesses();
        int rand = Random.Range(0, sicknesses.Length);
        sickness = sicknesses[rand];
        description.SetActive(true);
        textMesh.text = sickness.GetDescription();
        currentTime = maxTime;
    }
}
