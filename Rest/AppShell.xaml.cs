using Rest.Pages;

namespace Rest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("addpart", typeof(AddPartPage));
        }
    }
}