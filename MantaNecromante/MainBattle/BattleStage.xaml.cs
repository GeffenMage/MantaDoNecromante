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
using Windows.UI.Core;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace MantaNecromante.MainBattle {
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class BattleStage : Page {
        public BattleStage() {
            this.InitializeComponent();

            Adjuster.AdjustWindow(Floor);

            //Window.Current.CoreWindow.KeyDown += keySentinel;
            this.KeyDown += BattleStage_KeyDown; ;
        }

        private void BattleStage_KeyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.X) this.Frame.GoBack();
        }

        private void keySentinel(CoreWindow sender, KeyEventArgs e) {

            if (e.VirtualKey == Windows.System.VirtualKey.E)
                this.Frame.GoBack();
        }
    }
}
