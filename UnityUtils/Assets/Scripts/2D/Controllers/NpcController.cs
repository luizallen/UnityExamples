using UnityEngine;

public class NpcController : MonoBehaviour
{
    [Header("Movement")]
    public bool thenWalking;
    public float speed;

    public Animator npcAnimator;

    Vector2 movement = Vector2.zero;
    Rigidbody2D rgb2D;

    void Start()
    {
        thenWalking = true;
        speed = 1;
        rgb2D = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    void Update()
    {
        if (thenWalking)
            Move();
    }

    void Move()
    {
        rgb2D.MovePosition(rgb2D.position + movement * speed * Time.fixedDeltaTime);
    }

    void ChangeDirection()
    {
        var direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                //Walking to the right
                movement = Vector2.right;
                break;
            case 1:
                //Walking to the up
                movement = Vector2.up;
                break;
            case 2:
                //Walking to the left
                movement = Vector2.left;
                break;
            case 3:
                //Walking to the down
                movement = Vector2.down;
                break;
            default:
                break;
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        npcAnimator.SetFloat("input_x", movement.x);
        npcAnimator.SetFloat("input_y", movement.y);
        npcAnimator.SetBool("isWalking", true);
        npcAnimator.SetBool("isRunning", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var temp = movement;
        ChangeDirection();

        var loop = 0;

        while (temp == movement && loop < 5)
        {
            loop++;
            ChangeDirection();
        }
    }
}
