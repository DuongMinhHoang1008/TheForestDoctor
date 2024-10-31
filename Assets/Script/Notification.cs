using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI content;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestroy", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNoticeContent(string str) {
        content.text = str;
    }
    void SelfDestroy() {
        Destroy(gameObject);
    }
}
