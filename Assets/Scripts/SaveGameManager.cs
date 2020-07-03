using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public bool m_UseCrypto;
    public string m_Filename = "save.sav";

    private List<ISerializable> m_Data = new List<ISerializable>();
    private byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    private byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    public void Register(ISerializable data)
    {
        m_Data.Add(data);
    }

    public void Save()
    {
        var path = $"{Application.dataPath}/{m_Filename}";
        var data = new JObject();
        foreach (var obj in m_Data)
            data.Add(obj.GetKey(), obj.Serialize());

        if (m_UseCrypto)
        {
            File.WriteAllBytes(path, Encrypt(data.ToString()));
        }
        else
        {
            var write = new StreamWriter(path);
            write.WriteLine(data.ToString());
            write.Close();
        }
    }

    public void Load()
    {
        var path = $"{Application.dataPath}/{m_Filename}";
        var json = string.Empty;
        if (File.Exists(path))
        {
            
            if (m_UseCrypto)
            {
                var decryptedData = File.ReadAllBytes(path);
                json = Decrypt(decryptedData);
            }
            else
            {
                var reader = new StreamReader(path);
                json = reader.ReadToEnd();
                reader.Close();
            }

            Debug.Log(json);
            var data = JObject.Parse(json);
            foreach (var obj in m_Data)
                obj.Deserialize(data[obj.GetKey()].ToString());
        }
    }

    public byte[] Encrypt(string json)
    {
        var aes = new AesManaged();
        aes.Padding = PaddingMode.Zeros;
        var encryptor = aes.CreateEncryptor(key, iv);
        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        var writer = new StreamWriter(cryptoStream);

        writer.WriteLine(json);
        writer.Flush();

        var data = memoryStream.ToArray();

        writer.Close();
        cryptoStream.Close();
        memoryStream.Close();
       
        return data;
    }

    public string Decrypt(byte[] data)
    {
        var aes = new AesManaged();
        aes.Padding = PaddingMode.Zeros;
        var decryptor = aes.CreateDecryptor(key, iv);
        var memoryStream = new MemoryStream(data);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        var readerStream = new StreamReader(cryptoStream);
        var json = readerStream.ReadToEnd();
        
        readerStream.Close();
        cryptoStream.Close();
        memoryStream.Close();
        return json;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }
}
