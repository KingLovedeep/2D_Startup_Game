using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    public int currentSceneNumber { get; private set; } = 0;

    private void Awake()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex; 
    }

    #region SCENE MANAGE
    public void LoadAnyScene(int sceneIndexNumber)
    {
        SceneManager.LoadScene(sceneIndexNumber);
    }
    #endregion

    #region USER DATA MANAGE
    public void SaveUsername(string Username)
    {
        PlayerPrefs.SetString("Username", Username);
        PlayerPrefs.Save(); 
    }

    public void SaveUserGender(string gender)
    {
        PlayerPrefs.SetString("Gender", gender); 
        PlayerPrefs.Save();
    }

    #endregion
}
