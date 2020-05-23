using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLiveProject.Engine
{

    class GameState
    {
        public static int XSize = 45;
        public static int YSize = 45;
        private bool[,] CellsPull;
        private bool _NoLiveCells = false;
        private bool _NoChanged = false;

        public bool NoChanged
        {
            get { return _NoChanged; }
        }

        public bool NoLiveCells
        {
            get { return _NoLiveCells; }
        }

        public GameState ()
        {
            CellsPull = new bool [XSize,YSize];
        }

        public GameState DoStep()
        {
            bool wasChanged = false;
            bool isLiveCells = false;

            GameState Result = new GameState();
            int NeighbourNumber = 0;
            bool NewState;
            // Перебираем все ячейки старого состояния

            for (int i = 0; i < GameState.XSize;i++ )
            {
                for (int j = 0; j < GameState.YSize; j++)
                {
                    
                    // Для ячейки надо найти количество соседей
                    NeighbourNumber = this.getNeighbourNumber(i,j);
                    if (CellsPull[i, j] == true)
                    {
                        if (NeighbourNumber==2 || NeighbourNumber==3)
                        {
                            NewState = true;
                        }
                        else
                        {
                            NewState = false;
                        }
                    }
                    else
                    {
                        if (NeighbourNumber==3)
                        {
                            NewState = true;
                        }
                        else
                        {
                            NewState = false;
                        }
                    }

                    // Сравниваем флаги. Если состояние ДО не равно состоянию ПОСЛЕ - поднимаем флаг 
                    if (NewState!=CellsPull[i, j])
                    {
                        wasChanged = true;
                    }

                    // Если новое состояние true, то надо поднять флаг, что есть живые особи
                    if (NewState==true)
                    {
                        isLiveCells = true;
                    }

                    Result[i, j] = NewState;

                }
            }
            Result._NoChanged = !(wasChanged);
            Result._NoLiveCells = !(isLiveCells);


            return Result;
        }

        private int getNeighbourNumber(int X,int Y)
        {
            int Result = 0;
            int [] XIndexes = new int[8];
            int [] YIndexes = new int[8];

            XIndexes[0] = X - 1; YIndexes[0] = Y - 1;
            XIndexes[1] = X; YIndexes[1] = Y - 1;
            XIndexes[2] = X+1; YIndexes[2] = Y - 1;
            XIndexes[3] = X - 1; YIndexes[3] = Y;
            XIndexes[4] = X + 1; YIndexes[4] = Y;
            XIndexes[5] = X - 1; YIndexes[5] = Y + 1;
            XIndexes[6] = X; YIndexes[6] = Y + 1;
            XIndexes[7] = X + 1; YIndexes[7] = Y + 1;

            // Перебираем координаты
            for (int i = 0; i < 8;i++ )
            {
                // Если Х и Y в диапазоне
                if (
                    ((XIndexes[i] >= 0) && (XIndexes[i] < XSize))
                    &&
                    ((YIndexes[i] >= 0) && (YIndexes[i] < YSize))
                    )
                { 
                    if (CellsPull[XIndexes[i],YIndexes[i]]==true)
                    {
                        Result++;
                    }
                }
            }

                return Result;
        }

        public bool this[int row,int column]
        {
            get
            {
                return CellsPull[row,column];
            }

            set
            {
                CellsPull[row, column] = value;
            }
        }


    }
}
