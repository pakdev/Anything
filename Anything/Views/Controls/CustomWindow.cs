using System.Windows;

namespace Anything.Views.Controls
{
    public class CustomWindow : Window
    {
        private bool _haveDefaults;
        private double _defaultWidth;
        private double _defaultHeight;

        protected override Size MeasureOverride(Size availableSize)
        {
            if (!_haveDefaults)
            {
                _defaultWidth = availableSize.Width;
                _defaultHeight = availableSize.Height;
                _haveDefaults = true;
            }

            return base.MeasureOverride(availableSize);
        }
    }
}
