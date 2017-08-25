﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ApplicationTimeCounter
{
    class MyLabel
    {
        Label myLabel = new Label();
        public MyLabel(Canvas canvas, string content, int widthLabel, int heightLabel, int labelFontSize,
            double x, double y,  string colorFont = "DarkSlateGray",
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            FontWeight fontWeight = default(FontWeight))
        {
            if (object.Equals(fontWeight, default(FontWeight))) fontWeight = FontWeights.Normal;

            myLabel = new Label()
            {
                Content = content,
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(colorFont),
                FontSize = labelFontSize,
                Width = widthLabel,
                FontWeight = fontWeight,
                Height = heightLabel,
                FontFamily = new FontFamily("Comic Sans MS"),
                HorizontalContentAlignment = horizontalAlignment,

            };
            Canvas.SetLeft(myLabel, x);
            Canvas.SetTop(myLabel, y);
            canvas.Children.Add(myLabel);

            //myLabel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("Blue");
        }

        public void SetContent(string contentLabel)
        {
            myLabel.Content = contentLabel;
        }

        public string GetContent()
        {
            return myLabel.Content.ToString();
        }

        public void Position(double x = default(double), double y = default(double))
        {
            if (object.Equals(x, default(double))) x = Canvas.GetLeft(myLabel);
            if (object.Equals(y, default(double))) y = Canvas.GetTop(myLabel);
            Canvas.SetLeft(myLabel, x);
            Canvas.SetTop(myLabel, y);
        }

        public void Opacity(double value)
        {
            myLabel.Opacity = value;
        }
    }
}