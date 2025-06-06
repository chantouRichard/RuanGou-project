﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace frontend.Controls.Navigation.CommonPage;

/// <summary>
/// Provides tools for <see cref="FrameworkElement"/> animation.
/// </summary>
public static class Transitions
{
    private const double DecelerationRatio = 0.7D;

    /// <summary>
    /// Attempts to apply an animation effect while adding content to the frame.
    /// </summary>
    /// <param name="element">Currently rendered element.</param>
    /// <param name="type">Selected transition type.</param>
    /// <param name="duration">Transition duration.</param>
    public static bool ApplyTransition(object element, TransitionType type, int duration)
    {
        if (type == TransitionType.None)
            return false;

        // Disable transitions for non-accelerated devices.
        //if (!HardwareAcceleration.IsSupported(RenderingTier.PartialAcceleration))
        //    return false;

        if (element is not FrameworkElement frameworkElement)
            return false;

        if (duration < 10)
            return false;

        if (duration > 10000)
            duration = 10000;

        var timespanDuration = new Duration(TimeSpan.FromMilliseconds(duration));

        switch (type)
        {
            case TransitionType.FadeIn:
                FadeInTransition(frameworkElement, timespanDuration);
                break;

            case TransitionType.FadeInWithSlide:
                FadeInWithSlideTransition(frameworkElement, timespanDuration);
                break;

            case TransitionType.SlideBottom:
                SlideBottomTransition(frameworkElement, timespanDuration);
                break;

            case TransitionType.SlideRight:
                SlideRightTransition(frameworkElement, timespanDuration);
                break;

            case TransitionType.SlideLeft:
                SlideLeftTransition(frameworkElement, timespanDuration);
                break;
        }

        return true;
    }

    private static void FadeInTransition(FrameworkElement navigatedElement, Duration duration)
    {
        var opacityDoubleAnimation = new DoubleAnimation
        {
            Duration = duration,
            DecelerationRatio = DecelerationRatio,
            From = 0.0,
            To = 1.0,
        };

        navigatedElement.BeginAnimation(UIElement.OpacityProperty, opacityDoubleAnimation);
    }

    private static void FadeInWithSlideTransition(
        FrameworkElement navigatedElement,
        Duration duration
    )
    {
        var translateDoubleAnimation = new DoubleAnimation
        {
            Duration = duration,
            DecelerationRatio = DecelerationRatio,
            From = 30,
            To = 0,
        };

        if (navigatedElement.RenderTransform is not TranslateTransform)
            navigatedElement!.RenderTransform = new TranslateTransform(0, 0);

        if (!navigatedElement.RenderTransformOrigin.Equals(new Point(0.5, 0.5)))
            navigatedElement!.RenderTransformOrigin = new Point(0.5, 0.5);

        navigatedElement.RenderTransform.BeginAnimation(
            TranslateTransform.YProperty,
            translateDoubleAnimation
        );

        var opacityDoubleAnimation = new DoubleAnimation
        {
            Duration = duration,
            DecelerationRatio = DecelerationRatio,
            From = 0.0,
            To = 1.0,
        };
        navigatedElement.BeginAnimation(UIElement.OpacityProperty, opacityDoubleAnimation);
    }

    private static void SlideBottomTransition(FrameworkElement navigatedElement, Duration duration)
    {
        var translateDoubleAnimation = new DoubleAnimation
        {
            Duration = duration,
            DecelerationRatio = DecelerationRatio,
            From = 30,
            To = 0,
        };

        if (navigatedElement.RenderTransform is not TranslateTransform)
            navigatedElement!.RenderTransform = new TranslateTransform(0, 0);

        if (!navigatedElement.RenderTransformOrigin.Equals(new Point(0.5, 0.5)))
            navigatedElement!.RenderTransformOrigin = new Point(0.5, 0.5);

        navigatedElement.RenderTransform.BeginAnimation(
            TranslateTransform.YProperty,
            translateDoubleAnimation
        );
    }

    private static void SlideRightTransition(FrameworkElement navigatedElement, Duration duration)
    {
        var translateDoubleAnimation = new DoubleAnimation
        {
            Duration = duration,
            DecelerationRatio = DecelerationRatio,
            From = 50,
            To = 0,
        };

        if (navigatedElement.RenderTransform is not TranslateTransform)
            navigatedElement!.RenderTransform = new TranslateTransform(0, 0);

        if (!navigatedElement.RenderTransformOrigin.Equals(new Point(0.5, 0.5)))
            navigatedElement!.RenderTransformOrigin = new Point(0.5, 0.5);

        navigatedElement.RenderTransform.BeginAnimation(
            TranslateTransform.XProperty,
            translateDoubleAnimation
        );
    }

    private static void SlideLeftTransition(FrameworkElement navigatedElement, Duration duration)
    {
        var translateDoubleAnimation = new DoubleAnimation
        {
            Duration = duration,
            DecelerationRatio = DecelerationRatio,
            From = -50,
            To = 0,
        };

        if (navigatedElement.RenderTransform is not TranslateTransform)
            navigatedElement!.RenderTransform = new TranslateTransform(0, 0);

        if (!navigatedElement.RenderTransformOrigin.Equals(new Point(0.5, 0.5)))
            navigatedElement!.RenderTransformOrigin = new Point(0.5, 0.5);

        navigatedElement.RenderTransform.BeginAnimation(
            TranslateTransform.XProperty,
            translateDoubleAnimation
        );
    }
}
