using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace CollectionSample
{
    public class BaseNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class MainPageViewModel : BaseNotify
    {
		public MainPageViewModel()
		{
			GetTabs();
		}

		private bool _isParentTabVisible = true;
		private bool _isChildTabVisible;
        private bool _isContactsTabVisible;

        private ObservableCollection<TabViewModel> _tabs { get; set; }
		private TabViewModel _selectedTab { get; set; }

		

        public bool IsParentTabVisible
        {
            get => _isParentTabVisible;
            set { _isParentTabVisible = value; OnPropertyChanged(nameof(IsParentTabVisible)); }
        }

        public bool IsChildTabVisible
        {
            get => _isChildTabVisible;
            set { _isChildTabVisible = value; OnPropertyChanged(nameof(IsChildTabVisible)); }
        }

        public bool IsContactsTabVisible
        {
            get => _isContactsTabVisible;
            set { _isContactsTabVisible = value; OnPropertyChanged(nameof(IsContactsTabVisible)); }
        }

        public ObservableCollection<TabViewModel> Tabs
        {
            get => _tabs;
            set { _tabs = value; OnPropertyChanged(nameof(Tabs)); }
        }

        public TabViewModel SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
                //SetSelection();
            }
        }

        private void GetTabs()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            Tabs.Add(new TabViewModel { TabId = 1, IsSelected = true, TabTitle = "Parent record" });
            Tabs.Add(new TabViewModel { TabId = 2, TabTitle = "Child" });
            Tabs.Add(new TabViewModel { TabId = 3, TabTitle = "Contacts" });
            Tabs.Add(new TabViewModel { TabId = 4, TabTitle = "Previous inspections" });
            Tabs.Add(new TabViewModel { TabId = 5, TabTitle = "Attachments" });

            SelectedTab = Tabs.FirstOrDefault();
        }

        public ICommand TabChangedCommand { get { return new Command<TabViewModel>(ChangeTabClick); } }
        public void ChangeTabClick(TabViewModel tab)
        {
            Tabs.All((arg) =>
            {
                if (arg.TabId == tab.TabId)
                {
                    arg.IsSelected = true;
                }
                else
                {
                    arg.IsSelected = false;
                }
                return true;
            });

            SelectedTab = Tabs.Where(t => t.IsSelected == true).FirstOrDefault();

            switch (tab.TabId)
            {
                case 1:
                    IsParentTabVisible = true;
                    IsChildTabVisible = false;
                    IsContactsTabVisible = false;
                    break;
                case 2:
                    IsParentTabVisible = false;
                    IsChildTabVisible = true;
                    IsContactsTabVisible = false;
                    break;
                case 3:
                    IsParentTabVisible = false;
                    IsChildTabVisible = false;
                    IsContactsTabVisible = true;
                    break;
            }
        }
        
    }
}

