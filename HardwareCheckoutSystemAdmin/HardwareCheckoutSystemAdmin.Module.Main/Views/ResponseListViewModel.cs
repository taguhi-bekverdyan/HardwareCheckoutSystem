using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareCheckoutSystemAdmin.Module.Main.Views
{
    class ResponseListViewModel:BindableBase
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
            Responses = _responseService.FindAll().Result;
            DeleteResponse = new DelegateCommand(DeleteResponseAction, CanUpdateOrDelete);
            AddResponse = new DelegateCommand(AddResponseAction);
            UpdateResponse = new DelegateCommand(UpdateResponseAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddResponse { get; private set; }

        public void AddResponseAction()
        {
            //Category cat = new Category() { Id = Guid.NewGuid(),Name = "Phone"};
            //_categoryService.Insert(cat);
            //Categories = _categoryService.FindAll().Result;
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
            _responseService.Delete(SelectedResponse);
            SelectedResponse = null;
            Responses = _responseService.FindAll().Result;
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedResponse == null)
            {
                return false;
            }
            return true;
        }
    }
}
