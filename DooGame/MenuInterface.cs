using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DooGame
{
    internal class MenuInterface
    {
        private Rectangle btnPlay, btnMenu, btnReset, btnBack, btnContinu, btnGameOver;
        private ImageBrush btnPlayImg, btnMenuImg, btnResetImg, btnBackImg, btnContinuImg, btnGameOverImg;
        public bool showMenuInterface = true, elementsExsist = false, showStarterGui = true;
        public Canvas guiCanvas, starterCanvas, winCanvas, gameOverCanvas;
      
        public MenuInterface()
        {
            btnPlay = new Rectangle();
            btnMenu = new Rectangle();
            btnContinu = new Rectangle();
            btnReset = new Rectangle();
            btnBack = new Rectangle();
            btnGameOver = new Rectangle();
            starterCanvas = new Canvas();
            winCanvas = new Canvas();
            gameOverCanvas = new Canvas();
            btnResetImg = new ImageBrush(); 
            btnPlayImg = new ImageBrush();
            btnMenuImg = new ImageBrush();
            btnBackImg = new ImageBrush();
            btnGameOverImg = new ImageBrush();
            btnContinuImg = new ImageBrush();
            guiCanvas = new Canvas();   
        }
        public void InitialMenuInterface(DispatcherTimer GameTimer, Canvas mainCanvas, Action ResetGame)
        {
            RemoveElemenntsFromCanvas(mainCanvas);
            AddImgToBtns();
            AddColorToCanvas();
            InitialBtnEvents(GameTimer, ResetGame);
            AddElementToCanvas(mainCanvas);

            guiCanvas.Visibility = Visibility.Collapsed;
            winCanvas.Visibility = Visibility.Collapsed;
            gameOverCanvas.Visibility = Visibility.Collapsed;

            if (showStarterGui)
            {
                showStarterGui = false;
                ShowStarterGui();
            }
        }
        public void InitialBtnEvents(DispatcherTimer GameTimer, Action ResetGame)
        {
            btnPlay.MouseDown += (sender, e) => EnterOnBtnStart(GameTimer);
            btnPlay.MouseEnter += MouseOverBtnPlayEvent;
            btnPlay.MouseLeave += MouseLeaveBtnPlayEvent;

            btnReset.MouseDown += (sender, e) => EnterOnbtnReset(ResetGame);
            btnReset.MouseEnter += MouseOverbtnResetEvent;
            btnReset.MouseLeave += MouseLeavebtnResetEvent;

            btnContinu.MouseDown += (sender, e) => EnterOnBtnContinu(ResetGame);
            btnContinu.MouseEnter += MouseOverBtnContinuEvent;
            btnContinu.MouseLeave += MouseLeaveBtnContinuEvent;

            btnMenu.MouseDown += (sender, e) => EnterOnBtnMenu(GameTimer);
            btnMenu.MouseEnter += MouseOverBtnMenuEvent;
            btnMenu.MouseLeave += MouseLeaveBtnMenuEvent;

            btnBack.MouseDown += (sender, e) => EnterOnBtnBack(GameTimer);
            btnBack.MouseEnter += MouseOverBtnBackEvent;
            btnBack.MouseLeave += MouseLeaveBtnBackEvent;


            btnGameOver.MouseDown += (sender, e) => EnterOnBtnGameOver(ResetGame);
            btnGameOver.MouseEnter += MouseOverBtnGameOverEvent;
            btnGameOver.MouseLeave += MouseLeaveBtnGameOverEvent;
        }
        public void RemoveElemenntsFromCanvas(Canvas mainCanvas)
        {
            gameOverCanvas.Children.Remove(btnGameOver);
            guiCanvas.Children.Remove(btnReset);
            guiCanvas.Children.Remove(btnBack);
            starterCanvas.Children.Remove(btnPlay);
            winCanvas.Children.Remove(btnContinu);
            mainCanvas.Children.Remove(starterCanvas);
            mainCanvas.Children.Remove(btnMenu);
            mainCanvas.Children.Remove(gameOverCanvas);
            mainCanvas.Children.Remove(guiCanvas);
        }
        public void AddElementToCanvas(Canvas mainCanvas)
        {
            guiCanvas.Children.Add(btnReset);
            guiCanvas.Children.Add(btnBack);
            starterCanvas.Children.Add(btnPlay);
            gameOverCanvas.Children.Add(btnGameOver);
            winCanvas.Children.Add(btnContinu);
            mainCanvas.Children.Add(btnMenu);
            mainCanvas.Children.Add(guiCanvas);
            mainCanvas.Children.Add(starterCanvas);
            mainCanvas.Children.Add(winCanvas);
            mainCanvas.Children.Add(gameOverCanvas);
        }
        public void AddColorToCanvas()
        {
            starterCanvas.Background = new SolidColorBrush(Colors.Black);
            starterCanvas.Background.Opacity = new Double();
            starterCanvas.Background.Opacity = 0.65;

            guiCanvas.Background = new SolidColorBrush(Colors.Black);
            guiCanvas.Background.Opacity = new Double();
            guiCanvas.Background.Opacity = 0.65;

            winCanvas.Background = new SolidColorBrush(Colors.Black);
            winCanvas.Background.Opacity = new Double();
            winCanvas.Background.Opacity = 0.80;
        }
        public void AddImgToBtns()
        {
            btnPlayImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnStart.png"));
            btnMenuImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnMenu.png"));
            btnResetImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnReset.png"));
            btnBackImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnBack.png"));
            btnContinuImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnContinu.png"));
            btnGameOverImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnNewGame.png"));
         
            btnPlay.Fill = btnPlayImg; btnMenu.Fill = btnMenuImg; btnGameOver.Fill = btnGameOverImg;
            btnReset.Fill = btnResetImg; btnBack.Fill = btnBackImg; btnContinu.Fill = btnContinuImg;
        }
        public void ResponsiveGui(double H, double W)
        {
            guiCanvas.Width = W; guiCanvas.Height = H;
            starterCanvas.Width = W; starterCanvas.Height = H;
            gameOverCanvas.Width = W; gameOverCanvas.Height = H;
            winCanvas.Width = W; winCanvas.Height = H;

            btnPlay.Width = W / 5;btnPlay.Height = H / 8;
            btnReset.Width = W / 5; btnReset.Height = H / 8;
            btnGameOver.Width = W / 5; btnGameOver.Height = H / 8;
            btnBack.Width = W / 5; btnBack.Height = H / 8;
            btnMenu.Width = W / 10; btnMenu.Height = H / 16;
            btnContinu.Width = W / 5; btnContinu.Height = H / 8;

            Canvas.SetTop(btnPlay, (H / 2) - btnPlay.Height);
            Canvas.SetLeft(btnPlay, (W / 2) - (btnPlay.Width / 2));
            Canvas.SetTop(btnReset, (H / 2) - btnReset.Height);
            Canvas.SetLeft(btnReset, (W / 2) - (btnReset.Width / 2));
            Canvas.SetTop(btnGameOver, (H / 2) - btnGameOver.Height);
            Canvas.SetLeft(btnGameOver, (W / 2) - (btnGameOver.Width / 2));
            Canvas.SetTop(btnContinu, (H / 2) - btnContinu.Height);
            Canvas.SetLeft(btnContinu, (W / 2) - (btnContinu.Width / 2));
            Canvas.SetTop(btnBack, (H / 2) + 5 + btnReset.Height + 5);
            Canvas.SetLeft(btnBack, (W / 2) - (btnBack.Width / 2));
            Canvas.SetTop(btnMenu, 5);
            Canvas.SetLeft(btnMenu, W - (btnMenu.Width + 15));
        }
        public void ShowStarterGui()
        {
            starterCanvas.Visibility = Visibility.Visible;
        }
        public void ShowWinCanvas()
        {
            winCanvas.Visibility = Visibility.Visible;
        }
        public void ShowGameOverCanvas()
        {
            gameOverCanvas.Visibility = Visibility.Visible;
        }
        public void ShowGui(DispatcherTimer GameTimer)
        {
            GameTimer.Stop();
            guiCanvas.Visibility = Visibility.Visible;
        }
        // btns events: 
        private void MouseLeaveBtnGameOverEvent(object sender, MouseEventArgs e)
        {
            btnGameOverImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnNewGame.png"));
        }
        private void MouseOverBtnGameOverEvent(object sender, MouseEventArgs e)
        {
            btnGameOverImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnNewGamePressed.png"));
        }
        private void EnterOnBtnGameOver(Action ResetGame)
        {
            gameOverCanvas.Visibility = Visibility.Collapsed;
            ResetGame();
        }
        private void MouseLeaveBtnContinuEvent(object sender, MouseEventArgs e)
        {
            btnContinuImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnContinu.png"));
        }
        private void MouseOverBtnContinuEvent(object sender, MouseEventArgs e)
        {
            btnContinuImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnContinuPressed.png"));
        }
        private void EnterOnBtnContinu(Action ResetGame)
        {
            ResetGame();
            winCanvas.Visibility = Visibility.Collapsed;
        }
        private void MouseLeaveBtnBackEvent(object sender, MouseEventArgs e)
        {
            btnBackImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnBack.png"));
        }
        private void MouseOverBtnBackEvent(object sender, MouseEventArgs e)
        {
            btnBackImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnBackPressed.png"));
        }
        private void EnterOnBtnBack(DispatcherTimer GameTimer)
        {
            GameTimer.Start();
            guiCanvas.Visibility = Visibility.Collapsed;
        }
        private void MouseLeavebtnResetEvent(object sender, MouseEventArgs e)
        {
            btnResetImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnReset.png"));
        }
        private void MouseOverbtnResetEvent(object sender, MouseEventArgs e)
        {
            btnResetImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnResetPressed.png"));
        }
        private void EnterOnbtnReset(Action ResetGame)
        {
            ResetGame();
            guiCanvas.Visibility = Visibility.Collapsed;
        }
        private void MouseLeaveBtnMenuEvent(object sender, MouseEventArgs e)
        {
            btnMenuImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnMenu.png"));
        }
        private void MouseOverBtnMenuEvent(object sender, MouseEventArgs e)
        {
            btnMenuImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnMenuPressed.png"));
        }
        private void EnterOnBtnMenu(DispatcherTimer gameTimer)
        {
            guiCanvas.Visibility = Visibility.Visible;
            gameTimer.Stop();
        }
        private void MouseLeaveBtnPlayEvent(object sender, MouseEventArgs e)
        {
            btnPlayImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnStart.png"));
        }
        private void MouseOverBtnPlayEvent(object sender, MouseEventArgs e)
        {
            btnPlayImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/menuInterface/btnStartPressed.png"));
        }
        public void EnterOnBtnStart(DispatcherTimer GameTimer)
        {
            showMenuInterface = false;
            GameTimer.Start();
            starterCanvas.Visibility = Visibility.Collapsed;
        }
    }
}
