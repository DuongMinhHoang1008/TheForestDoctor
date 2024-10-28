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
    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = sickness.GetDescription();
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (BrewingInventoryManager.instance.ContainPotionCanCure(sickness)) {
            canBeCured = true;
            notOkIcon.SetActive(false);
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
    public void Cure() {
        if (canBeCured) {
            textMesh.text = "Hurray";
        }
    }
}
