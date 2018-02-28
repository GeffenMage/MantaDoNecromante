
using MantaNecromante.ClassMenu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FrontEnd {

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


public sealed partial class MainPage : Page {

        private MediaPlayer song = new MediaPlayer();

        public MainPage() {

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            this.InitializeComponent();

            //Alinhando as Malditas imagens dinamicamente, já que o xaml é indecifrável.

            Background.Height = Window.Current.Bounds.Height;
            Background.Width = Window.Current.Bounds.Width;

            Fall.Height = Window.Current.Bounds.Height + 20;
            Fall.Width = Window.Current.Bounds.Width + 30;

            Canvas.SetTop(Fall, -10);

            Canvas.SetTop(Start, Background.Height / 2 - Start.Height / 2);
            Canvas.SetLeft(Start, Background.Width / 2 - Start.Width / 2);

            Canvas.SetLeft(Exit, Canvas.GetLeft(Start));
            Canvas.SetTop(Exit, Canvas.GetTop(Start) + Exit.Height);

            Canvas.SetLeft(Title, Background.Width / 2 - Title.Width / 2);
            Canvas.SetTop(Title, Canvas.GetTop(Start) - Title.Height - Start.Height);
        }

        private void Sair(object sender, RoutedEventArgs e) {

            CoreApplication.Exit();
        }

        private void Iniciar(object sender, RoutedEventArgs e) {

            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Songs/buttons/button.mp3"));
            song.Play();
            this.Frame.Navigate(typeof(ClassMenu));
        }

    }
}
