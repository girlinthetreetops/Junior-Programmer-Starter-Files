using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;
    public MainManager mainManager;

    public void NewColorSelected(Color color)
    {

        mainManager.SetColor(color);
    }
    
    private void Start()
    {
        mainManager = FindObjectOfType<MainManager>();

        ColorPicker.Init();

        //this will call the NewColorSelected function when the color picker have a color button clicked.

        ColorPicker.onColorChanged += NewColorSelected;

        //Setting the save color in the json to make sure there is one there
        ColorPicker.SelectColor(MainManager.Instance.chosenColor);

    }

    private void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.chosenColor);

    }

    private void Exit()
    {
        MainManager.Instance.SaveColor();

#if UNITY_EDITOR
        Debug.Log("About to close the application");
        
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.quit;
#endif
    }
}
//learn: region, namespace, conditional compilation