using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameLiveProject.Engine
{
    class GraphicField
    {
        private PictureBox _PictureForm;
        public GraphicField(PictureBox Pb)
        {
            _PictureForm = Pb;
        }


        public int widthPicture
        {
            get
            {
                return _PictureForm.Size.Width;
            }
        }

        public int heightPicture
        {
            get
            {
                return _PictureForm.Size.Width;
            }
        }

        public void DrawState(GameState StateToPrint)
        {
            // Определяем высоту и ширину ячейки
            int XOneCell = _PictureForm.Size.Width / GameState.XSize;
            int YOneCell = _PictureForm.Size.Height / GameState.YSize;
          
            Bitmap BMP = new Bitmap(_PictureForm.Size.Width, _PictureForm.Size.Height);
            // Draw rectangle to screen.
            Graphics g = Graphics.FromImage(BMP);
            _PictureForm.Image = BMP;

            // Выводим пустое белое поле
            g.FillRectangle(Brushes.White, 0, 0, _PictureForm.Size.Width, _PictureForm.Size.Height);
            
            
            // Выводим решётку
            for (int i = 0; i < GameState.XSize; i++)
            {
                int XBegin = 0, YBegin=0;
                XBegin = i * XOneCell;

                int XEnd = XBegin + XOneCell;
                int YEnd = _PictureForm.Size.Height;
                g.DrawRectangle(Pens.Black, XBegin, YBegin, XEnd, YEnd);
               

            }

            for (int i = 0; i < GameState.YSize; i++)
            {
                int XBegin = 0, YBegin = 0;
                YBegin = i * YOneCell;

                int XEnd = _PictureForm.Size.Width;
                int YEnd = YBegin + YOneCell;
                g.DrawRectangle(Pens.Black, XBegin, YBegin, XEnd, YEnd);


            }


            // Выводим точки
            for (int i = 0;i<GameState.XSize;i++)
            {
                for (int j = 0; j < GameState.YSize; j++)
                {
                    if (StateToPrint[i,j])
                    {
                        // Надо вывести кружок
                        int XBegin = j * YOneCell;
                        int YBegin = i * XOneCell;
                        g.FillEllipse(Brushes.Black, XBegin, YBegin, XOneCell, YOneCell);

                    }
                }
            }
        }

    }
}
