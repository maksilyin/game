using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WriteFile : MonoBehaviour
{

    private static string dirPath = Application.persistentDataPath + "/" + "Save";
    private static string fileName = "save.bin";
    private static bool isCrypt = false;
    private static ushort secretKey = 0x0000;
    private static string pass = ";"

    private static string EncodeDecrypt(string str, ushort secretKey)
    {
        var ch = str.ToArray(); //преобразуем строку в символы
        string newStr = "";      //переменная которая будет содержать зашифрованную строку
        if (isCrypt)
        {
            foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
            return newStr;
        }
        else return str;
    }

    private static char TopSecret(char character, ushort secretKey)
    {
        character = (char)(character ^ secretKey); //Производим XOR операцию
        return character;
    }

    public static string FileToString()
    {
        string str = File.ReadAllText("");
        byte[] buff = System.Text.ASCIIEncoding.Default.GetBytes(str);
        str = System.Text.ASCIIEncoding.Default.GetString(buff);
        using (StreamWriter sw = new StreamWriter("", false))
        {
            sw.WriteLine(str);
        }
        return EncodeDecrypt(str, secretKey);
    }

    public static bool Serialize(System.Object save)
    {

        string path = dirPath + "/"+ fileName;
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            using (FileStream sFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                RijndaelManaged rmCrypto = new RijndaelManaged();
                byte[] key = Encoding.Default.GetBytes(pass);
                byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
                rmCrypto.Padding = PaddingMode.None;
                using (CryptoStream cryptStream = new CryptoStream(sFile, rmCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    formatter.Serialize(sFile, save);
                }
            }
        } catch { return false; }
        return true;
    }

    public static bool IssetFile (string filename)
    {
        string path = dirPath + "/" + filename;
        return File.Exists(path);
    }

   
    public async static void SerializeAsync(System.Object save)
    {
        await Task.Run(() => {
            Serialize(save);
        });
    }

    public static System.Object Deserialize()
    {
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        string path = dirPath + "/"+ fileName;
        BinaryFormatter formatter = new BinaryFormatter();
        System.Object obj;
        try
        {
            using (FileStream sFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                RijndaelManaged rmCrypto = new RijndaelManaged();
                byte[] key = Encoding.Default.GetBytes(pass);
                byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
                rmCrypto.Padding = PaddingMode.None;
                using (CryptoStream cryptStream = new CryptoStream(sFile, rmCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                   
                    obj=formatter.Deserialize(sFile);
                }
            }
            print("Десериализация успешна");
        } catch (Exception e)
        {
            obj = null;
            print("Десериализация ошибка "+e.Message);
        }
        return obj;
    }
    
}
