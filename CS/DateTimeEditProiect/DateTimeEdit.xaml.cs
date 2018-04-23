using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Validation.Native;
namespace DateTimeEditProiect {
    /// <summary>
    /// Interaction logic for DateTimeEdit.xaml
    /// </summary>
    public partial class DateTimeEdit : DateEdit, INotifyPropertyChanged {
        public DateTimeEdit() {
            InitializeComponent();
            descDateTime.AddValueChanged(this, OnDateTimeChanged);
            descTimeFormat.AddValueChanged(this, OnTimeFormatChanged);
            OnTimeFormatChanged(null, null);
            OnDateTimeChanged(null, null);
        }

        public bool IsShowTimePanel {
            get { return (bool)GetValue(IsShowTimePanelProperty); }
            set { SetValue(IsShowTimePanelProperty, value); }
        }
        public enumTimeFormat TimeFormat {
            get { return (enumTimeFormat)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }
        public string Time12Format {
            get { return _time12Format; }
            set {
                _time12Format = value;
                MakeTotalTime();
            }
        }
        public int DisplayHour {
            get { return _dispHour; }
            set {
                if (value > _maxHour) {
                    value = _minHour;
                }
                if (value < _minHour) {
                    value = _maxHour;
                }
                _dispHour = value;
                MakeTotalTime();
                NotifyPropertyChanged();
            }
        }
        public int RealHour {
            get { return _realHour; }
            set {
                _realHour = value;
                switch (TimeFormat) {
                    case enumTimeFormat.H24:
                        _dispHour = value;
                        break;
                    case enumTimeFormat.H12:
                        if (value == 0) {
                            _dispHour = 12;
                            _time12Format = "AM";
                            break;
                        }
                        if (value == 12) {
                            _dispHour = 12;
                            _time12Format = "PM";
                            break;
                        }
                        if (value > 12) {
                            _dispHour = value - 12;
                            _time12Format = "PM";
                            break;
                        }
                        else {
                            _dispHour = value;
                            _time12Format = "AM";
                            break;
                        }
                }
            }
        }
        public int Minute {
            get { return _minute; }
            set {
                if (value > 59) {
                    value = 0;
                }
                if (value < 0) {
                    value = 59;
                }
                _minute = value;
                MakeTotalTime();
                NotifyPropertyChanged();
            }
        }
        public int Second {
            get { return _second; }
            set {
                if (value > 59) {
                    value = 0;

                }
                if (value < 0) {
                    value = 59;

                }
                _second = value;
                MakeTotalTime();
                NotifyPropertyChanged();
            }
        }

        string _time12Format;
        int _dispHour;
        int _realHour;
        int _minute;
        int _second;
        int _maxHour;
        int _minHour;

        public static readonly DependencyProperty IsShowTimePanelProperty = DependencyProperty.Register("IsShowTimePanel", typeof(bool), typeof(DateTimeEdit), new PropertyMetadata(false));
        public static readonly DependencyProperty TimeFormatProperty = DependencyProperty.Register("TimeFormat", typeof(enumTimeFormat), typeof(DateTimeEdit), new PropertyMetadata(enumTimeFormat.Auto));

        DependencyPropertyDescriptor descDateTime = DependencyPropertyDescriptor.FromProperty(DateTimeProperty, typeof(DateTimeEdit));
        DependencyPropertyDescriptor descTimeFormat = DependencyPropertyDescriptor.FromProperty(TimeFormatProperty, typeof(DateTimeEdit));

        void MakeTotalTime() {
            int tempDispHour = DisplayHour;
            switch (TimeFormat) {
                case enumTimeFormat.H24:
                    _realHour = tempDispHour;
                    break;
                case enumTimeFormat.H12:
                    if (DisplayHour == 12) {
                        tempDispHour = 0;
                    }
                    if (Time12Format == "AM")
                        _realHour = tempDispHour;
                    else
                        _realHour = 12 + tempDispHour;
                    break;
            }
            //   EditStrategy.SetDateTime(this.DateTime.Date + new TimeSpan(RealHour, Minute, Second));
            EditStrategy.SetDateTime(this.DateTime.Date + new TimeSpan(RealHour, Minute, Second), new UpdateEditorSource());
            if (this.Calendar != null) {//  this.Calendar.SetNewDateTime(DateTime);
                this.Calendar.DateTime = DateTime;
            }
        }
        void OnDateTimeChanged(object sender, EventArgs e) {
            RealHour = this.DateTime.Hour;
            _minute = this.DateTime.Minute;
            _second = this.DateTime.Second;
        }
        void OnTimeFormatChanged(object sender, EventArgs e) {
            enumTimeFormat value = TimeFormat;
            if (value == enumTimeFormat.Auto) {
                Thread.CurrentThread.CurrentCulture.ClearCachedData();
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                DateTimeFormatInfo df = cI.DateTimeFormat;
                if (df.ShortTimePattern[0] == 'H')
                    value = enumTimeFormat.H24;
                else
                    value = enumTimeFormat.H12;
            }
            if (value == enumTimeFormat.H24) {
                _maxHour = 23;
                _minHour = 0;
            }
            else {
                _maxHour = 12;
                _minHour = 1;
                Time12Format = "AM";
            }
            TimeFormat = value;
        }

        private void MakeHourUp(object sender, RoutedEventArgs e) {
            this.DisplayHour++;
        }
        private void MakeHourDown(object sender, RoutedEventArgs e) {
            this.DisplayHour--;
        }
        private void MakeMinutUp(object sender, RoutedEventArgs e) {
            this.Minute++;
        }
        private void MakeMinutDown(object sender, RoutedEventArgs e) {
            this.Minute--;
        }
        private void MakeSecondUp(object sender, RoutedEventArgs e) {
            this.Second++;
        }
        private void MakeSecondDown(object sender, RoutedEventArgs e) {
            this.Second--;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    public enum enumTimeFormat { H24, H12, Auto };

    public class TimeFormatToVisibleConverter : MarkupExtension, IValueConverter {
        public TimeFormatToVisibleConverter() { }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            enumTimeFormat tf = (enumTimeFormat)value;
            if (tf == enumTimeFormat.H12)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class TimeFormatToColumnSpanConverter : MarkupExtension, IValueConverter {
        public TimeFormatToColumnSpanConverter() { }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            enumTimeFormat tf = (enumTimeFormat)value;
            if (tf == enumTimeFormat.H12)
                return 1;
            else
                return 2;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

}
