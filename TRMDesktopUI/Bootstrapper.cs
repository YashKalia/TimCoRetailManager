using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    public class Bootstrapper: BootstrapperBase
    {
        //Part of caliburn micro
        //Handles instantiation of most the classes whose dependencies are injected.
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper() {
            Initialize();
        }

        //Whenever you ask for a simple container instance it will return this instance to you.
        //Reason for this because you may want to get this container to manipulate something 
        //or get information out of it besides the constrcutor.
        protected override void Configure()
        {
            _container.Instance(_container);
            //Bringing windows in and out
            _container
                .Singleton<IWindowManager, WindowManager>() 
                //To pass event messaging around the application.
                //Ties whole application together. Since we are connecting events to each other so it makes sense to have a singleton.
                //Not good to have everything a singleton its not good for memory.
                .Singleton<IEventAggregator, EventAggregator>();

            //Use reflection gettype for our current instance and the current assembly
            //thats running and get all the types of the application
            //From there filter out all the class types and get all the classes that end with ViewModel
            //Take that list of classes and register them with the container.
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }



        protected override void OnStartup(object sender, StartupEventArgs e) {

            DisplayRootViewForAsync<ShellViewModel>();
        }

        //You pass this a type and name we get an instance of that type from the container.
        // This is done by caliburn micro.
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        //Get all instance of that type from the container.
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        //Constructs things.
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }



    }
}
