using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Custom class to store the resolution 
/// </summary>
[System.Serializable]


public class CustomResolution
{
    
    public int width;
    public int height;

    public CustomResolution(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}

public class TakeScreenshot : MonoBehaviour
{
    public string directoryPath;

    public List<CustomResolution> resulutionList;
    public KeyCode keyToPress = KeyCode.S;

    readonly string filePrefix = "Screenshot";
    public readonly string folderName = "Screenshots";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //when key for screenshot is pressed the app pauses
        if (Input.GetKeyDown(keyToPress))
        {
            Time.timeScale = 0;
            StartCoroutine(Take());
        }
    }

    /// <summary>
    /// Set the corresponding resolution, refresh the view and take the screenshot and save it
    /// </summary>
    /// <returns></returns>
    IEnumerator Take()
    {
        
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        int i = 0;
        while (i < resulutionList.Count)
        {
            int scale = 1;
            int width = resulutionList[i].width;
            int height = resulutionList[i].height;
            Screen.SetResolution(width, height, false);
            yield return null;

            while (Screen.height != height)
            {
                Debug.Log("\n " + Screen.height + " " + height);
                scale *= 2;
                width = Mathf.RoundToInt((float)width / 2);
                height = Mathf.RoundToInt((float)height / 2);
                Screen.SetResolution(width, height, false);
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot(directoryPath+ "/" + filePrefix + "-" + (width * scale) + "x" + (height * scale) + "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", scale);
            i++;
        }
        Time.timeScale = 1;
    }
}
