﻿using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TcoCore.Wpf
{
    /// <summary>
    /// Base color pallete is inspired by https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Brush-Names
    /// It's a facade of MaterialDesignXaml color system.
    /// </summary>
    public class TcoColors
    {

        public static Brush Primary => GetDynamicBrush($"PrimaryHueMidBrush", fallBackBrush: BrushFromHex("#5a7785"));
        public static Brush OnPrimary => GetDynamicBrush($"PrimaryHueMidForegroundBrush", fallBackBrush: Brushes.White);

        //public static Brush Secondary => GetDynamicBrush($"SecondaryHueMidBrush", fallBackBrush: BrushFromHex("#bc5052"));
        //public static Brush OnSecondary => GetDynamicBrush($"SecondaryHueMidForegroundBrush", fallBackBrush: Brushes.White);

        public static Brush Accent => GetDynamicBrush($"SecondaryHueMidBrush", fallBackBrush: Brushes.OrangeRed);
        public static Brush OnAccent => GetDynamicBrush($"SecondaryHueMidForegroundBrush", fallBackBrush: Brushes.White);

        public static Brush Alert => GetDynamicBrush($"SecondaryHueDarkBrush", fallBackBrush: BrushFromHex("#bc5052"));
        public static Brush OnAlert => GetDynamicBrush($"SecondaryHueDarkForegroundBrush", fallBackBrush: Brushes.White);

        public static Brush Error => GetDynamicBrush("MaterialDesignValidationErrorBrush", fallBackBrush: BrushFromHex("#dd2c00"));
        public static Brush OnError => GetDynamicBrush("MaterialDesignDarkForeground", fallBackBrush: BrushFromHex("#dd2c00"));

    
        private static Brush GetDynamicBrush(string brushResourceKey, Brush fallBackBrush) =>
            Application.Current?.TryFindResource(brushResourceKey)?.As<Brush>() ?? fallBackBrush;


      

        private static Color GetDynamicColor(string colorKey, Color fallbackColor)
        {
            var foundColor = Application.Current.TryFindResource(colorKey);
            if (foundColor != null && foundColor is Color color)
            {
                return color;
            }
            else
            {
                return fallbackColor;
            }

        }

        private static Brush BrushFromHex(string hex) => new SolidColorBrush(ColorFromHex(hex)).Apply(x => x.Freeze());
        private static Color ColorFromHex(string hex) => (Color)ColorConverter.ConvertFromString(hex);
    }

    public class TcoResources : ResourceDictionary
    {
        public TcoResources()
        {
            AddTcoColors();
            UseDefaultGroupBox();
        }

        private void UseDefaultGroupBox()
        {
            var groupbox = new GroupBox();
            var defualtStyle = new Style(typeof(GroupBox), groupbox.Style);
            Add(typeof(GroupBox), defualtStyle);
        }

        private void AddTcoColors() => typeof(TcoColors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(Brush))
                .ForEach(x => Add(x.Name, x.GetValue(null)));
    }

    internal static class ObjectExtensions
    {
        public static T As<T>(this object @this) where T : class => (@this as T);
        public static T Apply<T>(this T @this, Action<T> func)
        {
            func.Invoke(@this);
            return @this;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> predicate)
        {
            foreach (var item in source)
            {
                predicate.Invoke(item);
            }
        }

    }
}
