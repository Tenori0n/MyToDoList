using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MyToDoList
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            
            InitializeComponent();
            BindingContext = new MyToDoListModelView();
        }
    }

    public class MyToDoListModelView : INotifyPropertyChanged
    {
        private string? entryText;

        public MyToDoListModelView()
        {
            Add = new Command<string>(x => { ToDoItemList.Add(new ToDoItem(x)); EntryText = ""; }, x => string.IsNullOrWhiteSpace(x) == false);
            Delete = new Command<ToDoItem>(x => {  ToDoItemList.Remove(x); }); 
        }
        public ICommand Add { get; }
        public ICommand Delete { get; }
        public ObservableCollection<ToDoItem> ToDoItemList { get; set; } = new ObservableCollection<ToDoItem>();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string? EntryText { get => entryText; set { entryText = value; OnPropertyChanged(); } }
    }
    public class ToDoItem : BindableObject
    {
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public ToDoItem(string name, bool done = false) { Name = name; IsDone = done; }
    }
}
