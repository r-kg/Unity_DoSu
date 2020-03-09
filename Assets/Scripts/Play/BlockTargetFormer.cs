using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlockTargetFormer : MonoBehaviour
{
    public int targetFormNumber;

    //public SpriteAtlas saTargetForm;

    public Coordinate Pivot { get; set; }
    // Start is called before the first frame update
    
    public void SetTargetFormNumSprite(Image targetFormSprite)
    {
        targetFormNumber = Random.Range(0,8);
        targetFormSprite.sprite = Resources.Load<Sprite>("Image/TargetForm/Phase"+Constants.blockPhase+"/Form" + targetFormNumber);
        //targetFormSprite.sprite = saTargetForm.GetSprite("Phase"+Constants.blockPhase+"/Form" + targetFormNumber);
    }



    ///////////
    //Phase 1//
    ///////////
    private void Phase1Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
    }
    private void Phase1Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
    }
    private void Phase1Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
    }
    private void Phase1Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
    }
    private void Phase1Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
    }
    private void Phase1Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
    }
    private void Phase1Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
    }
    private void Phase1Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
    }


    ///////////
    //Phase 2//
    ///////////
    private void Phase2Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 3));
    }
    private void Phase2Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));        
    }
    private void Phase2Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y));
    }
    private void Phase2Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y + 1));        
    }
    private void Phase2Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));        
    }
    private void Phase2Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 3, Pivot.Y));
    }
    private void Phase2Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y - 2));
    }
    private void Phase2Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y));
    }


    ///////////
    //Phase 3//
    ///////////
    private void Phase3Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X +  1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
    }
    private void Phase3Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
    }
    private void Phase3Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
    }
    private void Phase3Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
    }
    private void Phase3Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
    }
    private void Phase3Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));
    }
    private void Phase3Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));       
    }
    private void Phase3Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));  
    }


    ///////////
    //Phase 4//
    ///////////
    private void Phase4Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y - 1));
    }
    private void Phase4Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 3, Pivot.Y + 1));
    }
    private void Phase4Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
    }
    private void Phase4Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
    }
    private void Phase4Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y - 1));
    }
    private void Phase4Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X - 3, Pivot.Y - 1));
    }
    private void Phase4Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y - 1));       
    }
    private void Phase4Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 3, Pivot.Y + 1));    
    }

    ///////////
    //Phase 5//
    ///////////
    private void Phase5Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
    }

    private void Phase5Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
    }

    private void Phase5Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 2));
    }

    private void Phase5Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 2));
    }

    private void Phase5Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 2));
    }

    private void Phase5Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 2));
    }

    private void Phase5Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 2));
    }

    private void Phase5Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 2));
    }

    ///////////
    //Phase 6//
    ///////////
    private void Phase6Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y + 2));        
    }
    private void Phase6Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));  
    }
    private void Phase6Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));  
    }
    private void Phase6Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 2));  
    }
    private void Phase6Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));  
    }
    private void Phase6Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y + 2));  
    }
    private void Phase6Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 3));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 4));  
    }
    private void Phase6Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));  
    }

    ///////////
    //Phase 7//
    ///////////
    private void Phase7Form0(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X - 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X - 3, Pivot.Y));
    }
    private void Phase7Form1(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y));    
    }
    private void Phase7Form2(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));        
    }
    private void Phase7Form3(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 1));    
    }
    private void Phase7Form4(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y + 1));
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y + 2));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y + 3));    
    }
    private void Phase7Form5(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y - 2));    
    }
    private void Phase7Form6(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X + 1, Pivot.Y));
        targetCoords.Add(new Coordinate(Pivot.X + 2, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X + 3, Pivot.Y));    
    }
    private void Phase7Form7(List<Coordinate> targetCoords)
    {
        targetCoords.Add(Pivot);
        targetCoords.Add(new Coordinate(Pivot.X, Pivot.Y - 1));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 2));
        targetCoords.Add(new Coordinate(Pivot.X - 1, Pivot.Y - 3));
        targetCoords.Add(new Coordinate(Pivot.X , Pivot.Y - 3));    
    }



    private void FormENTIRE(List<Coordinate> targetCoords)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                targetCoords.Add(new Coordinate(i, j));
            }
        }
    }

    private void Phase0Form0(List<Coordinate> targetCoords){}
    private void Phase0Form1(List<Coordinate> targetCoords){}
    private void Phase0Form2(List<Coordinate> targetCoords){}
    private void Phase0Form3(List<Coordinate> targetCoords){}
    private void Phase0Form4(List<Coordinate> targetCoords){}
    private void Phase0Form5(List<Coordinate> targetCoords){}
    private void Phase0Form6(List<Coordinate> targetCoords){}
    private void Phase0Form7(List<Coordinate> targetCoords){}
}
