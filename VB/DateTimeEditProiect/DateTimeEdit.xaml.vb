Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Xpf.Editors


Namespace DateTimeEditProiect
	''' <summary>
	''' Interaction logic for DateTimeEdit.xaml
	''' </summary>
	Partial Public Class DateTimeEdit
		Inherits DateEdit
		Implements INotifyPropertyChanged
		Public Sub New()
			InitializeComponent()
			DataContext = Me
			descDateTime.AddValueChanged(Me, AddressOf OnDateTimeChanged)
			descTimeFormat.AddValueChanged(Me, AddressOf OnTimeFormatChanged)
			OnTimeFormatChanged(Nothing, Nothing)
			OnDateTimeChanged(Nothing, Nothing)
		End Sub

		Public Property IsShowTimePanel() As Boolean
			Get
				Return CBool(GetValue(IsShowTimePanelProperty))
			End Get
			Set(ByVal value As Boolean)
				SetValue(IsShowTimePanelProperty, value)
			End Set
		End Property
		Public Property TimeFormat() As enumTimeFormat
			Get
				Return CType(GetValue(TimeFormatProperty), enumTimeFormat)
			End Get
			Set(ByVal value As enumTimeFormat)
				SetValue(TimeFormatProperty, value)
			End Set
		End Property
		Public Property Time12Format() As String
			Get
				Return _time12Format
			End Get
			Set(ByVal value As String)
				_time12Format = value
				MakeTotalTime()
			End Set
		End Property
		Public Property DisplayHour() As Integer
			Get
				Return _dispHour
			End Get
			Set(ByVal value As Integer)
				If value > _maxHour Then
					value = _minHour
				End If
				If value < _minHour Then
					value = _maxHour
				End If
				_dispHour = value
				MakeTotalTime()
				NotifyPropertyChanged()
			End Set
		End Property
		Public Property RealHour() As Integer
			Get
				Return _realHour
			End Get
			Set(ByVal value As Integer)
				_realHour = value
				Select Case TimeFormat
					Case enumTimeFormat.H24
						_dispHour = value
					Case enumTimeFormat.H12
						If value = 0 Then
							_dispHour = 12
							_time12Format = "AM"
							Exit Select
						End If
						If value = 12 Then
							_dispHour = 12
							_time12Format = "PM"
							Exit Select
						End If
						If value > 12 Then
							_dispHour = value - 12
							_time12Format = "PM"
							Exit Select
						Else
							_dispHour = value
							_time12Format = "AM"
							Exit Select
						End If
				End Select
			End Set
		End Property
		Public Property Minute() As Integer
			Get
				Return _minute
			End Get
			Set(ByVal value As Integer)
				If value > 59 Then
					value = 0
				End If
				If value < 0 Then
					value = 59
				End If
				_minute = value
				MakeTotalTime()
				NotifyPropertyChanged()
			End Set
		End Property
		Public Property Second() As Integer
			Get
				Return _second
			End Get
			Set(ByVal value As Integer)
				If value > 59 Then
					value = 0

				End If
				If value < 0 Then
					value = 59

				End If
				_second = value
				MakeTotalTime()
				NotifyPropertyChanged()
			End Set
		End Property

		Private _time12Format As String
		Private _dispHour As Integer
		Private _realHour As Integer
		Private _minute As Integer
		Private _second As Integer
		Private _maxHour As Integer
		Private _minHour As Integer

		Public Shared ReadOnly IsShowTimePanelProperty As DependencyProperty = DependencyProperty.Register("IsShowTimePanel", GetType(Boolean), GetType(DateTimeEdit), New PropertyMetadata(False))
		Public Shared ReadOnly TimeFormatProperty As DependencyProperty = DependencyProperty.Register("TimeFormat", GetType(enumTimeFormat), GetType(DateTimeEdit), New PropertyMetadata(enumTimeFormat.Auto))

		Private descDateTime As DependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DateTimeProperty, GetType(DateTimeEdit))
		Private descTimeFormat As DependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(TimeFormatProperty, GetType(DateTimeEdit))

		Private Sub MakeTotalTime()
			Dim tempDispHour As Integer = DisplayHour
			Select Case TimeFormat
				Case enumTimeFormat.H24
					_realHour = tempDispHour
				Case enumTimeFormat.H12
					If DisplayHour = 12 Then
						tempDispHour = 0
					End If
					If Time12Format = "AM" Then
						_realHour = tempDispHour
					Else
						_realHour = 12 + tempDispHour
					End If
			End Select
			EditStrategy.SetDateTime(Me.DateTime.Date + New TimeSpan(RealHour, Minute, Second))
			If Me.Calendar IsNot Nothing Then
				Me.Calendar.SetNewDateTime(DateTime)
			End If
		End Sub
		Private Sub OnDateTimeChanged(ByVal sender As Object, ByVal e As EventArgs)
			RealHour = Me.DateTime.Hour
			_minute = Me.DateTime.Minute
			_second = Me.DateTime.Second
		End Sub
		Private Sub OnTimeFormatChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim value As enumTimeFormat = TimeFormat
			If value = enumTimeFormat.Auto Then
				Thread.CurrentThread.CurrentCulture.ClearCachedData()
				Dim cI As CultureInfo = Thread.CurrentThread.CurrentCulture
				Dim df As DateTimeFormatInfo = cI.DateTimeFormat
				If df.ShortTimePattern(0) = "H"c Then
					value = enumTimeFormat.H24
				Else
					value = enumTimeFormat.H12
				End If
			End If
			If value = enumTimeFormat.H24 Then
				_maxHour = 23
				_minHour = 0
			Else
				_maxHour = 12
				_minHour = 1
				Time12Format = "AM"
			End If
			TimeFormat = value
		End Sub

		Private Sub MakeHourUp(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.DisplayHour += 1
		End Sub
		Private Sub MakeHourDown(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.DisplayHour -= 1
		End Sub
		Private Sub MakeMinutUp(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.Minute += 1
		End Sub
		Private Sub MakeMinutDown(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.Minute -= 1
		End Sub
		Private Sub MakeSecondUp(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.Second += 1
		End Sub
		Private Sub MakeSecondDown(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Me.Second -= 1
		End Sub

		#Region "INotifyPropertyChanged Members"
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Private Sub NotifyPropertyChanged(<CallerMemberName> Optional propertyName As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
		#End Region
	End Class

	Public Enum enumTimeFormat
		H24
		H12
		[Auto]
	End Enum

	Public Class TimeFormatToVisibleConverter
		Inherits MarkupExtension
		Implements IValueConverter
		Public Sub New()
		End Sub

		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
			Dim tf As enumTimeFormat = CType(value, enumTimeFormat)
			If tf = enumTimeFormat.H12 Then
				Return Visibility.Visible
			Else
				Return Visibility.Collapsed
			End If
		End Function
		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function

		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function
	End Class
	Public Class TimeFormatToColumnSpanConverter
		Inherits MarkupExtension
		Implements IValueConverter
		Public Sub New()
		End Sub

		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
			Dim tf As enumTimeFormat = CType(value, enumTimeFormat)
			If tf = enumTimeFormat.H12 Then
				Return 1
			Else
				Return 2
			End If
		End Function
		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function

		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function
	End Class

End Namespace
