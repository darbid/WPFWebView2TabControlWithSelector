using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFWebView2TabControlWithSelector
{
    class TabControlDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WPFTabBrowserDataTemplate { get; set; }
        public DataTemplate NewWPFTTabBrowserDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (((BaseViewModel)(item)).BrowserTabType == BaseViewModel.BrowserTabTypes.MainWPFTabBrowser)
            {
                return WPFTabBrowserDataTemplate;
            }
            return NewWPFTTabBrowserDataTemplate;
        }

    }
}
