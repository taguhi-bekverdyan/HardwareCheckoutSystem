using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Models;
using HardwareCheckoutSystemAdmin.Views;
using Prism.Commands;
using Prism.Events;
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

        #region Services

        private IShellService _shellService;
        private IRequestService _requestService;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Binding Properties

        private List<RequestViewModel> _requests = new List<RequestViewModel>();
        public List<RequestViewModel> Requests
        {
            get { return _requests; }
            set { SetProperty(ref _requests, value); }
        }

        private RequestViewModel _selectedRequest;
        public RequestViewModel SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                SetProperty(ref _selectedRequest, value);
                SendResponse.RaiseCanExecuteChanged();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy,value);
            }
        }

        private ShellView _sendResponseView;

        #endregion

        #region Ctor

        public RequestListViewModel(IRequestService service,IShellService shellService,
            IEventAggregator eventAggregator)
        {
            _requestService = service;
            _shellService = shellService;
            _eventAggregator = eventAggregator;

            IsBusy = true;
            _sendResponseView = null;
            SendResponse = new DelegateCommand(SendResponseAction, CanSendResponse);
        }

        #endregion

        #region Commands

        public DelegateCommand SendResponse { get; private set; }

        public void SendResponseAction()
        {
            NavigationParameters param;
            param = new NavigationParameters { { "request", SelectedRequest.GetId() } };
            _eventAggregator.GetEvent<SendResponseEvent>().Subscribe(SendResponseHandler, ThreadOption.UIThread);

            _sendResponseView = _shellService.ShowShell(nameof(SendResponseView),450,400,param);
        }

        private async void SendResponseHandler(SendResponseEventArgs obj)
        {
            _sendResponseView.Close();
            _sendResponseView = null;
            
                await UpdateData();
              
            _eventAggregator.GetEvent<SendResponseEvent>().Unsubscribe(SendResponseHandler);
        }

        public bool CanSendResponse()
        {
            if (SelectedRequest == null)
            {
                return false;
            }
            return true;

        }

        #endregion

        #region Navigation
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsBusy = true;
            
                await UpdateData();
            
            IsBusy = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Helpers
        private async Task UpdateData()
        {
            try
            {
                List<RequestViewModel> temp = new List<RequestViewModel>();
                var requests = await _requestService.FindRequestsInPending();
                foreach (var item in requests)
                {
                    temp.Add(new RequestViewModel(item));
                }
                Requests = temp;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        #endregion

    }
}
