using J_LinkMemDump.ViewModel;

namespace J_LinkMemDump.View
{
    /// <inheritdoc cref="idknowXD" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new VarViewModel();
        }
    }
}