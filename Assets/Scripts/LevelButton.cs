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
        m_Button.onClick.AddListener(() => ShowDetails(true));
    }

    public void Unlocked(){
        m_Level.locked = false;
        ShowDetails(false);
    }

    public void ShowDetails(bool select){
        if(select && !m_Level.locked) 
            SelectLevelManager.Instance.LevelSelected = m_Level.sceneName;
        else
            SelectLevelManager.Instance.LevelSelected = null;

        SelectLevelManager.Instance.m_DetailsImage.sprite = m_Level.normalPicture;       
        SelectLevelManager.Instance.m_LockedImage.color = new Color(1,1,1, m_Level.locked ? 1 : 0);
        SelectLevelManager.Instance.m_DetailsDescription.text = m_Level.description;
        SelectLevelManager.Instance.m_DetailsTitle.text = m_Level.title;
    }
}
