using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //for anything reading/writing data into files.. input - output :)

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Color chosenColor;

    public void SetColor(Color newColour)
    {
        chosenColor = newColour;
    }

    public Color GetColour()
    {
        return chosenColor;
    }

    public void SaveColor()
    {
        //create new savedata instance!
        SaveData saveData = new SaveData();

        //Get the right color to save
        saveData.chosenColor = chosenColor;

        //Create a varialbe to store this
        string jsonToSave = JsonUtility.ToJson(saveData);

        //create save file
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", jsonToSave); //this filename is the same, so you'll overwrite it whenever you do it 

    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/saveFile.json";

        if (File.Exists(path))
        {
            string jsonToLoad  = File.ReadAllText(path);

            SaveData loadData = JsonUtility.FromJson<SaveData>(jsonToLoad);

            chosenColor = loadData.chosenColor;
        } 


    }
}

[System.Serializable] //required for Jason utility!
class SaveData
{
    public Color chosenColor;
}