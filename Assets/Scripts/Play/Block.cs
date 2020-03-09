using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Sprites;

public class Block : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Coordinate Coord { get; set; }
    public Animator Animator { get; set; }

    

    /// <summary>
    /// Tile Color Property
    /// </summary>
    private Constants.BlockType color;
    public Constants.BlockType Color
    {
        get { return color; }
        set
        {
            if (value == Constants.BlockType.Random)
            {
                color = (Constants.BlockType)Random.Range(0, Constants.range);
                
            }
            else
            {
                color = value;
            }
            spriteRenderer.sprite = Main.blockColors[(int)color];
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    private void Awake()
    {
        Coord = new Coordinate(0, 0);
        Animator = this.GetComponent<Animator>();
        //spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Destroy(string anim, float delay)
    {
        StartCoroutine(DestroyBlock(anim, delay));
    }
    private IEnumerator DestroyBlock(string anim, float delay)
    {
        Animator.SetTrigger(""+anim);
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}