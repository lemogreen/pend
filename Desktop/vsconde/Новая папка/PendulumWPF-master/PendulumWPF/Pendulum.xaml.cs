using System;
using System.Windows;
using HelixToolkit;
using HelixToolkit.Wpf;
using System.IO;
using System.Linq;
using  ODE;

namespace PendulumWPF
{
    public partial class Pendulum : Window
    {
        public Pendulum()
        {
            InitializeComponent();
            Create3DViewPort();
            var sol = ExplicitRungeKutta.Solve(0.01);
            //StreamWriter file = new StreamWriter("t_z_theta_0_01.txt");
            //int count = 0;
            //var s = sol.t.ToString();
            //for (int i = 0; i < 4001; i++)
            //{
            //    file.WriteLine(sol.t[i]);
            //    file.WriteLine(sol.z[i]);
            //    file.WriteLine(sol.theta[i]);
            //    count++;
            //}

            //int a = count;
            File.WriteAllLines(
                "t.txt" // <<== Put the file name here
                , sol.t.Select(d => d.ToString())
            );

            File.WriteAllLines(
                "z.txt" // <<== Put the file name here
                , sol.z.Select(d => d.ToString())
            );

            File.WriteAllLines(
                "theta.txt" // <<== Put the file name here
                , sol.theta.Select(d => d.ToString())
            );
            string path_t = "t.txt";
            string[] t_lines = File.ReadAllLines(path_t);
            string path_z = "z.txt";
            string[] z_lines = File.ReadAllLines(path_z);
            string path_theta = "theta.txt";
            string[] theta_lines = File.ReadAllLines(path_theta);
            for (int i = 0; i < 4001; i++)
            {
                sol.t[i] = Convert.ToDouble(t_lines[i]);
                sol.z[i] = Convert.ToDouble(z_lines[i]);
                sol.theta[i] = Convert.ToDouble(theta_lines[i]);
            }
        }

        private void Create3DViewPort()
        {
            var hVp3D = new HelixViewport3D();
            var lights = new DefaultLights();
            var sphere = new SphereVisual3D();
            var cube = new CubeVisual3D();
            hVp3D.Children.Add(lights);
            hVp3D.Children.Add(sphere);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new System.Windows.Window();
            window.Content = new theta_plot();
            window.Show();
        }
    }
}
    