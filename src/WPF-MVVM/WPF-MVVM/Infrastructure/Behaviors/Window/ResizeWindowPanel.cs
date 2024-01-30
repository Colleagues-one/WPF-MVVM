using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;
using WPF_MVVM.Infrastructure.Extensions;

namespace WPF_MVVM.Infrastructure.Behaviors.Window
{
    internal class ResizeWindowPanel : Behavior<Panel>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;

            switch (e.OriginalSource)
            {
                default: return;
                case Line line: ResizeLine(line, window); break;
                case Rectangle rect: ResizeRect(rect, window); break;
            }
        }

        private enum SizingAction
        {
            West = 1,
            East = 2,
            North = 3,
            NorthWest = 4,
            NorthEast = 5,
            South = 6,
            SouthWest = 7,
            SouthEast = 8
        }


        private static void ResizeLine(Line line, Window window)
        {
            switch (line.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.North), nint.Zero); break;
                case VerticalAlignment.Bottom:
                    window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.South), nint.Zero); break;
                default:
                    switch (line.HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.West), nint.Zero); break;
                        case HorizontalAlignment.Right:
                            window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.East), nint.Zero); break;
                    }
                    break;
            }



        }

        private static void ResizeRect(Rectangle rect, Window window)
        {
            switch (rect.VerticalAlignment)
            {
                case VerticalAlignment.Top when rect.HorizontalAlignment == HorizontalAlignment.Left:
                    window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.NorthWest), nint.Zero); break;
                case VerticalAlignment.Top when rect.HorizontalAlignment == HorizontalAlignment.Right:
                    window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.NorthEast), nint.Zero); break;
                case VerticalAlignment.Bottom when rect.HorizontalAlignment == HorizontalAlignment.Left:
                    window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.SouthWest), nint.Zero); break;
                case VerticalAlignment.Bottom when rect.HorizontalAlignment == HorizontalAlignment.Right:
                    window.SendMessage(WM.SYSCOMMAND, (nint)((int)SC.SIZE + SizingAction.SouthEast), nint.Zero); break;
            }
        }
    }


}
