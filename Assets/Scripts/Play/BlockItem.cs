using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    public ItemDo itemDo;
    public ItemSoo itemSoo;

    private readonly float itemProb = 5f;
    protected readonly float probIncrease = 1f;

    private void Awake()
    {
        itemDo = new ItemDo();
        itemSoo = new ItemSoo();
        itemDo.Animator = GameObject.Find("Canvas").transform.Find("Do").transform.Find("ObjectDo").gameObject.GetComponent<Animator>();
        itemSoo.Animator = GameObject.Find("Canvas").transform.Find("Soo").transform.Find("ObjectSoo").gameObject.GetComponent<Animator>();
        itemDo.ItemAnim = GameObject.Find("Canvas").transform.Find("Do").transform.Find("itemIndicator").gameObject.GetComponent<Animator>();
        itemSoo.ItemAnim = GameObject.Find("Canvas").transform.Find("Soo").transform.Find("itemIndicator").gameObject.GetComponent<Animator>();
    }

    public abstract class Item
    {
        protected bool active = true;
        protected float prob = 5f;
        public Animator Animator { get; set; }
        public Animator ItemAnim {get; set;}

        public abstract void Use();
        public void TryGenerate()
        {
            if (active == true) return;

            active = Constants.Prob(active,ref prob, 3f);

            if (active)
            {
                prob = 5f;
                ItemAnim.SetBool("ItemActive",true);
            }
        }
    }
    
    public class ItemDo : Item
    {
        public override void Use()
        {
            if (active == false) return;

            for (int i = 0; i < Mathf.Pow(Constants.size, 2); i++)
            {
                Block blockScript = Main.blockList[i].GetComponent<Block>();

                if(blockScript.Coord.GetIndex()==(Mathf.Pow(Constants.size, 2)-1)/2)
                {
                    blockScript.Destroy("PopDo",0.5f);
                }
                else
                {
                    blockScript.Destroy("Destroy",0.5f);
                }
            }

            Animator.SetTrigger("DoSmile");
            SoundManager.Instance.DoMeow();
            Main.blockGenerator.DelayGenerate(0.51f, false);
            ItemAnim.SetBool("ItemActive",false);
            Main.timer.ModifyTime(10);
            active = false;
        }       
    }

    public class ItemSoo : Item
    {
        public override void Use()
        {
            if (active == false) return;

            for (int i = 0; i < Mathf.Pow(Constants.size, 2); i++)
            {
                Block blockScript = Main.blockList[i].GetComponent<Block>();
                if (Main.blockTarget.TargetColor == blockScript.Color)
                {
                    Main.blockTarget.Score += 100;
                    blockScript.Destroy("PopSoo",0.5f);
                }
            }

            Animator.SetTrigger("SooSmile");
            SoundManager.Instance.DoMeow();
            Main.blockGenerator.DelayGenerate(0.51f, false);
            ItemAnim.SetBool("ItemActive",false);
            Main.timer.ModifyTime(10);
            active = false;
            
        }
    }
}
