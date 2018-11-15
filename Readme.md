<!-- default file list -->
*Files to look at*:

* [DateTimeEdit.xaml](./CS/DateTimeEditProiect/DateTimeEdit.xaml) (VB: [DateTimeEdit.xaml.vb](./VB/DateTimeEditProiect/DateTimeEdit.xaml.vb))
* [DateTimeEdit.xaml.cs](./CS/DateTimeEditProiect/DateTimeEdit.xaml.cs) (VB: [DateTimeEdit.xaml.vb](./VB/DateTimeEditProiect/DateTimeEdit.xaml.vb))
* [MainWindow.xaml](./CS/DateTimeEditProiect/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/DateTimeEditProiect/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/DateTimeEditProiect/MainWindow.xaml.cs) (VB: [MainWindow.xaml](./VB/DateTimeEditProiect/MainWindow.xaml))
<!-- default file list end -->
# DateEdit - How to implement the capability to edit the time part


<p>To implement the aforementioned capability, you can create a DateEdit descendant as shown below.</p><br />
<p>Add two properties to this descendant:</p><br />
<p>1. IsShowTimePanel, which determines whether or not the edit time panel should be shown. Type - Boolean. Default value - false.</p><br />
<p>2. TimeFormat, which determines the time format. Type - enumTimeFormat (declare in code).</p><p>Possible values:</p><p>H12 - 12 hours format.</p><p>H24 - 24 hours format.</p><p>Auto - your regional settings format.</p><br />
<p>Default value - Auto.</p>

<br/>


