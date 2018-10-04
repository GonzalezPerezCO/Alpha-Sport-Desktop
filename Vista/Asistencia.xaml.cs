﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para Asistencia.xaml
    /// </summary>
    public partial class Asistencia : Window
    {
        public Asistencia()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            this.Hide();
            tabla.Show();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            lab5.Content = "nombre y apellido";
            txt1.Text = "";
        }
    }
}
