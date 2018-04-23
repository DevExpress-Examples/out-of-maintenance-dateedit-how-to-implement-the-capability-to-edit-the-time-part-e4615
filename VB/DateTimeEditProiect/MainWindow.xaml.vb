Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports System.ComponentModel
Imports System.Runtime.CompilerServices




Namespace DateTimeEditProiect
	Partial Public Class MainWindow
		Inherits Window
		Implements INotifyPropertyChanged
		Public Sub New()
			InitializeComponent()
			DataContext = Me
			MyDateProperty = New DateTime(2010, 10, 10)
		End Sub
		Private _myDateProperty As DateTime

		Public Property MyDateProperty() As DateTime
			Get
				Return _myDateProperty
			End Get
			Set(ByVal value As DateTime)
				_myDateProperty = value

                NotifyPropertyChanged("MyDateProperty")
			End Set
		End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Private Sub NotifyPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
	End Class


End Namespace
