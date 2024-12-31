using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string[] modes;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW ModeData = new WWW("http://localhost/pikachudbver2/ModeData.php");
        yield return ModeData;
        string ModeDataString = ModeData.text;
        print (ModeDataString);
        modes = ModeDataString.Split(';');
        print(GetDataValue(modes[0], "ModeID:"));
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if(value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
