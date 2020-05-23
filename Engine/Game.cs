using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLiveProject.Engine
{
    class Game
    {

        private GraphicField _PictureField;
        private Label _MessageLabel; 
        private Button _StartStopPicture; 
        private Button _ClearButton;
        private GameState CurrentGameState = null;
        private GameState PreviousGameState  = null;
        private bool GameProceeding = false;
        private Timer GameTimer;
        int TestPointer = 0;

        public Game(PictureBox PictureField, Label MessageLabel, Button StartStopPicture, Button ClearButton)
        {
            _PictureField = new GraphicField(PictureField);
            _MessageLabel = MessageLabel; 
            _StartStopPicture = StartStopPicture; 
            _ClearButton = ClearButton;

            CurrentGameState = new GameState();

            _PictureField.DrawState(CurrentGameState);

            GameTimer = new Timer();
            GameTimer.Interval = 30;

            // Подпишем обработчик
            GameTimer.Tick += new System.EventHandler(timerGame_Tick);
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            GameState NextGameState = CurrentGameState.DoStep();

            // Проверим
            // Если все умерли - останавливаем игру, выводим сообщение 
            if (NextGameState.NoLiveCells)
            {
                _MessageLabel.Text = "Увы, но все клетки умерли! Это печально";
                StopGame();

            }
            else
            {
                // Если нет изменений (Райский сад) - останавливаем игру, выводим сообщение
                if (NextGameState.NoChanged)
                {
                    _MessageLabel.Text = "Ваш мир достиг состояния <<Райский сад>>. Поздравляем!";
                    StopGame();

                }

            }

            // Меняем состояния
            PreviousGameState = CurrentGameState;
            CurrentGameState = NextGameState;
            _PictureField.DrawState(CurrentGameState);

        }

        public void Clear()
        {
            CurrentGameState = new GameState();
            PreviousGameState = null;
            _PictureField.DrawState(CurrentGameState);
            _MessageLabel.Text = "Расставьте клетки в ячейки и нажмите <<Поехали>>";
        }

        public void StartStop()
        {
            if (GameProceeding)
            {
                // Останавливаем игру
                StopGame();
                _MessageLabel.Text = "Расставьте клетки в ячейки и нажмите <<Поехали>>";
            }
            else
            {
                // Запускаем игру
                StartGame();
                _MessageLabel.Text = "Нажмите <<Стоп>>, чтобы остановить игру";
            }


        }

        private void StopGame()
        {
            _StartStopPicture.Text = "Поехали!";
            _ClearButton.Visible = true;
            GameProceeding = false;
            GameTimer.Stop();
        }

        private void StartGame()
        {
            _StartStopPicture.Text = "Стоп!";
            _ClearButton.Visible = false;
            GameProceeding = true;
            GameTimer.Start();
        }

        public void AddCell(int X, int Y  )
        {
            if (!GameProceeding)
            {
                // Определяем высоту и ширину ячейки
                int XOneCell = _PictureField.widthPicture / GameState.XSize;
                int YOneCell = _PictureField.heightPicture/ GameState.YSize;

                int XPos = X / XOneCell;
                int YPos = Y/ XOneCell;

                CurrentGameState[YPos, XPos] = !(CurrentGameState[YPos, XPos]);

                // Выводим CurrentState на картинку
                _PictureField.DrawState(CurrentGameState);

                _MessageLabel.Text = "Расставьте клетки в ячейки и нажмите <<Поехали>>";

            }
        }

    }
}
