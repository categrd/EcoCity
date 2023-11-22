using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using SVS;
using System;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    public Action NewGame, QuitGame;

    public GameObject questionPanel;
    public Button play;
    public Button quit;
    public Button commitAnswers;
    public Slider[] answer = new Slider[4];
    public int[] results = new int[4];
        
    private bool isNewGame = true;

    private void Start()
    {
        Debug.Log("Start method called");
        // Attach the click events to the buttons
        play.onClick.AddListener(OnPlayButtonClick);
        quit.onClick.AddListener(OnQuitButtonClick);
        commitAnswers.onClick.AddListener(OnCommitAnswerClick);
    }

    public void OnPlayButtonClick()
    {
        if (isNewGame)
        {
            // Show the QuestionPanel
            if (questionPanel != null)
            {
                Debug.Log("Play Button Clicked");
                questionPanel.SetActive(true);
            }
        }
        // Otherwise, do nothing
    }

    public void OnQuitButtonClick()
    {
        // Close the game
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }

    public void OnCommitAnswerClick()
    {
        for (int i = 0; i < answer.Length; i++)
            results[i] = (int)answer[i].value;
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


