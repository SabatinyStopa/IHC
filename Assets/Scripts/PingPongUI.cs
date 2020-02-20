using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongUI : MonoBehaviour{
    public float m_Range = 0.1f;
    public float m_SmoothTime = 10.0f;
    public Vector3 m_Axis = Vector3.up;
    private Vector3 m_Origin;

    private void Start() {
        m_Origin = transform.position;
    }

    private void Update() {
        transform.position = m_Origin + m_Axis * Mathf.Sin(Time.time * m_SmoothTime) * m_Range;
    }
}
