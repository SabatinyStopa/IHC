using UnityEngine;
using UnityEngine.UI;

public class TextKeyUI : MonoBehaviour {
    public string m_Key;
    private Text m_TextUI;

    private void Awake() {
        m_TextUI = GetComponent<Text>();
    }

    private void Start() {
        UpdateUI();
    }

    public void UpdateUI(){
        var text = TextManager.Instance.Translate(m_Key);
        m_TextUI.text = text;
    }
}