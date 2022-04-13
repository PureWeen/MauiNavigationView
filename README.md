# MauiNavigationView

WinUI3 bits of .NET MAUI NavigationView extracted out to a WinUI 3 only project. .NET MAUI not required to run this project.

https://github.com/dotnet/maui/pull/5811

## TODO
- Switch Implementation from `Window.SetTitleBar` to `AppWindow` APIs so that we can do draggable areas and styling
- Figure out a Windows 10 solution

### Description of Change

Previously we were just using 48 for the app bar title height. This PR makes it so the NavigationView will auto resize/pad/margin based on the height of the app titlebar. You can also now replace the AppTitleBar by specifying your own `DataTemplate` inside the `WinUI` resource file.

All of the logic here has been extracted out to
https://github.com/PureWeen/MauiNavigationView

To make playing around with this all easier

### Examples

#### Default height based on Caption

![image](https://user-images.githubusercontent.com/5375137/161636190-7b43671e-15f7-4f9b-b990-8b6563be85ec.png)


#### Replacing with your own AppTitleBar

```XAML
    <Application.Resources>
        <ResourceDictionary>
           <DataTemplate x:Key="MauiAppTitleBarTemplate">
                <TextBlock Height="300">RABBITS</TextBlock>
            </DataTemplate>
        </ResourceDictionary>
    </Application.Resources>
```

![image](https://user-images.githubusercontent.com/5375137/161636087-6da01bbe-cef8-434d-8f24-4fb055a1461e.png)

#### Replacing with your own AppTitleBar Container if you want interactive content on the edges.

```XAML
    <maui:MauiWinUIApplication.Resources>
        <DataTemplate x:Key="MauiAppTitleBarContainerTemplate">
            <Grid HorizontalAlignment="Stretch">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="100"></ColumnDefinition>
                    <ColumnDefinition Width="100"></ColumnDefinition>
                    <ColumnDefinition Width="100"></ColumnDefinition>
                    <ColumnDefinition Width="100"></ColumnDefinition>
                    <ColumnDefinition Width="*"></ColumnDefinition>
                </Grid.ColumnDefinitions>
                <Button Grid.Column="0" Content="Click Me"></Button>
                <Button Grid.Column="1" Content="Click Me"></Button>
                <Button Grid.Column="2" Content="Click Me"></Button>
                <Button Grid.Column="3" Content="Click Me"></Button>
                <ContentControl
                    Grid.Column="4"
                    IsTabStop="False"
                    HorizontalContentAlignment="Stretch"
                    VerticalContentAlignment="Stretch"
                    x:Name="AppTitleBarContentControl">
                </ContentControl>
            </Grid>
        </DataTemplate>
    </maui:MauiWinUIApplication.Resources>
```

![image](https://user-images.githubusercontent.com/5375137/162055938-ac00c65e-1462-4139-9807-c0cd09738836.png)
