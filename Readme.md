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