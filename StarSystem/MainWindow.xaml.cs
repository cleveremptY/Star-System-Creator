using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace StarSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int roundParam = 2;

        private Planet createPlanet;

        private bool isSystemMove; //WIP

        DispatcherTimer timerSystem;

        private static readonly Regex spaceObjectParamsRegex = new Regex("^[0-9]*[,][0-9]+$");

        public MainWindow()
        {
            StarSystemParams.MainStarSystem = new StarPlanetSystem();
            InitializeComponent();
            SystemSpeed.Value = StarSystemParams.StarSystemSpeed;
            SystemSize.Value = StarSystemParams.StarSystemSize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            string basePlanetName = StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name + "-" + (StarSystemParams.MainStarSystem.Planets.Count + 1);
            Color basePlanetColor = Color.FromRgb(Convert.ToByte(rand.Next(256)),
                Convert.ToByte(rand.Next(256)),
                Convert.ToByte(rand.Next(256)));
            createPlanet = new Planet(basePlanetName, StarSystemParams.SelectedSpaceObject, basePlanetColor);
            StarSystemParams.MainStarSystem.Planets.Add(new SpaceObjectCanvasAdapter(createPlanet));
            Ellipse ellipsePlanet = StarSystemParams.MainStarSystem.Planets[StarSystemParams.MainStarSystem.Planets.Count - 1].DrawFunctional();
            Ellipse ellipsePlanetOrbit = StarSystemParams.MainStarSystem.Planets[StarSystemParams.MainStarSystem.Planets.Count - 1].DrawOrbit();
            SpaceCanvas.Children.Add(ellipsePlanetOrbit);
            SpaceCanvas.Children.Add(ellipsePlanet);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StarSystemParams.SystemCenter = new Position(SpaceCanvas.ActualWidth / 2, SpaceCanvas.ActualHeight / 2);
            SpaceCanvas.Children.Add(StarSystemParams.MainStarSystem.MainStar.DrawFunctional());

            timerSystem = new DispatcherTimer();
            timerSystem.Tick += new EventHandler(timerTick);
            timerSystem.Interval = new TimeSpan(0, 0, 0, 0, 30);

            StarSystemParams.SelectedSpaceObject = StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject;
            PrintSelectedSpaceObjectInfo();
        }

        private void SpaceCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RedrawSpaceObjectView();
            //if (StarSystemParams.SelectedSpaceObject.Name == StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name)
            //    isSystemMove = true;
        }

        private void PrintSelectedSpaceObjectInfo()
        {
            if (StarSystemParams.SelectedSpaceObject != null)
            {
                SetParamsVisibility();

                SpaceObjectName.Text = StarSystemParams.SelectedSpaceObject.Name;
                SpaceObjectRadius.Value = StarSystemParams.SelectedSpaceObject.Radius;
                if (StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name != StarSystemParams.SelectedSpaceObject.Name)
                {
                    SpaceObjectOrbitRadius.Value =
                        ((Planet)StarSystemParams.MainStarSystem.FindSpaceObject(StarSystemParams.SelectedSpaceObject.Name).BaseSpaceObject).OrbitRadius;
                    SpaceObjectSpeed.Value =
                        ((Planet)StarSystemParams.MainStarSystem.FindSpaceObject(StarSystemParams.SelectedSpaceObject.Name).BaseSpaceObject).Speed;
                    PlanetColorPicker.SelectedColor = StarSystemParams.MainStarSystem.FindSpaceObject(StarSystemParams.SelectedSpaceObject.Name).BaseSpaceObject.ObjectColor;
                    //AddSpaceObject.Visibility = Visibility.Hidden;
                }
                else
                {
                    StarType.SelectedIndex = (int)((Star)StarSystemParams.SelectedSpaceObject).Type;
                    AddSpaceObject.Visibility = Visibility.Visible;
                }
                Ellipse viewPlanet = StarSystemParams.MainStarSystem.FindSpaceObject(StarSystemParams.SelectedSpaceObject.Name).Draw(2);

                double left = (SpaceObjectView.ActualWidth - viewPlanet.Width) / 2;
                Canvas.SetLeft(viewPlanet, left);
                double top = (SpaceObjectView.ActualHeight - viewPlanet.Height) / 2;
                Canvas.SetTop(viewPlanet, top);

                SpaceObjectView.Children.Add(viewPlanet);
            }
        }

        private void SetParamsVisibility()
        {
            if (StarSystemParams.SelectedSpaceObject.Name == StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name)
            {
                PlanetParams.Visibility = Visibility.Collapsed;
                StarParams.Visibility = Visibility.Visible;
            }
            else
            {
                PlanetParams.Visibility = Visibility.Visible;
                StarParams.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RedrawAll();
        }

        private void timerTick(object sender, EventArgs e)
        {
            StarSystemParams.MainStarSystem.AnimationIteration();
            RedrawSystem();
        }

        public void RedrawAll()
        {
            RedrawSpaceObjectView();
            RedrawSystem();
        }

        public void RedrawSystem()
        {
            StarSystemParams.SystemCenter = new Position(SpaceCanvas.ActualWidth / 2, SpaceCanvas.ActualHeight / 2);
            SpaceCanvas.Children.Clear();
            SpaceCanvas.Children.Add(StarSystemParams.MainStarSystem.MainStar.DrawFunctional());
            if (StarSystemParams.MainStarSystem.Planets != null)
                foreach (var planet in StarSystemParams.MainStarSystem.Planets)
                {
                    SpaceCanvas.Children.Add(planet.DrawOrbit());
                    SpaceCanvas.Children.Add(planet.DrawFunctional());
                }
        }

        public void RedrawSpaceObjectView()
        {
            SpaceObjectView.Children.Clear();
            PrintSelectedSpaceObjectInfo();
        }

        private void SpaceObjectName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StarSystemParams.SelectedSpaceObject == null)
                return;
            if (StarSystemParams.MainStarSystem.FindSpaceObject(SpaceObjectName.Text) == null)
            {
                SpaceObjectName.Foreground = Brushes.Black;
                StarSystemParams.SelectedSpaceObject.Name = SpaceObjectName.Text;
            }
            else
            {
                if (SpaceObjectName.Text != StarSystemParams.SelectedSpaceObject.Name)
                    SpaceObjectName.Foreground = Brushes.Red;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (timerSystem.IsEnabled)
            {
                timerSystem.Stop();
                PlayButton.Content = "4";
            }
            else
            {
                timerSystem.Start();
                PlayButton.Content = ";";
            }
        }

        private void SpaceObjectRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (StarSystemParams.SelectedSpaceObject == null)
                return;
            SpaceObjectRadius.Value = Math.Round(SpaceObjectRadius.Value, roundParam);
            StarSystemParams.SelectedSpaceObject.Radius = SpaceObjectRadius.Value;
            SpaceObjectRadiusText.Text = SpaceObjectRadius.Value.ToString();
            RedrawAll();
        }

        private void SpaceObjectOrbitRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (StarSystemParams.SelectedSpaceObject == null || StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name == StarSystemParams.SelectedSpaceObject.Name)
                return;
            SpaceObjectOrbitRadius.Value = Math.Round(SpaceObjectOrbitRadius.Value, roundParam);
            ((Planet)StarSystemParams.SelectedSpaceObject).OrbitRadius = SpaceObjectOrbitRadius.Value;
            SpaceObjectOrbitRadiusText.Text = SpaceObjectOrbitRadius.Value.ToString();
            RedrawSystem();
        }

        private void SpaceObjectSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (StarSystemParams.SelectedSpaceObject == null || StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name == StarSystemParams.SelectedSpaceObject.Name)
                return;
            SpaceObjectSpeed.Value = Math.Round(SpaceObjectSpeed.Value, roundParam);
            ((Planet)StarSystemParams.SelectedSpaceObject).Speed = SpaceObjectSpeed.Value;
            SpaceObjectSpeedText.Text = SpaceObjectSpeed.Value.ToString();
        }

        private void SystemSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StarSystemParams.StarSystemSpeed = SystemSpeed.Value;
        }

        private void StarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StarSystemParams.SelectedSpaceObject == null || StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name != StarSystemParams.SelectedSpaceObject.Name)
                return;
            ((Star)StarSystemParams.SelectedSpaceObject).Type = (StarTypes)StarType.SelectedIndex;
            RedrawAll();
        }

        private void PlanetColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (StarSystemParams.SelectedSpaceObject == null || StarSystemParams.MainStarSystem.MainStar.BaseSpaceObject.Name == StarSystemParams.SelectedSpaceObject.Name)
                return;
            StarSystemParams.SelectedSpaceObject.ObjectColor = (Color)PlanetColorPicker.SelectedColor;
            RedrawAll();
        }

        private void SystemSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StarSystemParams.StarSystemSize = SystemSize.Value;
            StarSystemParams.MainStarSystem.Resize();
            RedrawAll();
        }

        private void ResetSystemButton_Click(object sender, RoutedEventArgs e)
        {
            SystemSize.Value = 1;
            SystemSpeed.Value = 1;
        }

        private void SpaceObjectRadiusText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeParamOnText(SpaceObjectRadiusText, SpaceObjectRadius);
        }

        private void SpaceObjectOrbitRadiusText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeParamOnText(SpaceObjectOrbitRadiusText, SpaceObjectOrbitRadius);
        }

        private void SpaceObjectSpeedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeParamOnText(SpaceObjectSpeedText, SpaceObjectSpeed);
        }

        private void ChangeParamOnText(TextBox textBoxParam, Slider sliderParam)
        {
            if (StarSystemParams.SelectedSpaceObject == null)
                return;
            SpaceRemove(textBoxParam);
            if (Convert.ToDouble(textBoxParam.Text) >= sliderParam.Minimum &&
                Convert.ToDouble(textBoxParam.Text) <= sliderParam.Maximum)
            {
                textBoxParam.Foreground = Brushes.Black;
                sliderParam.Value = Convert.ToDouble(textBoxParam.Text);
            }
            else
                textBoxParam.Foreground = Brushes.Red;
        }

        private void SpaceObjectParams_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = spaceObjectParamsRegex.IsMatch(e.Text);
        }

        private static void SpaceRemove(TextBox textBox)
        {
            textBox.Text = textBox.Text.Replace(" ", "");
            textBox.Text = textBox.Text.Replace(".", ",");
            if (textBox.Text == "")
                textBox.Text = "0";
        }

        private void SpaceCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSystemMove)
            {
                Canvas.SetLeft(StarSystemParams.MainStarSystem.MainStar.EllipseSpaceObject, e.GetPosition(null).X);
                Canvas.SetTop(StarSystemParams.MainStarSystem.MainStar.EllipseSpaceObject, e.GetPosition(null).Y);
                RedrawSystem();
            }
        }

        private void SpaceCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isSystemMove = false;
        }
    }
}
