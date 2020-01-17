using MeineNoten.Model;
using MeineNoten.View;
using MeineNoten.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MeineNoten
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() : base()
        {
            // TODO: GridViews sollen sich zur Laufzeit nicht verändern lassen/ DataGrid anstatt GridView?
            // TODO: Idee: Schuljahr soll grau sein, wenn keine Noten vorhanden sind
            // TODO: Idee: Roter Text bei fehlerhaften Daten
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}
