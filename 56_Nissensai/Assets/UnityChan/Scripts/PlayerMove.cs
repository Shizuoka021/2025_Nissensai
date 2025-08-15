using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveScale = 5f; // Inspectorで調整可能
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerRotation playerRotation;
    private bool canMove = true; // 移動可能フラグ
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRotation = GetComponent<PlayerRotation>();
        animator = GetComponent<Animator>(); // Animator取得
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetBool("isMoving", false); // 停止状態を送信
            return;
        }

        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        moveDirection = new Vector3(lsh, 0, lsv);

        bool moving = moveDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", moving); // 移動状態を送信

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
            Debug.Log("Wallに接触 → 停止");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetCanMove(true);
            Debug.Log("Wallから離れた → 移動可能");
        }
    }
}