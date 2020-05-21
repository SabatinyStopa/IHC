using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog m_Dialog;
    private bool m_CanAsk;

    private void Update()
    {
        if(m_CanAsk && Input.GetButtonDown("Jump")){
            DialogManager.Instance.BeginDialog(m_Dialog);
        }
    }

    void OnTriggerEnter(Collider other) {        
        if(other.CompareTag("Player"))
        m_CanAsk = true;
    }

    void OnTriggerExit(Collider other) {
        m_CanAsk = false;    
    }
}
