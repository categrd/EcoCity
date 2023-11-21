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
    public GameObject questionPanel;
    public Button play;
    public Button quit;
    public Boolean NEW_GAME = true;
    List<Button> buttonList;

    void AddButtonClickListener(Button button, UnityAction action)
    {
        button.onClick.AddListener(() =>
        {
            action.Invoke();
        });
    }


    AddButtonClickListener(play, () => { ?.Invoke(); });
    AddButtonClickListener(placeRoadButton, () => { OnRoadPlacement?.Invoke(); });




    // Start is called before the first frame update
    void Start()
    {
        buttonList = new List<Button> {
            play,
            quit
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
#if NEW_GAME
        questionPanel.SetActive(true);
#else
        SceneManager.LoadSceneAsync(1);
        
    #endif
    }



    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }
}

