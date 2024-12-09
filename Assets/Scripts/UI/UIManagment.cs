using UnityEngine;
using UnityEngine.UI;

public class UIManagment : MonoBehaviour
{
    private SceneManagment sceneManager; 
    private LoginManagment loginManager;
    private LevelManagment levelManager;
    private PauseMenuManagment pauseMenuManager;

    private void Start()
    {
        // Find Referances 
        sceneManager = GameObject.Find("Scene Manager").GetComponent<SceneManagment>(); 

        // When scene is login scene
        if(sceneManager.currentSceneNumber == 1)
        {
            loginManager = GameObject.Find("Login Manager").GetComponent<LoginManagment>();
        }

        // When scene is level scene
        if(sceneManager.currentSceneNumber == 2)
        {
            levelManager = GameObject.Find("Level Manager").GetComponent<LevelManagment>(); 
        }

        // when scene is game scene
        if(sceneManager.currentSceneNumber == 3)
        {
            pauseMenuManager = GameObject.Find("Pause").GetComponent<PauseMenuManagment>(); 
        }
    }

    #region Login Screen Buttons

    public void PressedLoginButton()
    {
        StartCoroutine(loginManager.EmailRegister()); 
    }

    public void PressedOTPEnterButton()
    {
        loginManager.OTPEntered(); 
    }

    public void PressedGoButton()
    {
        loginManager.UsernameEntered(); 
    }

    public void PressedNextButton()
    {
        loginManager.GenderAuth(); 
    }

    public void RightButtonPressed()
    {
        loginManager.ChangeAtRight(); 
    }

    public void LeftButtonPressed()
    {
        loginManager.ChangeAtLeft();
    }

    public void SelectButtonPressed()
    {
        loginManager.CharacterSelected(); 
    }

    #endregion

    #region Level Screen Buttons
    public void LevelButtonPressed(int levelNumber)
    {
        levelManager.OpenLevel();
    }

    #endregion

    #region Pause Menu Buttons

    public void PauseButtonPressed()
    {
        pauseMenuManager.GamePause(); 
    }

    public void ResumeButtonPressed()
    {
       StartCoroutine(pauseMenuManager.GameResume()); 
    }

    public void RestartButtonPressed()
    {
        pauseMenuManager.RestartLevel(); 
    }

    public void BackButtonPressed()
    {
        pauseMenuManager.BackToLevelScene(); 
    }

    public void SettingButtonPressed()
    {
        pauseMenuManager.OpenSettingPanel(); 
    }

    public void SettingCloseButtonPressed()
    {
        pauseMenuManager.CloseSettingPanel(); 
    }
    #endregion
}
