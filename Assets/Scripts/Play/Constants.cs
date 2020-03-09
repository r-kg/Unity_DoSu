using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //Difiiculties
    public static int size = 5;         // block set 크기 (size x size)
    public static int range = 4;        // 등장하게 할 block 타입 갯수 (현재  max 5)
    public static int obsRange = 0;     // 등장하게 할 최대 장애물 갯수
    public static int targetPool = 3;   // targetColor 랜덤 풀에 들어갈 block 타입 갯수
    public static int blockPhase = 1;   // 목표 블럭 난이도 상수

    public static int gap = 190;

    public static int x3 = 350;
    public static int y3 = 1400;
    public static int gap3 = 190;

    public static int x5 = 160;
    public static int y5 = 1590;
    public static int gap5 = 190;


    public enum BlockType { Red, Pink, Blue, Green, Yellow, LOCK, Random};
    public enum SlideState { Left, Right, Up, Down, Click, NONE };


    public static bool Prob(bool flag, ref float currentProb, float increaseProb)
    {
        if(currentProb > Random.Range(0.0f,1.0f)*100)
        {
            flag = true;
        }
        else if(flag != true)
        {
            currentProb += increaseProb;
        }

        return flag;
    }
    public static bool Prob(ref float currentProb, float increaseProb)
    {
        if (currentProb > (Random.Range(0.0f, 1.0f) * 100))
        {
            return true;
        }
        else
        {
            currentProb += increaseProb;
            return false;
        }
    }
}
