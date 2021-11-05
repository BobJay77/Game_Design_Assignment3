using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    //variables for shooting
    private float previousTime;
    [SerializeField] private float shootDelay = 0.7f;
    

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        previousTime = 0;
    }
    
    void Update()
    {
        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (horizontal != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpingPower);
        }

        if (context.canceled && rigidbody2D.velocity.y > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - previousTime > shootDelay)
            {
                SpawnBulletFromPool();
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }
    }

    private void SpawnBulletFromPool()
    {
        previousTime = Time.time;
        Transform Spawnertransform = GameObject.Find("ShurikenThrowningHand").transform;
       var projectile = BasicPool.Instance.GetFromPool();
        projectile.transform.position = Spawnertransform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Spawnertransform.localScale.x * 10, 0);
        Debug.Log("Spawnbullet");
    }
}
