using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public HealthBar m_HealthBar;
    public GameObject m_GameOver;

    private void Update() {
        if(m_HealthBar.m_Value <= 0)
            m_GameOver.SetActive(true);
    }
}
