using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour{
    public static SelectLevelManager Instance { get; private set; }

    private void Awake(){
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Levels")]
    public GameObject m_LevelButton;
    public List<Level> m_Levels = new List<Level>();
    public GameObject m_LevelContent;

    [Header("Details")]
    public Image m_DetailsImage;
    public Image m_LockedImage;
    public Text m_DetailsTitle;
    public Text m_DetailsDescription;

    public string LevelSelected {get; set;}

    void Start() {
        foreach (Level level in m_Levels){
            GameObject button = Instantiate(m_LevelButton);
            button.transform.parent = m_LevelContent.transform;
            button.transform.localScale = Vector3.one;
            
            var script = button.GetComponent<LevelButton>();
            script.SetLevel(level);
            // script.ShowDetails();
        }    
    }

    public void PlayLevelSelected(){
        if(LevelSelected.Length != null)
            ScreenManager.Instance.LoadLevelLoading(LevelSelected);
    }
}

[System.Serializable]
public class Level{
    public string label;
    public string title;
    public string description;
    public Sprite normalPicture;
    public Sprite lockedPicture;
    public string sceneName;
    public bool locked;
    public float score;
}
