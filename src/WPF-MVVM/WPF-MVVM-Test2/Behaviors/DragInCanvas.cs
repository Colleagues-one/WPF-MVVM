using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;

namespace WPF_MVVM_Test2.Behaviors
{
    public class DragInCanvas : Behavior<UIElement>
    {
        private Point _startPoint;
        private Canvas _canvas;

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnButtonDown;
           
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }

        private void OnButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if ((_canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null) return;
            _startPoint = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var associatedObj = AssociatedObject;
            var currentPos = e.GetPosition(_canvas);

            var delta = currentPos - _startPoint;

            associatedObj.SetValue(Canvas.LeftProperty, delta.X);
            associatedObj.SetValue(Canvas.TopProperty, delta.Y);

        }
    }
}
