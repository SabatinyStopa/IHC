using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private static readonly char SEPARATOR = '=';
    private Dictionary<string, string> m_Map = new Dictionary<string, string>();
    public SystemLanguage m_DefaultLanguage = SystemLanguage.English;
    private SystemLanguage m_Language;

    public static TextManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("language"))
            {
                SetLanguage(Application.systemLanguage.ToString());
            } 
            else
            {
                var language = PlayerPrefs.GetString("language");
                SetLanguage(language);
            }
        }
    }

    public void SetLanguage(string language)
    {
        var file = Resources.Load<TextAsset>(language);
        if (file == null)
        {
            language = m_DefaultLanguage.ToString();
            file = Resources.Load<TextAsset>(language);
        }

        PlayerPrefs.SetString("language", language);
        PlayerPrefs.Save();

        m_Map.Clear();

        foreach (var line in file.text.Split('\n'))
        {
            var prop = line.Split(SEPARATOR);
            m_Map.Add(prop[0], prop[1]);
        }
    }

    public List<string> GetAllLanguages(){
        var langs = Resources.FindObjectsOfTypeAll<TextAsset>();
        return langs.Select(x => x.name).ToList();
    }
    
    public string Translate(string key)
    {
        if (m_Map.ContainsKey(key))
            return m_Map[key];

        return key;
    }
}
