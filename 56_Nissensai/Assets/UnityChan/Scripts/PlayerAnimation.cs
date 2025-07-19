using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //アニメーション
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

    protected Animator anim = null; //アニメ―ション
    private AnimName animName; //構造体の宣言

    void Start()
    {
        anim = this.GetComponent<Animator>();
        animName = new AnimName("run", "lift", "pullup");
    }

    //RunのAnimationを再生
    public void RunAnim()
    {
        anim.SetBool(animName.run, true);
    }

    //RunのAnimationを止める
    public void StopRunAnim()
    {
        anim.SetBool(animName.run, false);
    }

    //LiftのAnimation再生
    public void PlayLiftAnim()
    {
        anim.SetBool(animName.lift, true);
    }

    //LiftのAnimationを止める
    public void StopLiftAnim()
    {
        anim.SetBool(animName.lift, false);
    }

    //PullUpのAnimationを再生
    public void PlayPullUpAnim()
    {
        anim.SetBool(animName.pullUp, true);
    }

    //PullUpのAnimationを止める
    public void StopPullUpAnim()
    {
        anim.SetBool(animName.pullUp, false);
    }

}
