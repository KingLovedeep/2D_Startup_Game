using UnityEngine;

public class Player_Control : MonoBehaviour
{
    [Header("MOVEMENT")]
    private bool canPlayerMove = false; 
    public float maxRunSpeed = 0f; 
    public float maxWalkSpeed = 0f;
    private float maxMoveSpeed = 0f;
    [Space]

    [Header("PHYSICS")]
    private Rigidbody2D playerRB;
    private SpriteRenderer playerRenderer; 
    [Space]

    [Header("INPUT SYSTEM")]
    private float horizontalInput;
    private bool runInput;
    [Space]

    [Header("ANIMATION")]
    public string[] animationParameter = new string[2];
    private Animator playerAnim; 

    // Start is called before the first frame update
    void Start()
    {
        canPlayerMove = true; 

        // Get Componenets
        playerRB = GetComponent<Rigidbody2D>(); 
        playerAnim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlayerMove)
        {
            // Player PlayerMovement
            PlayerMovement();

            // Player Animation
            PlayerAnimator();

            // Player View 
            PlayerView();
        }
    }
    private void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        runInput = Input.GetKey(KeyCode.LeftShift);

        maxMoveSpeed = runInput ? maxRunSpeed : maxWalkSpeed;
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new(horizontalInput * maxMoveSpeed * Time.fixedDeltaTime, playerRB.velocity.y); 
    }

    private void PlayerView()
    {
        if(horizontalInput != 0)
        {
            if(horizontalInput > 0.01)
            {
                playerRenderer.flipX = true;    
            }
            else if(horizontalInput < -0.01)
            {
                playerRenderer.flipX = false;   
            }
        }
    }

    private void PlayerAnimator()
    {
        if(horizontalInput != 0)
        {
            Animations(animationParameter[0], true); 
        }
        else
        {
            Animations(animationParameter[0], false); 
        }
    }

    
    private void Animations(string parameterName, bool start_or_stop)
    {
        playerAnim.SetBool(parameterName, start_or_stop);
    }

    public void CanPlayerMove(bool move_or_not)
    {
        canPlayerMove = move_or_not;

        // Stop Animation 
        Animations(animationParameter[0], false);

        //Stop Moving 
        maxMoveSpeed = 0f; 
    }
}
