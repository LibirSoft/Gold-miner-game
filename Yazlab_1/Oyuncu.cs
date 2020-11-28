using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yazlab_1
{
  abstract class  Oyuncu
    {
        int SELF_GOLD = 200;
        int GOLD_SELECTION_COST = 20;
        int FAST = 5;
        int[,,] MAP;
        int BORDER_X, BORDER_Y;
        int POS_X, POS_Y;
        int TARGET_X, TARGET_Y;

        Oyuncu(int[,] matris, int Self_gold,int gold_selection_cost,int fast,int border_x,int border_y)
        {



            if (Self_gold != null)
            {
                this.SELF_GOLD = Self_gold;
            }
        }
        //public void Move() {
        //    int distance=9999999;
        //    for (int i = 0; i <BORDER_X ; i++)
        //    {
        //        for (int j = 0; j < BORDER_Y; j++)
        //        {
        //            if (MAP[i, j,] == 1)
        //            {
        //                if ((i - POS_X) + (j - POS_Y) < distance)
        //                {
        //                    distance = (i - POS_X) + (j - POS_Y);
        //                    TARGET_X = i;
        //                    TARGET_Y = j;

        //                }

        //            }

        //        }

        //    }




        
        //}


    }
}
