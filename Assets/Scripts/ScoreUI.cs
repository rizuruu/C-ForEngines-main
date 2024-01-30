using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private ScoreSystem scoreSystem;
    public TMP_Text uiLabel;

    // Start is called before the first frame update
    void Awake()
    {
        scoreSystem = GetComponent<ScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        uiLabel.text = "Score: " + scoreSystem.score;
    }
}
