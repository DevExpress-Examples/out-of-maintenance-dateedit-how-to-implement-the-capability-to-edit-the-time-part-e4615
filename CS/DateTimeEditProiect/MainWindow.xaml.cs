﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;




namespace DateTimeEditProiect {
    public partial class MainWindow : Window,INotifyPropertyChanged {
        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            MyDateProperty = new DateTime(2010, 10, 10);
        }
        DateTime _myDateProperty;

        public DateTime MyDateProperty {
            get { return _myDateProperty; }
            set { _myDateProperty = value;

            RaisePropertyChanged("MyDateProperty");
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


}
