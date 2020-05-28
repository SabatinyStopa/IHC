using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour{
    public Text m_TextUI;
    public Level m_Level;
    private Button m_Button;

    private void Awake() {
        m_Button = GetComponent<Button>();
    }


    public void SetLevel(Level level){
        m_Level = level;
        m_TextUI.text = level.label;
        m_Button.onClick.AddListener(() => ShowDetails());
    }

    public void ShowDetails(){
        SelectLevelManager.Instance.m_DetailsImage.sprite = m_Level.normalPicture;
        SelectLevelManager.Instance.m_DetailsDescription.text = m_Level.description;
    }
}
