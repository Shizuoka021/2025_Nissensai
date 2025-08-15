using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveScale = 5f; // Inspector�Œ����\
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerRotation playerRotation;
    private bool canMove = true; // �ړ��\�t���O
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRotation = GetComponent<PlayerRotation>();
        animator = GetComponent<Animator>(); // Animator�擾
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetBool("isMoving", false); // ��~��Ԃ𑗐M
            return;
        }

        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        moveDirection = new Vector3(lsh, 0, lsv);

        bool moving = moveDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", moving); // �ړ���Ԃ𑗐M

        if (moving)
        {
            playerRotation?.RotatePlayer(moveDirection);
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 moveVelocity = moveDirection.normalized * moveScale;
            rb.MovePosition(transform.position + moveVelocity * Time.fixedDeltaTime);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!value) moveDirection = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetCanMove(false);
            animator.SetBool("isMoving", false);
            Debug.Log("Wall�ɐڐG �� ��~");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetCanMove(true);
            Debug.Log("Wall���痣�ꂽ �� �ړ��\");
        }
    }
}