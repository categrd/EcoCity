using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using SVS;
using System;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Debug = UnityEngine.Debug;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;
using UnityEditor.UI;
using TMPro.EditorUtilities;
using System.Data.Common;
using Unity.VisualScripting;

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
    private float[] results = new float[19]; 
        
    private bool isNewGame = true;
    private string pythonExecutable = "code/dist/Classifier"; 
    private string pythonScript = "code/Classifier.py";
    private string pythonFunction = "prediction";
    private string arguments;
    private string cluster;
    
    private float[] EduLevel = new float[6];

 

        
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


        
    }

    public void GetDropdownValue()
    {
        for (int i = 0; i < dropdown.Length; i++)
        {
            int pickedEntryIndex = (int)dropdown[0].value;
            string selectedOption = dropdown[0].options[pickedEntryIndex].text;
            Debug.Log(selectedOption);
            
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

        string pythonExecutable = "Assets/Scripts/dist/Classifier";
        string pythonScript = "Assets/Scripts/Classifier.py";
        string args = "results";

        arguments = $"{pythonScript} {args} {results}";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonExecutable,
            Arguments = args,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = psi })
        {
            process.Start();
            cluster = process.StandardOutput.ReadToEnd();

            string c = cluster.ToString();
            Debug.Log('c'+ c); // Qui ci sarà il cluster a cui appartiene il giocatore
            process.WaitForExit();
        }

        // Qui cambiamo la scena (da capire come passare info del cluster)
        SceneManager.LoadSceneAsync(1);
    }

}
/*
// Start is called before the first framepdate
void Start()
    {
        buttonList = new List<Button> {
            play,
            quit
        };

        AddButtonClickListener(play, () => { NewGame?.Invoke(); });
        AddButtonClickListener(quit, () => { QuitGame?.Invoke(); });


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
#if isNewGame
        questionPanel.SetActive(true);
        isNewGame = false;
#else
        SceneManager.LoadSceneAsync(1);
        
    #endif
    }



    /*public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }*/


