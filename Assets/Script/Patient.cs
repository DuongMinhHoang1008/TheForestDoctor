using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI timeLeft;
    [SerializeField] SicknessClass sickness;
    [SerializeField] GameObject description;
    [SerializeField] GameObject notOkIcon;
    [SerializeField] float maxTime = 60;
    [SerializeField] Animator bed;
    [SerializeField] Animator patient;
    [SerializeField] Sprite cryIcon;
    [SerializeField] Sprite happyIcon;
    [SerializeField] Sprite dieIcon;
    [SerializeField] GameObject iconBubble;
    float currentTime;
    bool canBeCured = false;
    CurePotionClass curePotion;
    const float minTime = 60f;
    bool noticed = false;
    bool noticedDie = false;
    // Start is called before the first frame update
    void Start()
    {
        sickness = GameManager.instance.GetRandomSickness();
        textMesh.text = sickness.GetDescription();
        maxTime = sickness.level * minTime;
        currentTime = maxTime + 5f;
        description.SetActive(false);
        iconBubble.SetActive(false);
        PlayAnimationIn();
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
                SetBubbleIcon(dieIcon);
                if (!noticedDie) {
                    GameManager.instance.NewNotification("Có bệnh nhân tèo rồi");
                    noticedDie = true;
                    Invoke("GameOver", 2);
                }
                return;
            }
            if (currentTime < 30 && !noticed) {
                GameManager.instance.NewNotification("Có bệnh nhân sắp tèo");
                noticed = true;
            }
            int minute = (int) currentTime / 60;
            int second = (int) currentTime % 60;
            timeLeft.text = 
                ((minute < 10) ? "0" : "") + minute.ToString() + ":" 
                + ((second < 10) ? "0" : "") + second.ToString();
        }
    }
    public void Cure() {
        if (canBeCured) {
            MusicController.instance.PlaySound(3, 0.25f);
            GlobalGameVar.Instance().ChangeMoney(GlobalGameVar.Instance().money + (int) Mathf.Pow(3, sickness.level - 1) * 50);
            BrewingInventoryManager.instance.GetPotion(curePotion);
            sickness = null;
            description.SetActive(false);
            SetBubbleIcon(happyIcon);
            Invoke("PlayAnimationOut", 2f);
        }
    }

    public void NewPatient() {
        PlayAnimationIn();
        sickness = GameManager.instance.GetRandomSickness();
        textMesh.text = sickness.GetDescription();
        maxTime = minTime * Mathf.Pow(3, sickness.level - 1);
        currentTime = maxTime + 4f;
    }

    void PlayAnimationIn() {
        GameManager.instance.NewNotification("Có bệnh nhân mới");
        MusicController.instance.PlaySound(4, 0.1f);
        patient.SetTrigger("In");
        description.SetActive(false);
        Invoke("GetPatient", 2.6f);
    }
    void GetPatient() {
        MusicController.instance.PlaySound(1, 0.1f);
        bed.SetTrigger("Get");
        description.SetActive(true);
        iconBubble.SetActive(true);
        SetBubbleIcon(cryIcon);
    }

    void PlayAnimationOut() {
        MusicController.instance.PlaySound(4, 0.1f);
        bed.SetTrigger("Throw");
        patient.SetTrigger("Out");
        iconBubble.SetActive(false);
        Invoke("NewPatient", 12 + Random.Range(-3, 5));
    }
    void SetBubbleIcon(Sprite icon) {
        iconBubble.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    void GameOver() {
        GameManager.instance.GameOver();
    }
}
