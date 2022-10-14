using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DooGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PhysicsDecor ground = new PhysicsDecor();
        private Player player = new Player();


        DispatcherTimer GameTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
            ground.InitialDecor(mainCanvas);
            player.InitialPlayer(mainCanvas);          
        }

        private void InitialGame()
        {
            mainCanvas.Focus();
            GameTimer.Tick += GameEngine;
            GameTimer.Interval = TimeSpan.FromMilliseconds(20);
            GameTimer.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            ground.SetGroundRects();
            player.SetPlayerRects();
            ground.GroundPhysics(player);
            player.PlayerMoveController(ground);
            ground.NoveScreen(player);
            ground.MakeBackGroundInfinity();
            ground.GroundLocator(player);
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
               player.playrRigth = true;
            if (e.Key == Key.Left)
                player.playerLeft = true;
            if (e.Key == Key.Space)
                player.playerJump = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Right)
                player.playrRigth = false;
            if (e.Key == Key.Left)
                player.playerLeft = false;
            if (e.Key == Key.Space)
                player.playerJump = false;
        }
    }
}
