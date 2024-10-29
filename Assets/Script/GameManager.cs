using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    [SerializeField] TextMeshProUGUI showMoney;
    [SerializeField] SicknessClass[] sicknesses;
    [SerializeField] GameObject patientList;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showMoney.text = GlobalGameVar.Instance().money.ToString();
    }
    public SicknessClass[] GetSicknesses() {
        return sicknesses;
    }
    public void SetBedActive(int number) {
        patientList.transform.GetChild(number - 1).gameObject.SetActive(true);
    }
}
