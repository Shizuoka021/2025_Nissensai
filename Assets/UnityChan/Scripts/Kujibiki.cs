using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Sprite sanka;
    [SerializeField] Sprite gold;
    [SerializeField] Sprite silver;
    [SerializeField] Sprite bronze;
    [SerializeField] Image myPhoto;

    //一等、二等、三等の数
    public int Gold = 2;

    public int Silver = 3;

    public int Bronze = 3;

    public float time = 3.0f;

    //表示時間
    private float limit;

    //表示時間経過中か否かの判断
    bool beingMeasured; 

    // Start is called before the first frame update
    void Start()
    {
        myPhoto = GameObject.Find("Image").GetComponent<Image>();
        myPhoto.enabled = false;
        beingMeasured = false;
        limit = time;
    }

    // Update is called once per frame
    void Update()
    {
       
        if((Input.GetButtonDown("B_Button") || Input.GetKeyDown(KeyCode.J)) && !beingMeasured)
        {
            int rnd = Random.Range(1, 10000);
             Debug.Log(rnd);

            if(rnd == 1 && Gold >= 1)
            {
                myPhoto.sprite = gold;
                myPhoto.enabled = true;
                Gold--; 
            }
            else if(rnd >=2 && rnd <= 103 && Silver >= 1)
            {
                myPhoto.sprite = silver;
                myPhoto.enabled = true;
                Silver--;
            }
            else if(rnd >= 103 && rnd <= 404 && Bronze >= 1)
            {
                myPhoto.sprite = bronze;
                myPhoto.enabled = true;
                Bronze--;
            }
            else
            {
                myPhoto.sprite = sanka;
                myPhoto.enabled = true;
            }

            beingMeasured = true;
            
        }
        if(beingMeasured == true)
        {
            limit -= Time.deltaTime;
            if (limit <= 0.0f)
            {
                beingMeasured = false;
                myPhoto.enabled = false;
                limit = time;
            }
        }
    }
}
