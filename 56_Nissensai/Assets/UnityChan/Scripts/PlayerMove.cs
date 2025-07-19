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

    void Update()
    {
        ////�����グ�Ă��Ԃ̎������Ȃ��悤�ɂ���
        //if (animator.GetBool("PullUp") || animator.GetBool("Lift")) return;
        // ���͂ɂ��ړ������̍X�V
        Move(m_pMove_X, m_pMove_Y, m_pMove_Z);
        // ��葬�x�ɂ��ړ�����
        ApplyMovement();
        //�����Ă鎞�̃A�j���[�V����
        //RunAnimChange();
        //�X�e�B�b�N�œ��͂���Ă���������擾
    }

    // �ړ��x�N�g�����v�Z
    void Move(float x, float y, float z)
    {
        //������
        moveDirection = Vector3.zero;

        //Lstick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");

        Debug.Log($"Input: H={lsh}, V={lsv}");

        //�X�e�B�b�N��������Ă��邩�ǂ���
        if ((lsh != 0) || (lsv != 0))
        {
            //Lstick����ɓ|�����Ƃ��̏���
            if (transform.position.z < 10.0f)
            {
                if (lsv > 0)
                {
                    moveDirection += new Vector3(0, 0, z);
                }
            }
            //Lstick�����ɓ|�����Ƃ��̏���
            if (transform.position.z > -10.0f)
            {
                if (lsv < 0)
                {
                    moveDirection += new Vector3(0, 0, -z);
                }
            }
            //Lstick�����ɓ|�����Ƃ��̏���
            if (transform.position.x > -10.0f)
            {
                if (lsh < 0)
                {
                    moveDirection += new Vector3(-x, 0, 0);
                }
            }
            //Lstick���E�ɓ|�����Ƃ��̏���
            if (transform.position.x < 10.0f)
            {
                if (lsh > 0)
                {
                    moveDirection += new Vector3(x, 0, 0);
                }
            }
        }


        // �ړ��x�N�g���𐳋K�����āA�΂߈ړ��ł̑��x�������Ȃ�Ȃ��悤�ɂ���
        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection = moveDirection.normalized;  // ���K��
        }

    }

    // ��葬�x�ňړ������鏈��
    void ApplyMovement()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 moveVelocity = moveDirection * moveScale;

            // Rigidbody ���g���Ĉړ��i�������������j
            rigidbody.MovePosition(transform.position + moveVelocity * Time.deltaTime);

            playerAnimation.RunAnim();

            // ��]����
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