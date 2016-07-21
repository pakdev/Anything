using Anything.Shared;
using System.ComponentModel.Composition;
using System.Windows;

namespace Tester
{
    /// <summary>
    /// Interaction logic for TesterResultTemplate.xaml
    /// </summary>
    [Export(typeof(IPluginTemplate))]
    public partial class TesterResultTemplate : ResourceDictionary, IPluginTemplate
    {
        public TesterResultTemplate()
        {
            InitializeComponent();
        }

        public void AddToMergedDictionaries()
        {
            Application.Current.Resources.MergedDictionaries.Add(this);
        }
    }
}
