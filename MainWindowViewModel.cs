using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFWebView2TabControlWithSelector
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ICommand _TabCloseCommand;

        public ICommand TabCloseCommand { get { return _TabCloseCommand ??= new RelayCommand<BaseViewModel>((ti) => this.TabClosed(ti)); } }

        private ObservableCollection<BaseViewModel> _TabCollection;
        public ObservableCollection<BaseViewModel> TabCollection
        {
            get { return _TabCollection ??= new ObservableCollection<BaseViewModel>(); }

            set
            {
                _TabCollection = value;
                NotifyPropertyChanged();
            }
        }

        private int _BrowserTabControlSelectedIndex = 0;
        public int BrowserTabControlSelectedIndex
        {
            get { return _BrowserTabControlSelectedIndex; }
            set
            {
                _BrowserTabControlSelectedIndex = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            BrowserTabItemViewModel btivm = new();
            btivm.Source = new Uri(@"https://bing.com");
            btivm.BrowserTabType = BaseViewModel.BrowserTabTypes.MainWPFTabBrowser;
            btivm.BrowserTabNewWindowRequestedEvent += Btivm_BrowserTabNewWindowRequestedEvent;
            TabCollection.Add(btivm);

            this.BrowserTabControlSelectedIndex = 0;


            BrowserTabItemViewModel btivm1 = new();
            btivm1.BrowserTabType = BaseViewModel.BrowserTabTypes.OtherBrowser;
            btivm1.Source = new Uri(@"https://microsoft.com");
            btivm1.BrowserTabNewWindowRequestedEvent += Btivm_BrowserTabNewWindowRequestedEvent;
            TabCollection.Add(btivm1);

        }

        private void Btivm_BrowserTabNewWindowRequestedEvent(Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e)
        {
            BrowserTabItemViewModel btivm = new();
            btivm.AddNewTab(e);
            btivm.BrowserTabType = BaseViewModel.BrowserTabTypes.OtherBrowser;
            btivm.BrowserTabNewWindowRequestedEvent += Btivm_BrowserTabNewWindowRequestedEvent;
            TabCollection.Add(btivm);
            BrowserTabControlSelectedIndex = TabCollection.Count - 1;
        }

        public void TabClosed(BaseViewModel tab)
        {
            TabCollection.Remove(tab);
            BrowserTabControlSelectedIndex = TabCollection.Count - 1;
        }

        #region "Notify Property Changed"
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion //"Notify Property Changed"
    }
}
