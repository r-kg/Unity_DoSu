using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public bool Exceed { get; set; }
    private int x;
    public int X
    {
        get { return x; }
        set
        {
            x = CorrectCoord(value);
        }
    }

    private int y;
    public int Y
    {
        get { return y; }
        set
        {
            y = CorrectCoord(value);
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="y">y</param>
    public Coordinate(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// 초과하는 좌표값 보정
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private int CorrectCoord(int input)
    {
        if (input >= Constants.size)
        {
            input %= Constants.size;
            Exceed = true;
        }
        else if(input < 0)
        {
            input += Constants.size;
            Exceed = true;
        }

        return input;
    }

    /// <summary>
    /// TileNum
    /// </summary>
    /// <returns></returns>
    public int GetIndex()
    {
        return X + Y * Constants.size;
    }

    /// <summary>
    /// 두 좌표값을 비교하여 같을시 true, 아닐시 false 반환
    /// </summary>
    /// <param name="comp"> Coordinate </param>
    /// <returns></returns>
    public bool CompareCoord(Coordinate value)
    {
        if (this.X == value.X && this.Y == value.Y)
        {
            return true;
        }

        return false;
    }
}
