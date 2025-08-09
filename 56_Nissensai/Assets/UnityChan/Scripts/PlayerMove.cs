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

    // Update()内で入力値を取得し、FixedUpdate()で物理処理を行う
    void Update()
    {
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        moveDirection = new Vector3(lsh, 0, lsv);

        // 回転はUpdate()で行う
        if (moveDirection.magnitude > 0.1f && playerRotation != null)
        {
            playerRotation.RotatePlayer(moveDirection);
        }
    }

    // 物理演算のフレームレートに同期して呼び出される
    void FixedUpdate()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();

            Vector3 moveVelocity = moveDirection * moveScale;
            Vector3 newPosition = transform.position + moveVelocity * Time.deltaTime;

            rigidbody.MovePosition(newPosition);
        }
    }
}