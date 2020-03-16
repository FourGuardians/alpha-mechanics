using GameAPI.Entities;
using GameAPI.Navigation;
using UnityEngine;

public class Player : Entity2D<Player>
{
    // private Vector3 lastPos;

    public HealthBar HealthBar;
    public HealthBar InkBar => Globals.InkOverlay.HealthBar;

    public int Ink = 20;
    public int MaxInk = 20;

    private Animator animator;

    public new void Start()
    {
        base.Start();

        Speed = 2;

        animator = GetComponent<Animator>();
        // lastPos = transform.position;
    }

    public new void Update()
    {
        base.Update();

        // HealthBar.ApplyHealth(this);
        InkBar.ApplyInk(this);

        // if (lastPos != transform.position) {
        //     animator.SetInteger("Direction", GetDirection(Vector2.Angle(lastPos, transform.position)));

        //     lastPos = transform.position;
        // }
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

    // private int GetDirection(float angle)
    // {
    //     if (angle < 0)
    //         angle = 360 + angle;

    //     return (int)(angle / 45) % 8;
    // }
}
