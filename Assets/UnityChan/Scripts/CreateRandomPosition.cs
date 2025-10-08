using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreateRangeRandomPosition:MonoBehaviour
{
    //��������Q�[���I�u�W�F�N�g
    public GameObject createPrefab;

    //��������͈�
    public Transform rangeA;

    public Transform rangeB;

    //�������鍂��
    private float Height = 0.5f;

    //�������ڐG���Ȃ�
    private float Distance = 3.0f;

    //�����̃J�E���g
    private bool generate = true;

    private List<Vector3> placedPositions = new List<Vector3>();

    void Update()
    {
        if (generate)
        {
            for (int i = 0; i < 12; i++)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(rangeA.position.x, rangeB.position.x),
                    Height,
                    Random.Range(rangeA.position.z, rangeB.position.z));

                if (IsPositionValid(randomPos))
                {
                    Instantiate(createPrefab, randomPos, createPrefab.transform.rotation);
                    placedPositions.Add(randomPos);
                }
                else
                {
                    i--;
                }

            }
            generate = false;

        }
    }
    bool IsPositionValid(Vector3 newPos) 
    {
        foreach (Vector3 pos in placedPositions)
        {
            if(Vector3.Distance(newPos,pos) < Distance)
            {
                return false;
            }
        }
        return true;
    } 
}
