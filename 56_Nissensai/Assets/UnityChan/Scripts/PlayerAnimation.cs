using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //�A�j���[�V����
    private struct AnimName
    {
        public string run;
        public string lift;
        public string pullUp;

        public AnimName(string run, string lift, string pullup)
        {
            this.run = run;
            this.lift = lift;
            this.pullUp = pullup;
        }
    }

    protected Animator anim = null; //�A�j���\�V����
    private AnimName animName; //�\���̂̐錾

    void Start()
    {
        anim = this.GetComponent<Animator>();
        animName = new AnimName("run", "lift", "pullup");
    }

    //Run��Animation���Đ�
    public void RunAnim()
    {
        anim.SetBool(animName.run, true);
    }

    //Run��Animation���~�߂�
    public void StopRunAnim()
    {
        anim.SetBool(animName.run, false);
    }

    //Lift��Animation�Đ�
    public void PlayLiftAnim()
    {
        anim.SetBool(animName.lift, true);
    }

    //Lift��Animation���~�߂�
    public void StopLiftAnim()
    {
        anim.SetBool(animName.lift, false);
    }

    //PullUp��Animation���Đ�
    public void PlayPullUpAnim()
    {
        anim.SetBool(animName.pullUp, true);
    }

    //PullUp��Animation���~�߂�
    public void StopPullUpAnim()
    {
        anim.SetBool(animName.pullUp, false);
    }

}
