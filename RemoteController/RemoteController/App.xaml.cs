using Autofac;
using RemoteController.ViewModel.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RemoteController
{
    public partial class App : Application
    {
        private ILifetimeScope Scope { get; set; }

        public App(ILifetimeScope scope)
        {
            // Set up autofac DI
            Scope = scope;
            DependencyResolver.ResolveUsing(type => scope.IsRegistered(type) ? scope.Resolve(type) : null);
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
