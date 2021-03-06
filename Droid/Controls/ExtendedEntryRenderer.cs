﻿/*
 * 소스 참조 : http://www.c-sharpcorner.com/article/xamarin-forms-custom-entry/
 */ 
using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using EntryEffectAndValidation.Core.Controls;
using EntryEffectAndValidation.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace EntryEffectAndValidation.Droid.Controls
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntry ExtendedEntryElement => Element as ExtendedEntry;

        public ExtendedEntryRenderer(Context context) : base(context)
        {

        }

        #region Override Method Area
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.InputType |= Android.Text.InputTypes.TextFlagNoSuggestions;
                InitControl();
                UpdateControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(ExtendedEntry.BorderWidthProperty.PropertyName)
                || (e.PropertyName.Equals(nameof(Element.BackgroundColor)))
                || (e.PropertyName.Equals(nameof(ExtendedEntry.BorderColorToApply))))
            {
                UpdateControl();
            }
        }
        #endregion

        #region Private Method Area
        private void InitControl()
        {
            // Set padding for the internal text from border
            Control.SetPadding(
                (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                Control.PaddingTop,
                (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                Control.PaddingBottom);
        }

        private void UpdateControl()
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetColor(ExtendedEntryElement.BackgroundColor.ToAndroid());
            drawable.SetStroke(ExtendedEntryElement.BorderWidth, ExtendedEntryElement.BorderColorToApply.ToAndroid());
            Control.SetBackground(drawable);
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
        #endregion
    }
}
