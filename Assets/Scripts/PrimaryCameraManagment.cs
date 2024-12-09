using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrimaryCameraManagment : MonoBehaviour
{
    [Header("OFFSETS")]
    public Vector3 insideOffset = Vector3.zero;
    public Vector3 outsideOffset = Vector3.zero;

    // Player componenets referances 
    private Transform protagonistTransform;
    private LocationSwitch locationSwitchingSystem;

    // Camera's variables 
    private float currentXPosition; 
    private float currentYPosition;
    public bool canFollowProtagonist = false;

    [Header("END POINT")]
    public Transform endPoint;
    public float distanceFromPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        canFollowProtagonist = true; 

        protagonistTransform = GameObject.FindWithTag("Player").transform; 
        locationSwitchingSystem = protagonistTransform.GetComponent<LocationSwitch>();  
    }

    private void Update()
    {
        // check player current position 
        RangeOfCameraToFollowPlayer();

        // Set position for end point 
        SetEndPoint(); 
    }

    private void LateUpdate()
    {
        FollowPlayerPosition(); 
    }

    private void FollowPlayerPosition()
    {
        if(canFollowProtagonist)
        {
            if (locationSwitchingSystem != null)
            {
                // when player is inside building
                if (locationSwitchingSystem.isPlayerInHouse)
                {
                    Vector3 newPosition = new(protagonistTransform.position.x + insideOffset.x, protagonistTransform.position.y + insideOffset.y, insideOffset.z);
                    AssigningNewPosition(newPosition);
                }
                // when player is outside building
                else
                {
                    Vector3 newPosition = new(protagonistTransform.position.x + outsideOffset.x, protagonistTransform.position.y + outsideOffset.y, outsideOffset.z);
                    AssigningNewPosition(newPosition);
                }
            }
        }
     /*   else
        {
            if (locationSwitchingSystem != null)
            {
                // when player is inside building
                if (locationSwitchingSystem.isPlayerInHouse)
                {
                    Vector3 newPosition = new(this.transform.position.x + insideOffset.x, protagonistTransform.position.y + insideOffset.y, insideOffset.z);
                    AssigningNewPosition(newPosition);
                }
                // when player is outside building
                else
                {
                    Vector3 newPosition = new(this.transform.position.x + outsideOffset.x, protagonistTransform.position.y + outsideOffset.y, outsideOffset.z);
                    AssigningNewPosition(newPosition);
                }
            }
        }*/
    }

    private void RangeOfCameraToFollowPlayer()
    {
        float currentXPosition = protagonistTransform.position.x; 

        if(!locationSwitchingSystem.isPlayerInHouse)
        {
            // Out of range
            if(currentXPosition <= 4.2f)
            {
                canFollowProtagonist = false; 
            }
            // In range 
            else
            {
                canFollowProtagonist = true; 
            }
        }
    }
    
    private void AssigningNewPosition(Vector3 _position_)
    {
        this.transform.position = _position_;
    }

    // Change instant position of camera when player transform its location 
    public void AssigningInstantPosition(float newXPosition, float newYPosition)
    {
        Vector3 newPosition = new(newXPosition, newYPosition, outsideOffset.z); 
        AssigningNewPosition(newPosition);  
    }

    private void SetEndPoint()
    {
        Vector2 newPosition = new(protagonistTransform.position.x - distanceFromPlayer, 0);
        endPoint.position = newPosition; 
    }
}
