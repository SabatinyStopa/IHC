using Newtonsoft.Json.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, ISerializable{
    public float m_Life = 0;
    private void Start(){
        SaveGameManager.Instance.Register(this);
    }

    private class SaveData{
        public Vector3 position;
        public float life;


        public SaveData(Vector3 position, float life){
            this.position = position;
            this.life = life;
        }
    }
 
    public string GetKey(){
        return name;
    }

    public JObject Serialize(){
        var data = new SaveData(transform.position, m_Life);
        var json = JsonUtility.ToJson(data);
        return JObject.Parse(json);
    }

    public void Deserialize(string json){
        var data = JsonUtility.FromJson<SaveData>(json);
        transform.position = data.position;
        m_Life = data.life;
    }
}