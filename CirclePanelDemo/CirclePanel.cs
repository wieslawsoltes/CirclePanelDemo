using System;
using Avalonia;
using Avalonia.Controls;

namespace CirclePanelDemo;

public class CirclePanel : Panel
{
    public static readonly StyledProperty<double> StartAngleProperty =
        AvaloniaProperty.Register<CirclePanel, double>(nameof(StartAngle), defaultValue: 0);

    public double StartAngle
    {
        get { return GetValue(StartAngleProperty); }
        set { SetValue(StartAngleProperty, value); }
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        int n = Children.Count;
        double radius = Math.Min(finalSize.Width, finalSize.Height) / 2;
        double centerX = finalSize.Width / 2;
        double centerY = finalSize.Height / 2;

        for (int i = 0; i < n; i++)
        {
            double angle = 2 * Math.PI * i / n + StartAngle * Math.PI / 180;
            double x = centerX + radius * Math.Cos(angle) - Children[i].DesiredSize.Width / 2;
            double y = centerY + radius * Math.Sin(angle) - Children[i].DesiredSize.Height / 2;
            Children[i].Arrange(new Rect(x, y, Children[i].DesiredSize.Width, Children[i].DesiredSize.Height));
        }

        return finalSize;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        foreach (var child in Children)
        {
            child.Measure(availableSize);
        }

        return availableSize;
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == StartAngleProperty)
        {
            InvalidateArrange();
        }
    }
}
