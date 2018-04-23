
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
using Extension;
using NecromanteLL;

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

            //Teste de tamanho ajustável:
            //......................................................................................................
            //ApplicationView.PreferredLaunchViewSize = new Size(1366, 768);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            //......................................................................................................
            this.InitializeComponent();
            
            //Classe de extensão para ajustar todas os xalm:
            Adjuster.AdjustWindow(Floor);
            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/Waterfall.mp3"));
            song.Play();
            Debug.WriteLine(Start.Height + "," + Start.Width);
        }

        private void Sair(object sender, RoutedEventArgs e) {

               song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/ClickSound.mp3"));
               song.Play();

            CoreApplication.Exit();
        }

        private void Iniciar(object sender, RoutedEventArgs e) {

            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/ClickSound.mp3"));
            song.Play();

            this.Frame.Navigate(typeof(ClassMenu));
        }

    }
}
