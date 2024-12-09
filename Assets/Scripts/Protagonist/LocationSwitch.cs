using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LocationSwitch : MonoBehaviour
{
    private GameObject playerTransitionShadeObject;

    // New Location Details 
    private Transform newLocationPosition;
    private Camera playerCamera; 

    private Player_Control playerControlSystem;
    private PrimaryCameraManagment mainCameraManage; 

    public bool isPlayerInHouse { get; private set; } = true; 

    // Start is called before the first frame update
    void Start()
    {
        // Get Componenets 
        playerControlSystem = GetComponent<Player_Control>();

        // Find referances 
        playerTransitionShadeObject = GameObject.Find("Canvas").transform.Find("Player Transition Shade").gameObject;
        playerCamera = GameObject.Find("Primary Camera").GetComponent<Camera>(); 
        mainCameraManage = playerCamera.transform.GetComponent<PrimaryCameraManagment>(); 
    }

    public void GetLocation_and_CollidedIcon(Transform _newLocationPosition_)
    {
        newLocationPosition = _newLocationPosition_; 

        // Transform player to new location 
        StartCoroutine(TransformPlayer());
    }

    private IEnumerator TransformPlayer()
    {
        playerControlSystem.CanPlayerMove(false); 
        playerTransitionShadeObject.SetActive(true);

        yield return new WaitForSeconds(1f); 

        // Transform player
        this.transform.position = newLocationPosition.position;
        isPlayerInHouse = false;

        // Set camera details 
        yield return new WaitForSeconds(0.5f); 

        Camera.main.orthographicSize = 5f;
        mainCameraManage.AssigningInstantPosition(4.3f, 0.1182923f); 

        yield return new WaitForSeconds(1f); 

        playerTransitionShadeObject.SetActive(false);  
        playerControlSystem.CanPlayerMove(true);
    }
}
