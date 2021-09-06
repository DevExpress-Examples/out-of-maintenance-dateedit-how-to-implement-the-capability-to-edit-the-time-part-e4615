<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128644223/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4615)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# DateEdit - How to implement the capability to edit the time part

In version **19.2** of our controls, we created [DateEditNavigatorWithTimePickerStyleSettings](https://docs.devexpress.com/WPF/DevExpress.Xpf.Editors.DateEditNavigatorWithTimePickerStyleSettings) and [DateEditTimePickerStyleSettings](https://docs.devexpress.com/WPF/DevExpress.Xpf.Editors.DateEditTimePickerStyleSettings) that you can use in your DateEdit's [StyleS](https://docs.devexpress.com/WPF/DevExpress.Xpf.Editors.BaseEdit.StyleSettings)[ettings](https://docs.devexpress.com/WPF/DevExpress.Xpf.Editors.BaseEdit.StyleSettings) property to allow end users to change the time part in the popup:

```xaml
<dxe:DateEdit
    Mask="G"
    MaskUseAsDisplayFormat="True"
    MinValue="02/28/2019"
    MaxValue="02/20/2020">
    <dxe:DateEdit.StyleSettings>
        <dxe:DateEditNavigatorWithTimePickerStyleSettings />
    </dxe:DateEdit.StyleSettings>
</dxe:DateEdit>
```

```xaml
<dxe:DateEdit
    Mask="T"
    MaskUseAsDisplayFormat="True"
    MinValue="02/28/2019"
    MaxValue="02/20/2020">
    <dxe:DateEdit.StyleSettings>
        <dxe:DateEditTimePickerStyleSettings />
    </dxe:DateEdit.StyleSettings>
</dxe:DateEdit>
```

Use these objects in your application if you use version **19.2 or newer**. In versions **prior** to 19.2, use the approach that we illustrated in other branches of this example.
