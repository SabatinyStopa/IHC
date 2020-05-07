using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour{
   public Pause m_PauseScript;

   private void OnEnable(){
       m_PauseScript.Hide();
       m_PauseScript.enabled = false;
   }
}
