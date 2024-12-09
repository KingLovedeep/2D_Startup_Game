using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManagment : MonoBehaviour
{
    // Animation
    [Header("ANIMATION")]
    private Animator pauseMenuAnim;
    public string pauseAnimParam;
    public string resumeAnimParam;
    public string settingPanelAnimParam; 

    private GameObject pauseMenuObject; 

    private Player_Control playerControlSystem;
    private SceneManagment sceneManager;

    // Start is called before the first frame update
    private void Start()
    {
        // Get Componenets 
        pauseMenuAnim = GetComponent<Animator>();

        // Find Referenaces 
        pauseMenuObject = transform.Find("Pause Menu").gameObject;
        playerControlSystem = GameObject.FindWithTag("Player").GetComponent<Player_Control>();
        sceneManager = GameObject.Find("Scene Manager").GetComponent<SceneManagment>();
        Debug.Log("Work");
    }

    public void GamePause()
    {
        Debug.Log("Game Pause"); 
        // Pause Menu Show
        pauseMenuObject.SetActive(true);
        pauseMenuAnim.SetBool(pauseAnimParam, true);

        // Stop Player To Move 
        playerControlSystem.CanPlayerMove(false); 
    }

    public IEnumerator GameResume()
    {
        Debug.Log("Game Resume");

        // Pause Menu Hide
        pauseMenuAnim.SetBool(pauseAnimParam, false); 
        pauseMenuAnim.SetBool(resumeAnimParam, true); 
        pauseMenuAnim.SetBool(settingPanelAnimParam, false); 
        pauseMenuObject.SetActive(false);

        // Let player move 
        playerControlSystem.CanPlayerMove(true);

        yield return new WaitForSeconds(0.1f);

        pauseMenuAnim.SetBool(settingPanelAnimParam, false); 
        pauseMenuAnim.SetBool(pauseAnimParam, false);
        pauseMenuAnim.SetBool(resumeAnimParam, false);
    }

    public void RestartLevel()
    {
        sceneManager.LoadAnyScene(sceneManager.currentSceneNumber); 
    }

    public void BackToLevelScene()
    {
        sceneManager.LoadAnyScene(2); 
    }

    public void OpenSettingPanel()
    {
        SettingsPanel(true); 
    }
    public void CloseSettingPanel()
    {
        SettingsPanel(false); 
    }

    private void SettingsPanel(bool open_or_Close)
    {
        pauseMenuAnim.SetBool(settingPanelAnimParam, open_or_Close);
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("Pause Menu Button");

        // Disable some buttons :- resume , back , settings , restart 
        for (int i = 0; i < 4; i++)
        {
            buttonObjects[i].GetComponent<Button>().interactable = !open_or_Close; 
        }
    }
}
