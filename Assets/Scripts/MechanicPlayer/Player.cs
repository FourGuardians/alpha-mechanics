using GameAPI.Entities;
using GameAPI.Navigation;
using System;
using UnityEngine;

public class Player : Entity2D<Player>
{
    // private Vector3 lastPos;
    private Animator animator;

    new void Start()
    {
        base.Start();

        Speed = 2;

        animator = GetComponent<Animator>();
        // lastPos = transform.position;
    }

    new void Update()
    {
        base.Update();

        Move();

        // if (lastPos != transform.position) {
        //     animator.SetInteger("Direction", GetDirection(Vector2.Angle(lastPos, transform.position)));

        //     lastPos = transform.position;
        // }
    }

    private void Move()
    {
        int keys =
            ((Input.GetKey(KeyCode.W) ? 1 : 0) << 3) +
            ((Input.GetKey(KeyCode.S) ? 1 : 0) << 2) +
            ((Input.GetKey(KeyCode.A) ? 1 : 0) << 1) +
            ((Input.GetKey(KeyCode.D) ? 1 : 0) << 0);

        // Debug.Log(Convert.ToString(keys, 2).PadLeft(4, '0'));

        

        switch (keys)
        {
            case 0b1000: // UP
                AnimIdle();
                AnimDirection(0);

                transform.position += Direction.North * Speed * Time.deltaTime;
                break;

            case 0b1001: // UP RIGHT
                AnimIdle();
                AnimDirection(1);

                transform.position += Direction.NorthEast * Speed * Time.deltaTime;
                break;

            case 0b0001: // RIGHT
                AnimIdle();
                AnimDirection(2);

                transform.position += Direction.East * Speed * Time.deltaTime;
                break;

            case 0b0101: // DOWN RIGHT
                AnimIdle();
                AnimDirection(3);

                transform.position += Direction.SouthEast * Speed * Time.deltaTime;
                break;

            case 0b0100: // DOWN
                AnimIdle();
                AnimDirection(4);

                transform.position += Direction.South * Speed * Time.deltaTime;
                break;

            case 0b0110: // DOWN LEFT
                AnimIdle();
                AnimDirection(5);

                transform.position += Direction.SouthWest * Speed * Time.deltaTime;
                break;

            case 0b0010: // LEFT
                AnimIdle();
                AnimDirection(6);

                transform.position += Direction.West * Speed * Time.deltaTime;
                break;

            case 0b1010: // UP LEFT
                AnimIdle();
                AnimDirection(7);   

                transform.position += Direction.NorthWest * Speed * Time.deltaTime;
                break;

            default:
                AnimIdle(true);
                break;
        }
    }

    private void AnimIdle(bool value = false) =>
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
