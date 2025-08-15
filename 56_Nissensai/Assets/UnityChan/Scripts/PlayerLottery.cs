using System.Collections;
using UnityEngine;

public class PlayerLottery : MonoBehaviour
{
    private bool isNearLottery = false;
    private int lotteryCount = 0;             // ����������
    private bool ignoreCollision = false;     // �Փ˖����t���O
    private bool isLotteryRunning = false;    // �����������t���O

    private PlayerMove playerMove;
    private Animator animator;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>(); // Animator�擾
    }

    void Update()
    {
        // Lottery�ڐG������B�{�^���ł��������A�������������ł͂Ȃ�
        if (isNearLottery && !isLotteryRunning && Input.GetButtonDown("B_Button") && lotteryCount < 2)
        {
            StartCoroutine(LotteryRoutine());
        }
    }

    private IEnumerator LotteryRoutine()
    {
        isLotteryRunning = true;
        animator.SetBool("isLotteryRunning", true); // �� �A�j���[�^�ɑ��M
        lotteryCount++;
        Debug.Log($"�����������܂��I �c���: {2 - lotteryCount}");

        playerMove.SetCanMove(false);

        // �������o 3 �b
        yield return new WaitForSeconds(3f);

        playerMove.SetCanMove(true);

        // Lottery �Փ˖��� 1 �b
        ignoreCollision = true;
        yield return new WaitForSeconds(1f);
        ignoreCollision = false;

        isLotteryRunning = false;
        animator.SetBool("isLotteryRunning", false); // �� �A�j���[�^�ɑ��M
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lottery"))
        {
            if (ignoreCollision) return;

            isNearLottery = true;
            Debug.Log("Lottery�ɐڐG �� ��~");
            playerMove.SetCanMove(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lottery"))
        {
            isNearLottery = false;
            Debug.Log("Lottery���痣�ꂽ");

            if (!ignoreCollision)
            {
                playerMove.SetCanMove(true);
            }
        }
    }
}