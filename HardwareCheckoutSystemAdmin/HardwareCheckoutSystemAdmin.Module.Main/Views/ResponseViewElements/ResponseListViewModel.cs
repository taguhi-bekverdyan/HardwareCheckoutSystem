using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.ResponseViewElements
{
    class ResponseListViewModel:BindableBase, INavigationAware
    {
        private IResponseService _responseService;


        private List<Response> _responses = new List<Response>();
        public List<Response> Responses
        {
            get { return _responses; }
            set { SetProperty(ref _responses, value); }
        }

        private Response _selectedResponse;
        public Response SelectedResponse
        {
            get { return _selectedResponse; }
            set
            {
                SetProperty(ref _selectedResponse, value);
                DeleteResponse.RaiseCanExecuteChanged();
            }
        }


        public ResponseListViewModel(IResponseService service)
        {
            _responseService = service;

            DeleteResponse = new DelegateCommand(DeleteResponseAction, CanUpdateOrDelete);
            AddResponse = new DelegateCommand(AddResponseAction);
            UpdateResponse = new DelegateCommand(UpdateResponseAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddResponse { get; private set; }

        public void AddResponseAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand UpdateResponse { get; private set; }

        public void UpdateResponseAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteResponse { get; private set; }

        public void DeleteResponseAction()
        {
            MessageBox.Show("TODO");
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedResponse == null)
            {
                return false;
            }
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private async void UpdateData()
        {
            try
            {
                Responses = await _responseService.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

    }
}
