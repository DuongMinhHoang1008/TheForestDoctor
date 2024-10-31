using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    public int score { get; private set; } = 0;
    public int highscore { get; private set; } = 0;
    [SerializeField] TextMeshProUGUI showMoney;
    [SerializeField] SicknessClass[] sicknesses;
    [SerializeField] GameObject patientList;
    [SerializeField] GameObject notificationHang;
    [SerializeField] GameObject notificationPref;
    private void Awake() {
        if (instance == null) {
             highscore = GlobalGameVar.Instance().highscore;
            instance = this;
        }
        //DontDestroyOnLoad(gameObject);
    }
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showMoney != null) showMoney.text = GlobalGameVar.Instance().money.ToString();
    }
    public SicknessClass[] GetSicknesses() {
        return sicknesses;
    }
    public void SetBedActive(int number) {
        patientList.transform.GetChild(number - 1).gameObject.SetActive(true);
    }
    public SicknessClass GetRandomSickness() {
        int randIndex = Random.Range(0, sicknesses.Length);
        int playerLevelForFive = (int) Mathf.Ceil(GlobalGameVar.Instance().playerLevel / 5f);
        int playerLevel = GlobalGameVar.Instance().playerLevel;
        
        while (true) {
            float measure = MeasuringLevel(playerLevel, sicknesses[randIndex].level);
            int randMeasure = Random.Range(1, 100);
            if (playerLevelForFive < sicknesses[randIndex].level) {
                if (randMeasure > measure) {
                    break;
                }
            } else if (playerLevelForFive > sicknesses[randIndex].level) {
                if (randMeasure < 4 * measure) { 
                    break;
                }
            } else {
                break;
            }
            randIndex = Random.Range(0, sicknesses.Length);
        }

        return sicknesses[randIndex];
    }
    float MeasuringLevel(int playerLevel, int level) {
        return 100f * level * 0.25f / (playerLevel / 5f);
    }
    public void MoreScore(int value) {
        score += value;
    }
    public void NewNotification(string str) {
        MusicController.instance.PlaySound(0, 0.25f);
        GameObject notice = Instantiate(notificationPref, notificationHang.transform);
        notice.GetComponent<Notification>().SetNoticeContent(str);
    }
    public void GameOver() {
        if (!gameOver) {
            gameOver = true;
            if (score > highscore) {
                highscore = score;
                GlobalGameVar.Instance().highscore = highscore;
            }
            MusicController.instance.PlaySound(6, 0.25f);
            SceneManager.LoadScene("GameOver");
        }
    }
}
