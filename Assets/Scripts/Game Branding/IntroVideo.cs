using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer introVideoPlayer;

    private SceneManagment sceneManager;

    private void Start()
    {
        sceneManager = GameObject.Find("Scene Manager").GetComponent<SceneManagment>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(introVideoPlayer != null)
        {
            if (introVideoPlayer.isPlaying)
            {
                if (introVideoPlayer.time >= introVideoPlayer.length - 0.5f)
                {
                    LoadLoginScene(); 
                }
            }
        }
    }

    private void LoadLoginScene()
    {
        sceneManager.LoadAnyScene(1); 
    }
}
