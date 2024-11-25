using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagment : MonoBehaviour
{
    public int LevelNumber = 0; // for testing 

    private List<Button> levelButtons = new();
    private List<Image> lockIcons = new();

    private SceneManagment sceneManager; 

    // Start is called before the first frame update
    void Start()
    {
        // Find all locsk and level buttons
        GetLocksAndButtons();

        // Find References 
        sceneManager = GameObject.Find("Scene Manager").GetComponent<SceneManagment>(); 
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.Space))
        {
         /*   UnlockLevels(LevelNumber); */
        }
    }

    private void GetLocksAndButtons()
    {
        // Find All Level Buttons 
        GameObject[] levelButtonObjects = GameObject.FindGameObjectsWithTag("Level Btn");
        foreach(GameObject currentLevelButtonObject in levelButtonObjects)
        {
            Button currentLevelButton = currentLevelButtonObject.GetComponent<Button>();
            levelButtons.Add(currentLevelButton);
        }

        levelButtons.Reverse();

        // Find all lock Icons
        GameObject[] lockIconsObjects = GameObject.FindGameObjectsWithTag("Lock"); 
        foreach(GameObject currentIconObject in lockIconsObjects)
        {
            Image currentlockImage = currentIconObject.GetComponent<Image>();
            lockIcons.Add(currentlockImage); 
        }

        lockIcons.Reverse(); 
    }

    private void UnlockLevels(int levelNumber)
    {
        if (!levelButtons[levelNumber - 2].interactable && lockIcons[levelNumber - 2].enabled)
        {
            levelButtons[levelNumber - 2].interactable = true;
            lockIcons[levelNumber - 2].enabled = false;
        }
    }

    public void OpenLevel()
    {
        // Send details of this level to scene 
        // CODE: 

        // Load Level Scene
        sceneManager.LoadAnyScene(3); 
    }
}
