using System.Collections;
using UnityEngine;

public class PlayerLottery : MonoBehaviour
{
    private bool isNearLottery = false;
    private int lotteryCount = 0;             // ����������
    private bool ignoreCollision = false;     // �Փ˖����t���O�i�ړ��ĊJ����p�j
    private bool isLotteryRunning = false;    // �����������t���O

    private PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        // Lottery�ڐG������B�{�^���ł�������
        // �������������ł͂Ȃ�
        if (isNearLottery && !isLotteryRunning && Input.GetButtonDown("B_Button") && lotteryCount < 2)
        {
            StartCoroutine(LotteryRoutine());
        }
    }

    private IEnumerator LotteryRoutine()
    {
        isLotteryRunning = true; // ���������J�n
        lotteryCount++;
        Debug.Log($"�����������܂��I �c���: {2 - lotteryCount}");

        // �ړ���~�i�O�̂��߁j
        playerMove.SetCanMove(false);

        // �������o 3 �b
        yield return new WaitForSeconds(3f);

        // �ړ��ĊJ
        playerMove.SetCanMove(true);

        // Lottery �Փ˖��� 1 �b
        ignoreCollision = true;
        yield return new WaitForSeconds(1f);
        ignoreCollision = false;

        isLotteryRunning = false; // ���������I��
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lottery"))
        {
            if (ignoreCollision) return; // ���������͖���

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