using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlider : MonoBehaviour
{
    private GameObject tileSliderParent;
    private Vector2 posArrival;
    private bool sliding = false;

    /// <summary>
    /// Constructor
    /// </summary>
    void Awake()
    {
        tileSliderParent = new GameObject("BlockSlider");
    }

    /// <summary>
    /// parameter를 base로 슬라이드 대상 타일들과 x, y coordinate 변화값 설정
    /// </summary>
    /// <param name="hitTile">마우스 Up 타일</param>
    /// <param name="state">슬라이드 방향</param>
    /// <returns></returns>
    public bool Slide(GameObject hitTile, Constants.SlideState state)
    {
        Block hitTemp = hitTile.GetComponent<Block>();
        List<Constants.BlockType> blockTypes = new List<Constants.BlockType>();
        int rX = 0, rY = 0;
        tileSliderParent.transform.position = new Vector2(0, 0);

        if (sliding) return true;

        if(state == Constants.SlideState.Left || state == Constants.SlideState.Right)
        {
            for(int i = 0; i < Mathf.Pow(Constants.size,2); i++)
            {
                Block temp = Main.blockList[i].GetComponent<Block>();

                if(temp.Coord.Y == hitTemp.Coord.Y)
                {
                    Main.blockList[i].transform.parent = tileSliderParent.GetComponent<Transform>();
                    blockTypes.Add(Main.blockList[i].GetComponent<Block>().Color);
                }
            }//for

            if (state == Constants.SlideState.Left) rX = -1;
            else rX = 1;
        }//if

        if (state == Constants.SlideState.Up || state == Constants.SlideState.Down)
        {
            for (int i = 0; i < Mathf.Pow(Constants.size, 2); i++)
            {
                Block temp = Main.blockList[i].GetComponent<Block>();

                if (temp.Coord.X == hitTemp.Coord.X)
                {
                    Main.blockList[i].transform.parent = tileSliderParent.GetComponent<Transform>();
                    blockTypes.Add(Main.blockList[i].GetComponent<Block>().Color);
                }
            }//for

            if (state == Constants.SlideState.Up) rY = -1;
            else rY = 1;

        }//if

        //장애물이 있는 경우
        
        
        if(blockTypes.Contains(Constants.BlockType.LOCK))
        {

            foreach(var item in tileSliderParent.transform.GetComponentsInChildren<Block>())
            {
                if(item.Color != Constants.BlockType.LOCK)
                {
                    item.Animator.SetTrigger("Locked");
                }
            }

            tileSliderParent.transform.DetachChildren();
            blockTypes.Clear();

            return true;
        }
        
        UpdateSliderTiles(rX, rY);
        SoundManager.Instance.Slide();

        return true;
    }


    /// <summary>
    /// Based on rX, rY values, index 밖의 타일 위치 보정 밑 슬라이드된 타일 coordinate 갱신
    /// </summary>
    /// <param name="rX">x coordinate 변화값</param>
    /// <param name="rY">y coordinate 변화값</param>
    private void UpdateSliderTiles(int rX, int rY)
    {
        Block pivotTile = tileSliderParent.transform.GetChild(tileSliderParent.transform.childCount / 2).gameObject.GetComponent<Block>();
        float posX = pivotTile.transform.position.x + Constants.gap * (Constants.size / 2 + 1) * rX * -1;
        float posY = pivotTile.transform.position.y + Constants.gap * (Constants.size / 2 + 1) * rY;
        posArrival = new Vector3(Constants.gap * rX, Constants.gap * rY * -1, 98);

        for (int i=0; i < tileSliderParent.transform.childCount; i++)
        {
            GameObject blockObject = tileSliderParent.transform.GetChild(i).gameObject;
            Block blockScript = blockObject.GetComponent<Block>();

            blockScript.Coord.X += rX;
            blockScript.Coord.Y += rY;
            blockObject.name = "Block " + blockScript.Coord.GetIndex();

            Main.blockList[blockScript.Coord.GetIndex()] = blockObject;

            if (blockScript.Coord.Exceed) 
            {
                blockObject.SetActive(false);
                blockObject.transform.position = new Vector3(posX, posY, 0);
                blockScript.Coord.Exceed = false;
                blockObject.SetActive(true);
            }
        }
        sliding = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (sliding)
        {
            tileSliderParent.transform.position = Vector3.MoveTowards(tileSliderParent.transform.position, posArrival, 1150 * Time.deltaTime);

            if (tileSliderParent.transform.position.Equals(posArrival))
            {
                sliding = false;
                tileSliderParent.transform.DetachChildren();
            }
        }
    }
}
