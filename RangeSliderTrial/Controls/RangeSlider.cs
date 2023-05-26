﻿using System.Runtime.CompilerServices;
using static System.Math;
using static Microsoft.Maui.Controls.AbsoluteLayout;
using Microsoft.Maui.Controls.Shapes;

namespace RangeSliderTrial.Controls
{
    public class RangeSlider : BaseTemplatedView<AbsoluteLayout>
    {
        const double enabledOpacity = 1;

        const double disabledOpacity = .6;

        
        public event EventHandler? ValueChanged;

        
        public event EventHandler? LowerValueChanged;

        
        public event EventHandler? UpperValueChanged;

        
        public event EventHandler? DragStarted;

        
        public event EventHandler? LowerDragStarted;

        
        public event EventHandler? UpperDragStarted;

        
        public event EventHandler? DragCompleted;

        
        public event EventHandler? LowerDragCompleted;

        
        public event EventHandler? UpperDragCompleted;

        public static BindableProperty MinimumValueProperty
            = BindableProperty.Create(nameof(MinimumValue), typeof(double), typeof(RangeSlider), .0, propertyChanged: OnMinimumMaximumValuePropertyChanged);

        public static BindableProperty MaximumValueProperty
            = BindableProperty.Create(nameof(MaximumValue), typeof(double), typeof(RangeSlider), 1.0, propertyChanged: OnMinimumMaximumValuePropertyChanged);

        public static BindableProperty StepValueProperty
            = BindableProperty.Create(nameof(StepValue), typeof(double), typeof(RangeSlider), 0.0, propertyChanged: OnMinimumMaximumValuePropertyChanged);

        public static BindableProperty LowerValueProperty
            = BindableProperty.Create(nameof(LowerValue), typeof(double), typeof(RangeSlider), .0, BindingMode.TwoWay, propertyChanged: OnLowerUpperValuePropertyChanged, coerceValue: CoerceValue);

        public static BindableProperty UpperValueProperty
            = BindableProperty.Create(nameof(UpperValue), typeof(double), typeof(RangeSlider), 1.0, BindingMode.TwoWay, propertyChanged: OnLowerUpperValuePropertyChanged, coerceValue: CoerceValue);

        public static BindableProperty ThumbSizeProperty
            = BindableProperty.Create(nameof(ThumbSize), typeof(double), typeof(RangeSlider), 28.0, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty LowerThumbSizeProperty
            = BindableProperty.Create(nameof(LowerThumbSize), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty UpperThumbSizeProperty
            = BindableProperty.Create(nameof(UpperThumbSize), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty TrackSizeProperty
            = BindableProperty.Create(nameof(TrackSize), typeof(double), typeof(RangeSlider), 4.0, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty ThumbColorProperty
            = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty LowerThumbColorProperty
            = BindableProperty.Create(nameof(LowerThumbColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty UpperThumbColorProperty
            = BindableProperty.Create(nameof(UpperThumbColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty TrackColorProperty
            = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty TrackHighlightColorProperty
            = BindableProperty.Create(nameof(TrackHighlightColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty ThumbBorderColorProperty
            = BindableProperty.Create(nameof(ThumbBorderColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty LowerThumbBorderColorProperty
            = BindableProperty.Create(nameof(LowerThumbBorderColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty UpperThumbBorderColorProperty
            = BindableProperty.Create(nameof(UpperThumbBorderColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty TrackBorderColorProperty
            = BindableProperty.Create(nameof(TrackBorderColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty TrackHighlightBorderColorProperty
            = BindableProperty.Create(nameof(TrackHighlightBorderColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty ValueLabelStyleProperty
            = BindableProperty.Create(nameof(ValueLabelStyle), typeof(Style), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty LowerValueLabelStyleProperty
            = BindableProperty.Create(nameof(LowerValueLabelStyle), typeof(Style), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty UpperValueLabelStyleProperty
            = BindableProperty.Create(nameof(UpperValueLabelStyle), typeof(Style), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty ValueLabelStringFormatProperty
            = BindableProperty.Create(nameof(ValueLabelStringFormat), typeof(string), typeof(RangeSlider), "{0:0.##}", propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty LowerThumbViewProperty
            = BindableProperty.Create(nameof(LowerThumbView), typeof(View), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty UpperThumbViewProperty
            = BindableProperty.Create(nameof(UpperThumbView), typeof(View), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty ValueLabelSpacingProperty
            = BindableProperty.Create(nameof(ValueLabelSpacing), typeof(double), typeof(RangeSlider), 5.0, propertyChanged: OnLayoutPropertyChanged);

        //public static BindableProperty ThumbStrokeShapeProperty
        //    = BindableProperty.Create(nameof(ThumbStrokeShape), typeof(Shape), typeof(RangeSlider), new Rectangle(), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty LowerThumbStrokeShapeProperty
            = BindableProperty.Create(nameof(LowerThumbStrokeShape), typeof(Shape), typeof(RangeSlider), new Rectangle(), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty UpperThumbStrokeShapeProperty
            = BindableProperty.Create(nameof(UpperThumbStrokeShape), typeof(Shape), typeof(RangeSlider), new Rectangle(), propertyChanged: OnLayoutPropertyChanged);

        public static BindableProperty TrackStrokeShapeProperty
            = BindableProperty.Create(nameof(TrackStrokeShape), typeof(Shape), typeof(RangeSlider), new Rectangle(), propertyChanged: OnLayoutPropertyChanged);



        public static BindableProperty UpperThumbStrokeThicknessProperty
            = BindableProperty.Create(nameof(UpperThumbStrokeThickness), typeof(double), typeof(RangeSlider), 0.0, propertyChanged: OnLayoutPropertyChanged);

        public double UpperThumbStrokeThickness
        {
            get => (double)GetValue(UpperThumbStrokeThicknessProperty);
            set => SetValue(UpperThumbStrokeThicknessProperty, value);
        }

        public static BindableProperty LowerThumbStrokeThicknessProperty
            = BindableProperty.Create(nameof(LowerThumbStrokeThickness), typeof(double), typeof(RangeSlider), 0.0, propertyChanged: OnLayoutPropertyChanged);

        public double LowerThumbStrokeThickness
        {
            get => (double)GetValue(LowerThumbStrokeThicknessProperty);
            set => SetValue(LowerThumbStrokeThicknessProperty, value);
        }

        public static BindableProperty TrackStrokeThicknessProperty
            = BindableProperty.Create(nameof(TrackStrokeThickness), typeof(double), typeof(RangeSlider), 0.0, propertyChanged: OnLayoutPropertyChanged);

        public double TrackStrokeThickness
        {
            get => (double)GetValue(TrackStrokeThicknessProperty);
            set => SetValue(TrackStrokeThicknessProperty, value);
        }



        public static BindableProperty UpperThumbShadowProperty
            = BindableProperty.Create(nameof(UpperThumbShadow), typeof(Shadow), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

        public Shadow UpperThumbShadow
        {
            get => (Shadow)GetValue(UpperThumbShadowProperty);
            set => SetValue(UpperThumbShadowProperty, value);
        }

        public static BindableProperty LowerThumbShadowProperty
            = BindableProperty.Create(nameof(LowerThumbShadow), typeof(Shadow), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

        public Shadow LowerThumbShadow
        {
            get => (Shadow)GetValue(LowerThumbShadowProperty);
            set => SetValue(LowerThumbShadowProperty, value);
        }

        public static BindableProperty TrackShadowProperty
            = BindableProperty.Create(nameof(TrackShadow), typeof(Shadow), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

        public Shadow TrackShadow
        {
            get => (Shadow)GetValue(TrackShadowProperty);
            set => SetValue(TrackShadowProperty, value);
        }

        public static BindableProperty TrackHighlightSizeProperty
            = BindableProperty.Create(nameof(TrackHighlightSize), typeof(double), typeof(RangeSlider), 0.0, propertyChanged: OnLayoutPropertyChanged);

        public double TrackHighlightSize
        {
            get => (double)GetValue(TrackHighlightSizeProperty);
            set => SetValue(TrackHighlightSizeProperty, value);
        }


        readonly Dictionary<View, double> thumbPositionMap = new Dictionary<View, double>();

        readonly PanGestureRecognizer lowerThumbGestureRecognizer = new PanGestureRecognizer();

        readonly PanGestureRecognizer upperThumbGestureRecognizer = new PanGestureRecognizer();

        Size allocatedSize;

        double labelMaxHeight;

        double lowerTranslation;

        double upperTranslation;

        int dragCount;

        public double MinimumValue
        {
            get => (double)GetValue(MinimumValueProperty);
            set => SetValue(MinimumValueProperty, value);
        }

        public double MaximumValue
        {
            get => (double)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }

        public double StepValue
        {
            get => (double)GetValue(StepValueProperty);
            set => SetValue(StepValueProperty, value);
        }

        public double LowerValue
        {
            get => (double)GetValue(LowerValueProperty);
            set => SetValue(LowerValueProperty, value);
        }

        public double UpperValue
        {
            get => (double)GetValue(UpperValueProperty);
            set => SetValue(UpperValueProperty, value);
        }

        public double ThumbSize
        {
            get => (double)GetValue(ThumbSizeProperty);
            set => SetValue(ThumbSizeProperty, value);
        }

        public double LowerThumbSize
        {
            get => (double)GetValue(LowerThumbSizeProperty);
            set => SetValue(LowerThumbSizeProperty, value);
        }

        public double UpperThumbSize
        {
            get => (double)GetValue(UpperThumbSizeProperty);
            set => SetValue(UpperThumbSizeProperty, value);
        }

        public double TrackSize
        {
            get => (double)GetValue(TrackSizeProperty);
            set => SetValue(TrackSizeProperty, value);
        }

        public Color ThumbColor
        {
            get => (Color)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        public Color LowerThumbColor
        {
            get => (Color)GetValue(LowerThumbColorProperty);
            set => SetValue(LowerThumbColorProperty, value);
        }

        public Color UpperThumbColor
        {
            get => (Color)GetValue(UpperThumbColorProperty);
            set => SetValue(UpperThumbColorProperty, value);
        }

        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        public Color TrackHighlightColor
        {
            get => (Color)GetValue(TrackHighlightColorProperty);
            set => SetValue(TrackHighlightColorProperty, value);
        }

        public Color ThumbBorderColor
        {
            get => (Color)GetValue(ThumbBorderColorProperty);
            set => SetValue(ThumbBorderColorProperty, value);
        }

        public Color LowerThumbBorderColor
        {
            get => (Color)GetValue(LowerThumbBorderColorProperty);
            set => SetValue(LowerThumbBorderColorProperty, value);
        }

        public Color UpperThumbBorderColor
        {
            get => (Color)GetValue(UpperThumbBorderColorProperty);
            set => SetValue(UpperThumbBorderColorProperty, value);
        }

        public Color TrackBorderColor
        {
            get => (Color)GetValue(TrackBorderColorProperty);
            set => SetValue(TrackBorderColorProperty, value);
        }

        public Color TrackHighlightBorderColor
        {
            get => (Color)GetValue(TrackHighlightBorderColorProperty);
            set => SetValue(TrackHighlightBorderColorProperty, value);
        }

        public Style ValueLabelStyle
        {
            get => (Style)GetValue(ValueLabelStyleProperty);
            set => SetValue(ValueLabelStyleProperty, value);
        }

        public Style LowerValueLabelStyle
        {
            get => (Style)GetValue(LowerValueLabelStyleProperty);
            set => SetValue(LowerValueLabelStyleProperty, value);
        }

        public Style UpperValueLabelStyle
        {
            get => (Style)GetValue(UpperValueLabelStyleProperty);
            set => SetValue(UpperValueLabelStyleProperty, value);
        }

        public string ValueLabelStringFormat
        {
            get => (string)GetValue(ValueLabelStringFormatProperty);
            set => SetValue(ValueLabelStringFormatProperty, value);
        }

        public View? LowerThumbView
        {
            get => (View?)GetValue(LowerThumbViewProperty);
            set => SetValue(LowerThumbViewProperty, value);
        }

        public View? UpperThumbView
        {
            get => (View?)GetValue(UpperThumbViewProperty);
            set => SetValue(UpperThumbViewProperty, value);
        }

        public double ValueLabelSpacing
        {
            get => (double)GetValue(ValueLabelSpacingProperty);
            set => SetValue(ValueLabelSpacingProperty, value);
        }

        //public Shape ThumbStrokeShape
        //{
        //    get => (Shape)GetValue(ThumbStrokeShapeProperty);
        //    set => SetValue(ThumbStrokeShapeProperty, value);
        //}
        public Shape LowerThumbStrokeShape
        {
            get => (Shape)GetValue(LowerThumbStrokeShapeProperty);
            set => SetValue(LowerThumbStrokeShapeProperty, value);
        }

        public Shape UpperThumbStrokeShape
        {
            get => (Shape)GetValue(UpperThumbStrokeShapeProperty);
            set => SetValue(UpperThumbStrokeShapeProperty, value);
        }

        public Shape TrackStrokeShape
        {
            get => (Shape)GetValue(TrackStrokeShapeProperty);
            set => SetValue(TrackStrokeShapeProperty, value);
        }

        Border Track { get; } = CreateBorderElement<Border>();

        Border TrackHighlight { get; } = CreateBorderElement<Border>();

        Border LowerThumb { get; } = CreateBorderElement<ThumbBorder>();

        Border UpperThumb { get; } = CreateBorderElement<ThumbBorder>();

        Label LowerValueLabel { get; } = CreateLabelElement();

        Label UpperValueLabel { get; } = CreateLabelElement();

        double TrackWidth => Width - LowerThumb.Width - UpperThumb.Width;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(IsEnabled):
                    OnIsEnabledChanged();
                    break;
                case nameof(LowerValue):
                    RaiseEvent(LowerValueChanged);
                    RaiseEvent(ValueChanged);
                    break;
                case nameof(UpperValue):
                    RaiseEvent(UpperValueChanged);
                    RaiseEvent(ValueChanged);
                    break;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width == allocatedSize.Width && height == allocatedSize.Height)
                return;

            allocatedSize = new Size(width, height);
            OnLayoutPropertyChanged();
        }

        protected override void OnControlInitialized(AbsoluteLayout control)
        {
            control.Children.Add(Track);
            control.Children.Add(TrackHighlight);
            control.Children.Add(LowerThumb);
            control.Children.Add(UpperThumb);
            control.Children.Add(LowerValueLabel);
            control.Children.Add(UpperValueLabel);

            AddGestureRecognizer(LowerThumb, lowerThumbGestureRecognizer);
            AddGestureRecognizer(UpperThumb, upperThumbGestureRecognizer);
            Track.SizeChanged += OnViewSizeChanged;
            LowerThumb.SizeChanged += OnViewSizeChanged;
            UpperThumb.SizeChanged += OnViewSizeChanged;
            LowerValueLabel.SizeChanged += OnViewSizeChanged;
            UpperValueLabel.SizeChanged += OnViewSizeChanged;
            OnIsEnabledChanged();
            OnLayoutPropertyChanged();
        }

        static Border CreateBorderElement<TBorder>() where TBorder : Border, new()
        {
            var Border = new TBorder
            {
                Padding = 0
            };

            return Border;
        }

        static Label CreateLabelElement()
            => new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.NoWrap,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };

        static object CoerceValue(BindableObject bindable, object value)
            => ((RangeSlider)bindable).CoerceValue((double)value);

        static void OnMinimumMaximumValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RangeSlider)bindable).OnMinimumMaximumValuePropertyChanged();
            OnLowerUpperValuePropertyChanged(bindable, oldValue, newValue);
        }

        static void OnLowerUpperValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
            => ((RangeSlider)bindable).OnLowerUpperValuePropertyChanged();

        static void OnLayoutPropertyChanged(BindableObject bindable, object oldValue, object newValue)
            => ((RangeSlider)bindable).OnLayoutPropertyChanged();

        void OnIsEnabledChanged()
        {
            if (Control == null)
                return;

            Control.Opacity = IsEnabled
                ? enabledOpacity
                : disabledOpacity;
        }

        double CoerceValue(double value)
        {
            if (StepValue > 0 && value < MaximumValue)
            {
                var stepIndex = (int)((value - MinimumValue) / StepValue);
                value = MinimumValue + (stepIndex * StepValue);
            }
            return Clamp(value,MinimumValue, MaximumValue);
        }

        void OnMinimumMaximumValuePropertyChanged()
        {
            LowerValue = CoerceValue(LowerValue);
            UpperValue = CoerceValue(UpperValue);
        }

        void OnLowerUpperValuePropertyChanged()
        {
            var rangeValue = MaximumValue - MinimumValue;
            var trackWidth = TrackWidth;

            lowerTranslation = (LowerValue - MinimumValue) / rangeValue * trackWidth;
            upperTranslation = ((UpperValue - MinimumValue) / rangeValue * trackWidth) + LowerThumb.Width;

            LowerThumb.TranslationX = lowerTranslation;
            UpperThumb.TranslationX = upperTranslation;
            OnValueLabelTranslationChanged();

            var bounds = GetLayoutBounds(TrackHighlight);
            SetLayoutBounds(TrackHighlight, new Rect(lowerTranslation, bounds.Y, upperTranslation - lowerTranslation + UpperThumb.Width, bounds.Height));
        }

        void OnValueLabelTranslationChanged()
        {
            var labelSpacing = 5;
            var lowerLabelTranslation = lowerTranslation + ((LowerThumb.Width - LowerValueLabel.Width) / 2);
            var upperLabelTranslation = upperTranslation + ((UpperThumb.Width - UpperValueLabel.Width) / 2);
            LowerValueLabel.TranslationX = Min(Max(lowerLabelTranslation, 0), Width - LowerValueLabel.Width - UpperValueLabel.Width - labelSpacing);
            UpperValueLabel.TranslationX = Min(Max(upperLabelTranslation, LowerValueLabel.TranslationX + LowerValueLabel.Width + labelSpacing), Width - UpperValueLabel.Width);
        }

        void OnLayoutPropertyChanged()
        {
            BatchBegin();
            Track.BatchBegin();
            TrackHighlight.BatchBegin();
            LowerThumb.BatchBegin();
            UpperThumb.BatchBegin();
            LowerValueLabel.BatchBegin();
            UpperValueLabel.BatchBegin();

            var lowerThumbColor = GetColorOrDefault(LowerThumbColor, ThumbColor);
            var upperThumbColor = GetColorOrDefault(UpperThumbColor, ThumbColor);
            var lowerThumbBorderColor = GetColorOrDefault(LowerThumbBorderColor, ThumbBorderColor);
            var upperThumbBorderColor = GetColorOrDefault(UpperThumbBorderColor, ThumbBorderColor);

            LowerThumb.Stroke = lowerThumbBorderColor;
            UpperThumb.Stroke = upperThumbBorderColor;
            LowerThumb.BackgroundColor = GetColorOrDefault(lowerThumbColor, Colors.White);
            UpperThumb.BackgroundColor = GetColorOrDefault(upperThumbColor, Colors.White);
            Track.BackgroundColor = GetColorOrDefault(TrackColor, Color.FromRgb(182, 182, 182));
            TrackHighlight.BackgroundColor = GetColorOrDefault(TrackHighlightColor, Color.FromRgb(46, 124, 246));
            Track.Stroke = GetColorOrDefault(TrackBorderColor, Colors.Gray);
            TrackHighlight.Stroke = GetColorOrDefault(TrackHighlightBorderColor, Colors.Gray);

            var trackSize = TrackSize;
            //var trackRadius = (float)GetDoubleOrDefault(TrackRadius, trackSize / 2);
            var lowerThumbSize = GetDoubleOrDefault(LowerThumbSize, ThumbSize);
            var upperThumbSize = GetDoubleOrDefault(UpperThumbSize, ThumbSize);
            Track.StrokeShape = TrackStrokeShape;
            TrackHighlight.StrokeShape = TrackStrokeShape;

            LowerThumb.StrokeShape = LowerThumbStrokeShape;
            UpperThumb.StrokeShape = UpperThumbStrokeShape;

            Track.StrokeThickness = TrackStrokeThickness;
            UpperThumb.StrokeThickness = UpperThumbStrokeThickness;
            LowerThumb.StrokeThickness = LowerThumbStrokeThickness;


            LowerThumb.Shadow = LowerThumbShadow;
            Track.Shadow = TrackShadow;
            UpperThumb.Shadow = UpperThumbShadow;


            LowerThumb.Content = LowerThumbView;
            UpperThumb.Content = UpperThumbView;

            var labelWithSpacingHeight = Max(Max(LowerValueLabel.Height, UpperValueLabel.Height), 0);
            if (labelWithSpacingHeight > 0)
                labelWithSpacingHeight += ValueLabelSpacing;

            var trackThumbHeight = Max(Max(lowerThumbSize, upperThumbSize), trackSize);
            var trackVerticalPosition = labelWithSpacingHeight + ((trackThumbHeight - trackSize) / 2);
            var lowerThumbVerticalPosition = labelWithSpacingHeight + ((trackThumbHeight - lowerThumbSize) / 2);
            var upperThumbVerticalPosition = labelWithSpacingHeight + ((trackThumbHeight - upperThumbSize) / 2);

            if (Control != null)
                Control.HeightRequest = labelWithSpacingHeight + trackThumbHeight;

            var trackHighlightSize = TrackHighlightSize > 0 ? TrackHighlightSize : trackSize;

            var highlightTrackVerticalPosition = trackVerticalPosition + (trackSize - trackHighlightSize) / 2;

            var trackHighlightBounds = GetLayoutBounds(TrackHighlight);
            SetLayoutBounds(TrackHighlight, new Rect(trackHighlightBounds.X, highlightTrackVerticalPosition, trackHighlightBounds.Width, trackHighlightSize));
            SetLayoutBounds(Track, new Rect(0, trackVerticalPosition, Width, trackSize));
            SetLayoutBounds(LowerThumb, new Rect(0, lowerThumbVerticalPosition, lowerThumbSize, lowerThumbSize));
            SetLayoutBounds(UpperThumb, new Rect(0, upperThumbVerticalPosition, upperThumbSize, upperThumbSize));
            SetLayoutBounds(LowerValueLabel, new Rect(0, 0, -1, -1));
            SetLayoutBounds(UpperValueLabel, new Rect(0, 0, -1, -1));
            SetValueLabelBinding(LowerValueLabel, LowerValueProperty);
            SetValueLabelBinding(UpperValueLabel, UpperValueProperty);
            LowerValueLabel.Style = LowerValueLabelStyle ?? ValueLabelStyle;
            UpperValueLabel.Style = UpperValueLabelStyle ?? ValueLabelStyle;

            OnLowerUpperValuePropertyChanged();

            Track.BatchCommit();
            TrackHighlight.BatchCommit();
            LowerThumb.BatchCommit();
            UpperThumb.BatchCommit();
            LowerValueLabel.BatchCommit();
            UpperValueLabel.BatchCommit();
            BatchCommit();
        }

        void OnViewSizeChanged(object? sender, System.EventArgs e)
        {
            var maxHeight = Max(LowerValueLabel.Height, UpperValueLabel.Height);
            if ((sender == LowerValueLabel || sender == UpperValueLabel) && labelMaxHeight == maxHeight)
            {
                Device.BeginInvokeOnMainThread(OnValueLabelTranslationChanged);
                return;
            }

            labelMaxHeight = maxHeight;
            OnLayoutPropertyChanged();
        }

        void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            var view = (View)(sender ?? throw new NullReferenceException($"{nameof(sender)} cannot be null"));

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    OnPanStarted(view);
                    break;
                case GestureStatus.Running:
                    OnPanRunning(view, e.TotalX);
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    OnPanCompleted(view);
                    break;
            }
        }

        void OnPanStarted(View view)
        {
            thumbPositionMap[view] = view.TranslationX;
            RaiseEvent(view == LowerThumb
                ? LowerDragStarted
                : UpperDragStarted);

            if (Interlocked.Increment(ref dragCount) == 1)
                RaiseEvent(DragStarted);
        }

        void OnPanRunning(View view, double value)
            => UpdateValue(view, value + GetPanShiftValue(view));

        void OnPanCompleted(View view)
        {
            thumbPositionMap[view] = view.TranslationX;
            RaiseEvent(view == LowerThumb
                ? LowerDragCompleted
                : UpperDragCompleted);

            if (Interlocked.Decrement(ref dragCount) == 0)
                RaiseEvent(DragCompleted);
        }

        void UpdateValue(View view, double value)
        {
            var rangeValue = MaximumValue - MinimumValue;
            if (view == LowerThumb)
            {
                LowerValue = Min(Max(MinimumValue, (value / TrackWidth * rangeValue) + MinimumValue), UpperValue);
                return;
            }
            UpperValue = Min(Max(LowerValue, ((value - LowerThumb.Width) / TrackWidth * rangeValue) + MinimumValue), MaximumValue);
        }

        double GetPanShiftValue(View view)
            => DeviceInfo.Platform == DevicePlatform.Android
                ? view.TranslationX
                : thumbPositionMap[view];

        void SetValueLabelBinding(Label label, BindableProperty bindableProperty)
            => label.SetBinding(Label.TextProperty, new Binding
            {
                Source = this,
                Path = bindableProperty.PropertyName,
                StringFormat = ValueLabelStringFormat
            });

        void AddGestureRecognizer(View view, PanGestureRecognizer gestureRecognizer)
        {
            gestureRecognizer.PanUpdated += OnPanUpdated;
            view.GestureRecognizers.Add(gestureRecognizer);
        }

        Color GetColorOrDefault(Color color, Color defaultColor)
            => color == Colors.Gray
                ? defaultColor
                : color;

        double GetDoubleOrDefault(double value, double defaultSize)
            => value < 0
                ? defaultSize
                : value;

        void RaiseEvent(EventHandler? eventHandler)
            => eventHandler?.Invoke(this, EventArgs.Empty);
    }
}
