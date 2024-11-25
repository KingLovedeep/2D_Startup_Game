using UnityEngine;

public class UIManagment : MonoBehaviour
{
    private SceneManagment sceneManager; 
    private LoginManagment loginManager;
    private LevelManagment levelManager; 

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
    }

    #region LoginScreenButtons

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

    #endregion

    #region LevelScreenButtons
    public void LevelButtonPressed(int levelNumber)
    {
        levelManager.OpenLevel();
    }

    #endregion
}
