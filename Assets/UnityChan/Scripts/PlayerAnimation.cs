using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //�A�j���[�V����
    private struct AnimName
    {
        public string run;
        public string pullUp;

        public AnimName(string run, string pullup)
        {
            this.run = run;
            this.pullUp = pullup;
        }
    }

    protected Animator anim = null; //�A�j���\�V����
    private AnimName animName; //�\���̂̐錾

    void Start()
    {
        anim = this.GetComponent<Animator>();
        animName = new AnimName("run", "pullup");
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
