<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128644223/14.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4615)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DateTimeEdit.xaml](./CS/DateTimeEditProiect/DateTimeEdit.xaml) (VB: [DateTimeEdit.xaml](./VB/DateTimeEditProiect/DateTimeEdit.xaml))
* [DateTimeEdit.xaml.cs](./CS/DateTimeEditProiect/DateTimeEdit.xaml.cs) (VB: [DateTimeEdit.xaml.vb](./VB/DateTimeEditProiect/DateTimeEdit.xaml.vb))
* [MainWindow.xaml](./CS/DateTimeEditProiect/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/DateTimeEditProiect/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/DateTimeEditProiect/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/DateTimeEditProiect/MainWindow.xaml.vb))
<!-- default file list end -->
# DateEdit - How to implement the capability to edit the time part


<p>To implement the aforementioned capability, you can create a DateEdit descendant as shown below.</p><br />
<p>Add two properties to this descendant:</p><br />
<p>1. IsShowTimePanel, which determines whether or not the edit time panel should be shown. Type - Boolean. Default value - false.</p><br />
<p>2. TimeFormat, which determines the time format. Type - enumTimeFormat (declare in code).</p><p>Possible values:</p><p>H12 - 12 hours format.</p><p>H24 - 24 hours format.</p><p>Auto - your regional settings format.</p><br />
<p>Default value - Auto.</p>

<br/>


