using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public bool canMove = false;
    public string moveSide = null;
    public float walkSpeed = 0f;
    public float maxDistance = 0;

    private Transform playerTransform;  

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        // Find other refernced 
        playerTransform = GameObject.FindWithTag("Player").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            MoveNPC(); 
        }

        Distance_From_Player(); 
    }

    private void MoveNPC()
    {
        // Move at left
        if(moveSide == "Left")
        {
            this.transform.Translate(walkSpeed * Time.deltaTime * Vector2.left); 
        }
        // Move at right 
        else if(moveSide == "Right")
        {
            this.transform.Translate(walkSpeed * Time.deltaTime * Vector2.right); 
        }
    }

    private void Distance_From_Player()
    {
        Vector2 npcPosition = this.transform.position;
        Vector2 playerPosition = playerTransform.position; 

        float Distance = Vector2.Distance(npcPosition, playerPosition); 

        if(Distance > maxDistance)
        {
            Destroy(this.gameObject); 
        }
    }
}