using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Extension;
using System.Diagnostics;
using MantaNecromante.GameStage;
using NecromanteLL;
using Windows.Media.Playback;
using Windows.Media.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MantaNecromante.ClassMenu {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    


    public sealed partial class ClassMenu : Page {
        private MediaPlayer song = new MediaPlayer();
        public ClassMenu() {
            this.InitializeComponent();
            Adjuster.AdjustWindow(Floor);
            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/Waterfall.mp3"));
            song.Play();
        }

        private void Iniciar_Gladiador(object sender, RoutedEventArgs e) {

            Warrior w = new Warrior("Jogador");
            
            this.Frame.Navigate(typeof(MainStage),w);
            
            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/ClickSound.mp3"));
            song.Play();
        }

        private void IniciarMago(object sender, RoutedEventArgs e)
        {

            Wizard m = new Wizard("Jogador");

            this.Frame.Navigate(typeof(MainStage),m);

            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/ClickSound.mp3"));
            song.Play();
        }

        private void IniciarLadina(object sender, RoutedEventArgs e)
        {

            Rogue r = new Rogue("Jogador");

            this.Frame.Navigate(typeof(MainStage),r);

            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/ClickSound.mp3"));
            song.Play();
        }
        private void Retornar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FrontEnd.MainPage));
            
            song.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///GameAssets/Songs/ClickSound.mp3"));
            song.Play();
        }
        
    }
}
