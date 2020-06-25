using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour{
    public AudioMixer m_Mixer;
    [Space(10)]
    public Slider m_MusicSlider;
    public Slider m_SfxSlider;
    public Slider m_BrightnessSlider;

    [Range(0.0f, 1.0f)]
    public float m_Brightness = 0.5f;
    public Color m_AmbientLight = Color.white;
    public Color m_AmbientDark = Color.black;

    private void Start(){
        Load();
    }

    public void SetBrightness(){
        m_Brightness = m_BrightnessSlider.value;
        RenderSettings.ambientLight = Color.Lerp(m_AmbientDark, m_AmbientLight, m_Brightness);
    }

    public void SetMusicVolume(){
        m_Mixer.SetFloat("musicVolume", m_MusicSlider.value);
    }

    public void SetSfxVolume(){
        m_Mixer.SetFloat("sfxVolume", m_SfxSlider.value);
    }

    public void Load(){
        m_MusicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        m_SfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
        m_Brightness = PlayerPrefs.GetFloat("brightness", 0.5f);
    }

    private void OnDisable() {
        Save();
    }
    public void Save(){
        float musicVolume = 0.75f;
        float sfxVolume = 0.75f;
        m_Mixer.GetFloat("musicVolume", out musicVolume);
        m_Mixer.GetFloat("sfxVolume", out sfxVolume);

        PlayerPrefs.SetFloat("brightness", m_Brightness);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();

    }
}
