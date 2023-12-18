using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;
using System.Dynamic;
using System.Runtime;
using System.Text;
using TMPro;




using Debug = UnityEngine.Debug;
using Button = UnityEngine.UI.Button;

using Slider = UnityEngine.UI.Slider;
using Dropdown = UnityEngine.UI.Dropdown;
using System.Diagnostics;
using UnityEngine.UI;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEditor;
using UnityEditor.UI;
using Unity.VisualScripting;
using UnityEngine.XR;
using Unity.VisualScripting.FullSerializer;


public class MainMenuManager : MonoBehaviour
{
    public Action NewGame, QuitGame;

    public GameObject questionPanel;
    public Button play;
    public Button quit;
    public Button commitAnswers;
    public TMP_InputField nickname;
    public TMP_InputField income;
    

    public TMP_Dropdown[] dropdown = new TMP_Dropdown[4];
   
    public Slider[] answer = new Slider[8]; 
    public TMP_Text[] sliderText = new TMP_Text[8];
    
        
    private bool isNewGame = true;
    private string pythonExecutable = "code/dist/Classifier"; 
    private string pythonScript = "code/Classifier.py";
    private string pythonFunction = "prediction";
    private string arguments;
    private int cluster;
    
    private float[] EduLevel = new float[6];
    public string[] CCS_text = new string[7];
    public string[] HEAS_text = new string[4];

 

        
    private void Start()
    {
        Debug.Log("Start method called");
        play.onClick.AddListener(OnPlayButtonClick);
        quit.onClick.AddListener(OnQuitButtonClick);
        commitAnswers.onClick.AddListener(OnCommitAnswerClick);
        
        EduLevel[0] = 5.0F;
        EduLevel[1] = 8.0F;
        EduLevel[2] = 13.0F;
        EduLevel[3] = 18.0F;
        EduLevel[4] = 22.0F;
        EduLevel[5] = 25.0F;


        for (int i = 0; i<answer.Length; i++)
        {
            answer[i].onValueChanged.AddListener(delegate { UpdateSliderValue(); });
        }

    }

    public void UpdateSliderValue()
    {   
        for (int i = 0; i < answer.Length/2; i++)
        {
            if (answer[i].value == 0)
            {
                sliderText[i].text = "Never";
            }
            else if (answer[i].value == 1)
            {
                sliderText[i].text = "Some days";
            }
            else if (answer[i].value == 2)
            {
                sliderText[i].text = "More than half";
            }
            else if (answer[i].value == 3)
            {
                sliderText[i].text = "Almost every day";
            }
        }

        for (int i = 4; i<answer.Length; i++)
        {
            if (answer[i].value == 0)
            {
                sliderText[i].text = "Strongly disagree";
            }
            else if (answer[i].value == 1)
            {
                sliderText[i].text = "Disagree";
            }
            else if (answer[i].value == 2)
            {
                sliderText[i].text = "Somewhat disagree";
            }
            else if (answer[i].value == 3)
            {
                sliderText[i].text = "Neither agree nor disagree";
            }
            else if (answer[i].value == 4)
            {
                sliderText[i].text = "Somewhat agree";
            }
            else if (answer[i].value == 5)
            {
                sliderText[i].text = "Agree";
            }
            else if (answer[i].value == 6)
            {
                sliderText[i].text = "Strongly agree";
            }
        }
    }


    
    public void OnPlayButtonClick()
    {
        Debug.Log("Play Button Clicked");
        if (isNewGame)
        {
            // Show the QuestionPanel
            if (questionPanel != null)
            {
                Debug.Log("Play Button Clicked");
                questionPanel.SetActive(true);
            }
        }
        else{
        SceneManager.LoadSceneAsync(1);
        }
    }

    public void OnQuitButtonClick()
    {
        // Close the game
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }


/* pd.DataFrame(data = data, columns = ['Age', 'Education', 'Income', 'Male', 'Female', 'Non-binary', 
                                         'Single', 'Married', 'Divorced', 'Widowed', 'Separated', 'Affective Symptoms', 'Rumination', 'Behavioural Symptoms', 
                                         'Anxiety Personal Impact', 'Attribution Skepticism', 'Impact Skepticism', 
                                         'Trend Skepticism', 'Response Skepticism']) 
    */
    public void OnCommitAnswerClick()
    {
        dynamic results = new float[19];
        Debug.Log("Commit Answer Button Clicked");
        results[0] = dropdown[0].value + 18;    
        results[1] = EduLevel[dropdown[3].value];
        results[2] = float.Parse(income.text);

        if (dropdown[1].value == 0)
        {
            results[3] = (float)1;
            results[4] = (float)0;
            results[5] = (float)0;
        }
        else if (dropdown[1].value == 1)
        {
            results[3] = (float)0;
            results[4] = (float)1;
            results[5] = (float)0;
        }
        else if (dropdown[1].value == 2)
        {
            results[3] = (float)0;
            results[4] = (float)0;
            results[5] = (float)1;
        }
        else{
            results[3] = (float)0;
            results[4] = (float)0;
            results[5] = (float)0;
        }

        if (dropdown[2].value == 0)
        {
            results[6] = (float)1;
            results[7] = (float)0;
            results[8] = (float)0;
            results[9] = (float)0;
            results[10] = (float)0;
        } 
        else if (dropdown[2].value == 1)
        {
            results[6] = (float)0;
            results[7] = (float)1;
            results[8] = (float)0;
            results[9] = (float)0;
            results[10] = (float)0;
        }
        else if (dropdown[2].value == 2)
        {
            results[6] = (float)0;
            results[7] = (float)0;
            results[8] = (float)1;
            results[9] = (float)0;
            results[10] = (float)0;
        }
        else if (dropdown[2].value == 3)
        {
            results[6] = (float)0;
            results[7] = (float)0;
            results[8] = (float)0;
            results[9] = (float)1;
            results[10] = (float)0;
        }
        else if (dropdown[2].value == 4)
        {
            results[6] = (float)0;
            results[7] = (float)0;
            results[8] = (float)0;
            results[9] = (float)0;
            results[10] = (float)1;
        }
        else
        {
            results[6] = (float)0;
            results[7] = (float)0;
            results[8] = (float)0;
            results[9] = (float)0;
            results[10] = (float)0;
        }

        for (int i = 0; i < dropdown.Length; i++)
            results[i+11] = (float)answer[i].value;

        predict(results);




    /*

        using (StreamWriter sw = new StreamWriter("Assets/Scripts/to_classify.csv", true, Encoding.UTF8))
        {
            string str = results[0].ToString();
            for (int i = 0; i < results.Length; i++)
            {
                str = str + "," + results[i].ToString();
            }
            sw.WriteLine(str);
        }


        
        // Path to your Python executable
        //string pythonPath = "/Library/Frameworks/Python.framework/Versions/3.11/Python";
        string pythonPath = "/Library/Frameworks/Python.framework/Versions/3.11/bin/python3.11";

        // Path to your Python script
        string pythonScriptPath = "Assets/Scripts/Classifier.py";

        // Arguments to pass to the Python script
        string arguments = "";

        // Create process start info
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"{pythonScriptPath} {arguments}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Create process and start
        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();

            // Read the output (standard output and error)
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            this.cluster = output;
            process.WaitForExit();

            // Display output and error
            Console.WriteLine("Output:");
            Console.WriteLine(output);

            Console.WriteLine("Error:");
            Console.WriteLine(error);
        }
        string[] lines = File.ReadAllLines("Assets/Scripts/prediction.csv");
        if (lines.Length == 0)
        {
            
        }
        var cluster = lines[lines.Length - 1];
        Debug.Log(cluster.ToString() + " Richiiiiii io sono quiiiii ");
*/
    
/*
        Runtime.PythonDLL = "/Library/Frameworks/Python.framework/Versions/3.11/bin/python3.11";
        PythonEngine.Initialize();
        using (Py.GIL()) // Acquire the Python GIL (Global Interpreter Lock)
        {
        
            var pythonScript = Py.Import("Classifier");
            var tmp = new PyList(results);
            var r = pythonScript.InvokeMethod("predict", new PyObject[] {tmp});

            // Access the result from the Python script
            //string[] lines = File.ReadAllLines("Assets/Scripts/prediction.csv");
            //var c = lines[lines.Length - 1];
            //Console.WriteLine(c);
            this.cluster = r.ToString();
        }
        
        // Create a new ScriptEngine
        var engine = Python.CreateEngine();
        var searchPaths = engine.GetSearchPaths();
        searchPaths.Add("/Library/Frameworks/Python.framework/Versions/3.12/lib/python3.12/site-packages");
        engine.SetSearchPaths(searchPaths);
      
        var scriptSource = engine.CreateScriptSourceFromFile("Assets/Scripts/Classifier.py");

        // Execute the Python script with the input data
        scriptSource.Execute();
        // Access the result from the Python script
        string[] lines = File.ReadAllLines("Assets/Scripts/prediction.csv");
        this.cluster = lines[lines.Length - 1];
        Debug.Log(cluster.ToString());*/
        SceneManager.LoadSceneAsync(1);
    }

    static string GetPythonLibPath()
    {
        string pythonPath = RuntimeEnvironment.GetRuntimeDirectory();

        // Check if the Lib directory exists
        string libPath = Path.Combine(pythonPath, "Lib");

        if (Directory.Exists(libPath))
        {
            return libPath;
        }

        return null;
    }

    public void predict(float[] r)
    {
        bool[] features = new bool[19];
        features[0] = false;
        features[1] = true;
        features[2] = true;
        features[3] = false;
        features[4] = false;
        features[5] = false;
        features[6] = false;
        features[7] = false;
        features[8] = false;
        features[9] = false;
        features[10] = false;
        features[11] = true;
        features[12] = true;
        features[13] = true;
        features[14] = true;
        features[15] = true;
        features[16] = true;
        features[17] = true;
        features[18] = true;
        
        float[] result = new float[10];

        int[] c = new int[3];
        c[0] = 0;
        c[1] = 0;
        c[2] = 0;


        if (r[1] < 13.0){ c[1] += 1; c[2] += 1;}
        else { c[0] += 1;}
        if (r[2] < 40000) { c[2] += 1; }
        else { c[0] += 1; c[1] += 1; }
        if (r[11] >= 2.0){ c[1] += 1; }
        else {
            if (r[11] >= 1.0){ c[2] += 1; }
            else { c[0] += 1; }
        }
        if (r[12] <= 2.0){ c[0] += 1; }
        else {
            if (r[12] >= 2.33){ c[1] += 1; }
            else { c[2] += 1; }
        }
        if (r[13] >= 3.0 ){ c[1] += 1;}
        else{
            if (r[13] >= 1.33){ c[2] += 1; }
            else { c[0] += 1; }
        }
        if (r[14] >= 3.0) { c[1] += 1; }
        else {
            if (r[14] >= 1.66){ c[2] += 1; }
            else { c[0] += 1; }
        }
        if (r[15] >= 3.0){ c[1] += 1; }
        else {
            if (r[15] >= 1.66){ c[2] += 1; }
            else { c[0] += 1; }
        }
        if (r[16] >= 3.0){ c[1] += 1; }
        else { c[2] += 1;  c[0] += 1; }
        if (r[17] >= 3.0){ c[1] += 1; }
        else { 
            if (r[17] <= 1.33){ c[0] += 1; }
            else { c[2] += 1; }
        }
        this.cluster = arg_max(c);
    }

    public int arg_max(int[] c)
    {
        int max = 0;
        int index = 0;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] > max)
            {
                max = c[i];
                index = i;
            }
        }
        return index;
    }
    

    

}

