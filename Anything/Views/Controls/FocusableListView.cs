using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;

namespace Anything.Views.Controls
{
    public class FocusableListView : ListView
    {
        public override void EndInit()
        {
            base.EndInit();
            this.FocusCommand = new RelayCommand(() => 
                DispatcherHelper.CheckBeginInvokeOnUI(() => this.Focus()));
        }

        public ICommand FocusCommand
        {
            get { return (ICommand) GetValue(FocusCommandProperty); }
            set { SetValue(FocusCommandProperty, value); }
        }

        public static readonly DependencyProperty FocusCommandProperty =
            DependencyProperty.Register("FocusCommand", typeof (ICommand), typeof (FocusableListView));
    }
}
