using MeineNoten.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace MeineNoten.View
{
    /// <summary>
    /// Interaktionslogik für NewGradeWindow.xaml
    /// </summary>
    public partial class NewGradeWindow : Window
    {
        public NewGradeWindow(SchoolYearViewModel schoolYearViewModel, MarkViewModel markViewModel) : base()
        {
            this.DataContext = new NewGradeWindowViewModel(schoolYearViewModel, markViewModel);
            InitializeComponent();
        }
    }
    
}
