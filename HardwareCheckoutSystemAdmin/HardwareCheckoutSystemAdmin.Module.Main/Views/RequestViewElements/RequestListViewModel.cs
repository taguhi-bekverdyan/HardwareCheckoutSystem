﻿using HardwareCheckoutSystemAdmin.Data.Infrastructure;
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

namespace HardwareCheckoutSystemAdmin.Module.Main.Views.RequestViewElements
{
    class RequestListViewModel:BindableBase, INavigationAware
    {
        private IRequestService _requestService;


        private List<Request> _requests = new List<Request>();
        public List<Request> Requests
        {
            get { return _requests; }
            set { SetProperty(ref _requests, value); }
        }

        private Request _selectedRequest;
        public Request SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                SetProperty(ref _selectedRequest, value);
                DeleteRequest.RaiseCanExecuteChanged();
            }
        }


        public RequestListViewModel(IRequestService service)
        {
            _requestService = service;
            


            DeleteRequest = new DelegateCommand(DeleteRequestAction, CanUpdateOrDelete);
            AddRequest = new DelegateCommand(AddRequestAction);
            UpdateRequest = new DelegateCommand(UpdateRequestAction, CanUpdateOrDelete);
        }


        public DelegateCommand AddRequest { get; private set; }

        public void AddRequestAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand UpdateRequest { get; private set; }

        public void UpdateRequestAction()
        {
            MessageBox.Show("TODO");
        }

        public DelegateCommand DeleteRequest { get; private set; }

        public void DeleteRequestAction()
        {
            MessageBox.Show("TODO");
        }

        public bool CanUpdateOrDelete()
        {
            if (SelectedRequest == null)
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
            Requests = await _requestService.FindAll();
        }

    }
}
