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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MantaNecromante.ClassMenu {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassMenu : Page {
        public ClassMenu() {

            this.InitializeComponent();
            Adjuster.AdjustWindow(Floor);
        }

        private void Iniciar(object sender, RoutedEventArgs e) {

            this.Frame.Navigate(typeof(MainStage));
        }
    }
}
