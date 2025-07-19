using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveScale;  // 最大移動速度

    // プレイヤーの移動速度
    [SerializeField, Header("プレイヤーの移動速度X軸")] private float m_pMove_X = 1.0f;
    [SerializeField, Header("プレイヤーの移動速度Y軸")] private float m_pMove_Y = 0.0f;  // 通常は使用しない
    [SerializeField, Header("プレイヤーの移動速度Z軸")] private float m_pMove_Z = 1.0f;

    private Vector3 moveDirection = Vector3.zero;  // 移動方向を保存する変数
    private Vector3 currentVelocity = Vector3.zero;  // 現在の移動速度
    private Rigidbody rigidbody;
    private PlayerRotation playerRotation;  // PlayerRotation クラスのインスタンス
    private PlayerAnimation playerAnimation;
    private Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerRotation = GetComponent<PlayerRotation>();  // PlayerRotation クラスを取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ////持ち上げてる状態の時動かないようにする
        //if (animator.GetBool("PullUp") || animator.GetBool("Lift")) return;
        // 入力による移動方向の更新
        Move(m_pMove_X, m_pMove_Y, m_pMove_Z);
        // 一定速度による移動処理
        ApplyMovement();
        //走ってる時のアニメーション
        //RunAnimChange();
        //スティックで入力されている方向を取得
    }

    // 移動ベクトルを計算
    void Move(float x, float y, float z)
    {
        //初期化
        moveDirection = Vector3.zero;

        //Lstick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");

        Debug.Log($"Input: H={lsh}, V={lsv}");

        //スティック操作をしているかどうか
        if ((lsh != 0) || (lsv != 0))
        {
            //Lstickを上に倒したときの処理
            if (transform.position.z < 10.0f)
            {
                if (lsv > 0)
                {
                    moveDirection += new Vector3(0, 0, z);
                }
            }
            //Lstickを下に倒したときの処理
            if (transform.position.z > -10.0f)
            {
                if (lsv < 0)
                {
                    moveDirection += new Vector3(0, 0, -z);
                }
            }
            //Lstickを左に倒したときの処理
            if (transform.position.x > -10.0f)
            {
                if (lsh < 0)
                {
                    moveDirection += new Vector3(-x, 0, 0);
                }
            }
            //Lstickを右に倒したときの処理
            if (transform.position.x < 10.0f)
            {
                if (lsh > 0)
                {
                    moveDirection += new Vector3(x, 0, 0);
                }
            }
        }


        // 移動ベクトルを正規化して、斜め移動での速度が速くならないようにする
        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection = moveDirection.normalized;  // 正規化
        }

    }

    // 一定速度で移動させる処理
    void ApplyMovement()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 moveVelocity = moveDirection * moveScale;

            // Rigidbody を使って移動（物理干渉を避ける）
            rigidbody.MovePosition(transform.position + moveVelocity * Time.deltaTime);

            playerAnimation.RunAnim();

            // 回転処理
            playerRotation.RotatePlayer(moveVelocity);
        }
        else
        {
            playerAnimation.StopRunAnim();
        }
    }

    public void RunStop()
    {
        currentVelocity = Vector3.zero;
        playerAnimation.StopRunAnim();
    }
}