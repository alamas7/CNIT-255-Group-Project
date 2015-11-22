using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TIMER
{
    /// <summary>
    /// Interaction logic for TimerItem.xaml
    /// </summary>
    public partial class TimerItem : Window
    {
        BindingList<Item> m_timerItems = new BindingList<Item>();

        public TimerItem(BindingList<Item> a_timerItems)
        {
            InitializeComponent();
            lblItem.Text = "Item: ";
            txtItem.Focus();
            lblTime.Text = "Time: ";
            cbTimeType.Items.Add("Secs");
            cbTimeType.Items.Add("Mins");
            cbTimeType.Items.Add("Hrs");
            this.m_timerItems = a_timerItems;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Item item = Item.CreateNewItem();
            item.Food = txtItem.Text;
            item.Time = txtTime.Text;
            item.TimeValue = cbTimeType.Text;
            m_timerItems.Add(item);                     
            this.Close();
            this.UpdateLayout();
        }
    }

    public class Item : INotifyPropertyChanged
    {
        Guid idValue = Guid.NewGuid();
        string time = string.Empty;
        string timeType = String.Empty;
        string item = String.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Item()
        {
            time = string.Empty;
            timeType = string.Empty;
            item = string.Empty;
        }

        public static Item CreateNewItem()
        {
            return new Item();
        }

        public Guid ID
        {
            get
            {
                return this.idValue;
            }
        }

        public string Food
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                NotifyPropertyChanged();
            }
        }

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyPropertyChanged();
            }
        }

        public string TimeValue
        {
            get
            {
                return timeType;
            }
            set
            {
                timeType = value;
                NotifyPropertyChanged();
            }
        }
    }
}
