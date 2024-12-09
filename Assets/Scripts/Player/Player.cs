using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField]
    private float moveSpeed = 1;
    private BoxCollider2D z_BoxCollider;
    private Animator z_Animator;

    public HealthManager healthManager;

    //Methods
    private void Start()
    {
        z_BoxCollider = GetComponent<BoxCollider2D>();
        z_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //Get the input X,Y 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //Cache it in a Vector
        Vector2 moveDelta = new Vector2(moveX, moveY);

        //Flip the player according to the move direction
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //Collision check
        RaycastHit2D castResult;

        //Check if we are hitting something in the X Axis
        castResult = Physics2D.BoxCast(
            transform.position,
            z_BoxCollider.size, 0,
            new Vector2(moveX, 0),
            Mathf.Abs(moveX * Time.fixedDeltaTime * moveSpeed),
            LayerMask.GetMask("Enemy", "BlockMove")
        );
        if (castResult.collider)
        {
            moveDelta.x = 0;            //STOP MOVING ON THE X AXIS
        }

        //Check if we are hitting something in the Y Axis
        castResult = Physics2D.BoxCast(
            transform.position,
            z_BoxCollider.size, 0,
            new Vector2(0, moveY),
           Mathf.Abs(moveY * Time.fixedDeltaTime * moveSpeed),
           LayerMask.GetMask("Enemy", "BlockMove")
        );
        if (castResult.collider)
        {
            moveDelta.y = 0; //STOP MOVING ON THE Y AXIS
        }

        bool isWalking = moveDelta.magnitude > 0;
        z_Animator.SetBool("isWalking", isWalking);
        transform.Translate(moveDelta * Time.fixedDeltaTime * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        if (healthManager != null)
        {
            healthManager.health -= damage;
            if (healthManager.health <= 0)
            {
                Debug.Log("Game Over: El jugador ha muerto.");
            }
        }
    }
}
