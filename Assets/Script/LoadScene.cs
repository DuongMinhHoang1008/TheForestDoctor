using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<TextMeshProUGUI>() != null) {
            GetComponent<TextMeshProUGUI>().text = "Kỉ lục: " + GlobalGameVar.Instance().highscore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToPlay() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ToStart() {
        SceneManager.LoadScene("Start");
    }
}
