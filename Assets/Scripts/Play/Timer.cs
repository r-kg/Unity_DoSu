using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Image timeBar;
    public Animator timeBarAnim, timerAnim;
    public bool End { get; set; }
    private float maxTime;
    public float MaxTime
    {
        get { return maxTime; }
        set
        {
            if (value < 10) value = 10;
            maxTime = value;
            timeSpeed = 1.0f / (float)maxTime;
        }
    }
    
    private float progress = 1.0f;
    private float timePassed;
    private float timeSpeed;

    public readonly float startTime = 25f;

    public bool isPause;

    void Awake()
    {
        timeBar = GameObject.Find("TimerBar").GetComponent<Image>();
        timeBarAnim = GameObject.Find("TimerBar").GetComponent<Animator>();
        timerAnim = GameObject.Find("TimerBar/Timer").GetComponent<Animator>();
        timePassed = Time.deltaTime;
        MaxTime = startTime;
        isPause = false;
    }

    
    public float GetRemainTime()
    {
        return maxTime - timePassed;
    }

    public void SetTimeDifficulty(int score)
    {
        if(score < 62000)
        {
            MaxTime = startTime - (float)score / 4200;
        }
        else
        {
            MaxTime = startTime - (float)score / 7500;
        }
    }

    public void ModifyTime(int time)
    {
        if (time > 2)
        {
            progress = 1.0f;
            timePassed = Time.deltaTime;
            timeBar.fillAmount = 1.0f;
        }
        else
        {
            timePassed = timePassed - time;
            progress = progress + (float)time / maxTime;
            timeBar.fillAmount = Mathf.Lerp(0, 1, progress);
        }
    }

    /**
     ** Start Timer with coroutine **
     */ 
    public void StartTimer()
    {
        StartCoroutine(TimeCoroutine());
    }

    public void PauseTimer(bool flag)
    {
        isPause = flag;
    }

    public IEnumerator TimeCoroutine()
    {
        while(progress >= 0)
        {

            if(!isPause)
            {
                timeBar.fillAmount = Mathf.Lerp(0, 1, progress);
                progress -= timeSpeed * Time.deltaTime;
                timePassed += Time.deltaTime;
            }

            if(timePassed/MaxTime >= 0.66)
            {
                timerAnim.SetBool("TimeUrgent",true);
            }
            else
            {  
                timerAnim.SetBool("TimeUrgent",false);
            }

            yield return null;

            if(timePassed > maxTime)
            {
                Debug.Log("Time's up : " + timePassed);
                End = true;

                yield break;
            }
        }
    }
}
