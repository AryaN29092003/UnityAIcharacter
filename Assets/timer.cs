using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    // Start i
    public TMP_Text timerText;  // Reference to the UI Text component
    private float Timer = 0.0f;  // Timer values called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
            Timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(Timer / 60);
            int seconds = Mathf.FloorToInt(Timer % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}
