using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public float m_Speed = 3.0f;
    
    private void Update(){
        float x = Input.GetAxis("Horizontal") * m_Speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * m_Speed * Time.deltaTime;
        transform.Translate(new Vector3(x, 0.0f, z));
    }
}
