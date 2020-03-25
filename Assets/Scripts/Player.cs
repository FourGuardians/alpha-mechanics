using GameAPI.Entities;
using GameAPI.Navigation;
using UnityEngine;

public class Player : Entity2D<Player>
{
    [HideInInspector]
    public ShaderBar HealthBar;

    [HideInInspector]
    public HealthBar InkBar;

    [HideInInspector]
    public PlayerDetails Details;


    [Header("Statistics")]
    public int Ink = 20;
    public int MaxInk = 20;

    private Animator animator;

    public new void Start()
    {
        base.Start();

        Speed = 2;

        Details = GetComponent<PlayerDetails>();    
        animator = GetComponent<Animator>();
    }

    public new void Update()
    {
        base.Update();

        InkBar.ApplyInk(this);
        HealthBar.ApplyHealth(this);
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        int keys =
            ((Input.GetKey(KeyCode.W) ? 1 : 0) << 3) +
            ((Input.GetKey(KeyCode.S) ? 1 : 0) << 2) +
            ((Input.GetKey(KeyCode.A) ? 1 : 0) << 1) +
            ((Input.GetKey(KeyCode.D) ? 1 : 0) << 0);

        (int, Vector3)[] controls = new [] {
            (0b1000, Direction.North),
            (0b1001, Direction.NorthEast),
            (0b0001, Direction.East),
            (0b0101, Direction.SouthEast),
            (0b0100, Direction.South),
            (0b0110, Direction.SouthWest),
            (0b0010, Direction.West),
            (0b1010, Direction.NorthWest)
        };

        for (int i = 0; i < controls.Length; i++)
        {
            var (key, direction) = controls[i];

            if (keys != key)
                continue;

            AnimIdle(false);
            AnimDirection(i);

            transform.position += direction * Speed * Time.deltaTime;
            return;
        }

        AnimIdle(true);
    }

    private void AnimIdle(bool value) =>
        animator.SetBool("Idle", value);

    private void AnimDirection(int number) =>
        animator.SetInteger("Direction", number);
}
