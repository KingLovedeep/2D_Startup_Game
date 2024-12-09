using UnityEngine;

public class PlayerCollisionDetaction : MonoBehaviour
{
    private LocationSwitch playerLocationSwitchSystem;

    private void Start()
    {
        playerLocationSwitchSystem = GetComponent<LocationSwitch>();   
    }

    private void OnTriggerEnter2D(Collider2D collisionDetails)
    {
        if(collisionDetails != null)
        {
            if(collisionDetails.CompareTag("Location Icon"))
            {
                Debug.Log("Location Icon");
                Transform newlocationPostion = collisionDetails.transform.parent.transform.Find("Switch Location Position").transform; 
                playerLocationSwitchSystem.GetLocation_and_CollidedIcon(newlocationPostion); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collisionDetails)
    {
        if(collisionDetails != null)
        {
            if(collisionDetails.CompareTag("Location Icon"))
            {
                Debug.Log("Exit Location Icon"); 
            }
        }
    }
}
