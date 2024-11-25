using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    public int currentSceneNumber { get; private set; } = 0;

    private void Awake()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex; 
    }

    public void LoadAnyScene(int sceneIndexNumber)
    {
        SceneManager.LoadScene(sceneIndexNumber); 
    }
}
