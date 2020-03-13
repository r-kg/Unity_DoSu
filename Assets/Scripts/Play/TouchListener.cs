using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchListener : MonoBehaviour
{
    private Constants.SlideState touchState;
    private Vector2 touchIn, touchOut;
    private GameObject hitObject;


    public void GetTouch()
    {
        List<Touch> touches = InputHelper.GetTouches();

        if (touches.Count > 0)
        {
            Touch t = touches[0];
            Ray ray;
            RaycastHit hit;

            if (Main.resetCheck == false) return;

            if(Main.blockGenerator.isGenerating) return;

             switch (t.phase)
             {
                 case TouchPhase.Began:
                     //터치 시작
                     touchIn = Camera.main.ScreenToWorldPoint(t.position);
                     ray = Camera.main.ScreenPointToRay(t.position);
                     Physics.Raycast(ray, out hit);

                     if (hit.collider)
                     {
                        hitObject = hit.collider.gameObject;
                     }
                     break;

                 case TouchPhase.Moved:
                    //터치 이동
                    if(hitObject)
                    {
                        //Main.blockTarget.HintTarget(hitObject, true);    
                    }

                    touchOut = Camera.main.ScreenToWorldPoint(t.position);
                    touchState = GetSlideState();
                    
                     break;

                 case TouchPhase.Ended:
                    //터치 종료
                    
                    //Main.blockTarget.HintTarget(hitObject, false);
                    touchOut = Camera.main.ScreenToWorldPoint(t.position);
                    touchState = GetSlideState();
                    ray = Camera.main.ScreenPointToRay(t.position);
                    Physics.Raycast(ray, out hit);

                    if(hitObject)
                    {
                        foreach(Animator item in Main.blockAnimatorList)
                        {
                            if(item.GetCurrentAnimatorStateInfo(0).IsName("Locked"))
                            {
                                return;
                            }
                            else
                            {
                                //item.SetTrigger("Reset");
                            }
                        }

                        if(touchState == Constants.SlideState.Click && hit.collider)
                        {
                            OnClick(hit.collider.gameObject.tag, hit, t.position);
                        }

                        else if(touchState != Constants.SlideState.NONE) // moveState = Slide
                        {
                            OnSlide(hitObject.tag);
                        }
                    }

                    hitObject = null;
                    break;
             }//switch
        }

    }

    /// <summary>
    /// Click시 실행할 tag별 switch 분기 함수
    /// </summary>
    /// <param name="hitTag"></param>
    /// <param name="hit"></param>
    /// <param name="touchPosToVector"></param>
    private void OnClick(string hitTag, RaycastHit hit, Vector2 touchPosToVector)
    {
        switch (hitTag)
        {
            case "Block":
                if (hit.collider.gameObject.GetComponent<Block>().Color == Constants.BlockType.LOCK)
                {
                    break;
                }
                else
                {
                    Main.resetCheck = false;
                    int timeSub = Main.blockTarget.CalculateTargets(hit.collider.gameObject, Camera.main.ScreenToWorldPoint(touchPosToVector));
                    Main.timer.ModifyTime(timeSub);
                    Main.blockGenerator.DelayGenerate(0.51f, true, true);
                }
                break;

            case "ItemSoo":
                Main.blockItem.itemSoo.Use();
                Invoke("SetScore",0.4f);
                break;

            case "ItemDo":
                Main.blockItem.itemDo.Use();
                break;
        }
    }

    /// <summary>
    /// Slide시 실행할 tag별 분기 함수
    /// </summary>
    /// <param name="tag"></param>
    private void OnSlide(string tag)
    {
        switch (tag)
        {
            case "Block":

                Main.blockSlider.Slide(hitObject, touchState);

                break;
        }
    }

    /// <summary>
    /// 클릭 down, up 좌표와 distance로 Click, 혹은 슬라이드 방향 계산
    /// </summary>
    /// <returns></returns>
    private Constants.SlideState GetSlideState()
    {
        Vector2 v = touchOut - touchIn;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        if (Vector2.Distance(touchIn, touchOut) < 30)
        {
            return Constants.SlideState.Click;
        }
        else if (angle >= 55 && angle <= 125)
        {
            return Constants.SlideState.Up;
        }
        else if (angle <= -55 && angle >= -125)
        {
            return Constants.SlideState.Down;
        }
        else if (angle >= -35 && angle <= 35)
        {
            return Constants.SlideState.Right;
        }
        else if (Mathf.Abs(angle) >= 145)
        {
            return Constants.SlideState.Left;
        }
        return Constants.SlideState.NONE;
    }

        //수수아이템을 위해
        public void SetScore()
        {
            //Main.blockTarget.SetDifficulty();
            Main.blockTarget.scoreTextAnim.SetTrigger("ScorePlus");
            Main.blockTarget.scoreText.text = string.Format("{0:#,###0}", Main.blockTarget.Score);
            Main.timer.SetTimeDifficulty(Main.blockTarget.Score);
        }
}
