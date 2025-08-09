using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveScale;  // �ő�ړ����x

    // �v���C���[�̈ړ����x
    [SerializeField, Header("�v���C���[�̈ړ����xX��")] private float m_pMove_X = 1.0f;
    [SerializeField, Header("�v���C���[�̈ړ����xY��")] private float m_pMove_Y = 0.0f;  // �ʏ�͎g�p���Ȃ�
    [SerializeField, Header("�v���C���[�̈ړ����xZ��")] private float m_pMove_Z = 1.0f;

    private Vector3 moveDirection = Vector3.zero;  // �ړ�������ۑ�����ϐ�
    private Vector3 currentVelocity = Vector3.zero;  // ���݂̈ړ����x
    private Rigidbody rigidbody;
    private PlayerRotation playerRotation;  // PlayerRotation �N���X�̃C���X�^���X
    private PlayerAnimation playerAnimation;
    private Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerRotation = GetComponent<PlayerRotation>();  // PlayerRotation �N���X���擾
        animator = GetComponent<Animator>();
    }

    // Update()���œ��͒l���擾���AFixedUpdate()�ŕ����������s��
    void Update()
    {
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        moveDirection = new Vector3(lsh, 0, lsv);

        // ��]��Update()�ōs��
        if (moveDirection.magnitude > 0.1f && playerRotation != null)
        {
            playerRotation.RotatePlayer(moveDirection);
        }
    }

    // �������Z�̃t���[�����[�g�ɓ������ČĂяo�����
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