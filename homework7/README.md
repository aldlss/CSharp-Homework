可能需要 `SOGOU_COOKIE` 和 `BING_SEARCH_V7_SUBSCRIPTION_KEY` 环境变量

***

现有如下代码：

```xml
<Grid Grid.Row="0">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    <StackPanel Grid.Column="0">
        <TextBlock Text="搜狗搜索结果：" TextAlignment="Center" FontSize="32"/>
        <ScrollViewer VerticalScrollBarVisibility="Auto">
            <TextBlock Text="{Binding Path=Res1}" TextWrapping="Wrap" Margin="5" Background="AliceBlue"/>
        </ScrollViewer>
    </StackPanel>
    <StackPanel Grid.Column="1">
        <TextBlock Text="必应搜索结果：" TextAlignment="Center" FontSize="32"/>
        <ScrollViewer VerticalScrollBarVisibility="Auto">
            <TextBlock Text="{Binding Path=Res2}" TextWrapping="Wrap" Margin="5" Background="AliceBlue"/>
        </ScrollViewer>
    </StackPanel>
</Grid>
```

问：为什么搜索结果的 TextBlock 里的内容 Wrap 溢出显示框的的时候并不会显示滚动条？

答：因为父级有个 StackPanel，这个控件的长度被 Warp 内容撑大溢出了，导致了显示不完全。但在 TextBook 看来 StackPanel 是给了足够的高度的，因此不会显示滚动条。如果加滚动条的话，可以猜想到加到 StackPanel 上应该是可以显示的。但是这样会导致标题也会滚动，并不符合预期，因此考虑将 StackPanel 换成 Grid。

改动后的代码：

```xml
<Grid Grid.Row="0">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <TextBlock Grid.Column="0" Grid.Row="0" Text="搜狗搜索结果：" TextAlignment="Center" FontSize="32"/>
    <ScrollViewer Grid.Column="0" Grid.Row="1" VerticalScrollBarVisibility="Auto">
        <TextBlock Text="{Binding Path=Res1}" TextWrapping="Wrap" Margin="5" Background="AliceBlue"/>
    </ScrollViewer>
    <TextBlock Grid.Column="1" Grid.Row="0" Text="必应搜索结果：" TextAlignment="Center" FontSize="32"/>
    <ScrollViewer Grid.Column="1" Grid.Row="1" VerticalScrollBarVisibility="Auto">
        <TextBlock Text="{Binding Path=Res2}" TextWrapping="Wrap" Margin="5" Background="AliceBlue"/>
    </ScrollViewer>
</Grid>
```