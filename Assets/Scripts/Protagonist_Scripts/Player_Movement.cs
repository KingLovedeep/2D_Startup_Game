using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float maxRunSpeed = 0f; 
    public float maxWalkSpeed = 0f;
    private float maxMoveSpeed = 0f; 

    private Rigidbody2D playerRB;

    private float horizontalInput;
    private bool runInput; 

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        runInput = Input.GetKey(KeyCode.LeftShift);

        maxMoveSpeed = runInput ? maxRunSpeed : maxWalkSpeed; 
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new(horizontalInput * maxMoveSpeed * Time.fixedDeltaTime, playerRB.velocity.y); 
    }
}
