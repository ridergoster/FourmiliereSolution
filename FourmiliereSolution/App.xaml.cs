﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FourmiliereSolution
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FourmiliereViewModel FourmiliereVM { get; set; }

        public App()
        {
            FourmiliereVM = new FourmiliereViewModel(10, 10);
        }
    }
}