using MeineNoten.Model;
using MeineNoten.Class;
using MeineNoten.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MeineNoten
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
       
        // TODO: Applikation signieren https://www.libe.net/themen/Herausgeber-nicht-verifiziert--Unbekannter-Herausgeber.php#NaN 
        // TODO: Idee: TextBoxes mit Klasse und Lehrer die auch in der XML gespeichert werden

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);


                // TODO: Splash Screen und Programm immer im selben Fenster öffnen
                //SplashScreen splashScreen = new SplashScreen("Images/Splash Screen.png");
                //splashScreen.Show(true, true);

                MeineNoten.Class.MeineNoten.LoadData();


                Window mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception)
            {
                // catching unhandled exceptions
                MessageBox.Show("Programm konnte aufgrund einer unbehandelten Exception nicht gestartet werden!", "Absturz");
            }
            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            MeineNoten.Class.MeineNoten.SaveData();
        }
    }
}
