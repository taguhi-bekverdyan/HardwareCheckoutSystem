using HardwareCheckoutSystemAdmin.Common.Prism;
using HardwareCheckoutSystemAdmin.Data.Infrastructure;
using HardwareCheckoutSystemAdmin.Data.Services;
using HardwareCheckoutSystemAdmin.Models;
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
    class SendResponseViewModel :BindableBase, INavigationAware
    {
        #region services
        private readonly IEventAggregator _eventAggregator;
        private readonly IShellService _shellService;
        private readonly IResponseService _responseService;
        private readonly IRequestService _requestService;
        #endregion

        #region Binding Properties

        private string _senderId;
        public string SenderId
        {
            get { return _senderId; }
            set
            {
                SetProperty(ref _senderId , value);
                Send.RaiseCanExecuteChanged();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
                Send.RaiseCanExecuteChanged();
            }
        }

        private RequestStatus _newStatus;
        public RequestStatus NewStatus
        {
            get { return _newStatus; }
            set
            {
                SetProperty(ref _newStatus ,value);
                Send.RaiseCanExecuteChanged();
            }
        }

        private Guid _requestId;
        

        #endregion

        #region ctor

        public SendResponseViewModel(IEventAggregator eventAgregator, IShellService shellService,
            IResponseService responseService,IRequestService requestService)
        {
            _eventAggregator = eventAgregator;
            _shellService = shellService;
            _responseService = responseService;
            _requestService = requestService;

            Cancel = new DelegateCommand(CancelAction);
            Send = new DelegateCommand(SendAction,CanSendResponse);
        }

        #endregion

        #region Commands

        public DelegateCommand Cancel { get; private set; }
        private void CancelAction()
        {
            _eventAggregator.GetEvent<SendResponseEvent>().Publish(new SendResponseEventArgs { Response = null });
        }

        public DelegateCommand Send { get; private set; }
        private async void SendAction()
        {
            try
            {
                Response response = new Response();
                response.Date = DateTime.Now;
                response.Message = Message;
                response.NewStatus = NewStatus;
                response.RequestId = _requestId;
                response.UserId = Guid.Parse(SenderId);

                Request request = await _requestService.FindRequestById(_requestId);
                request.LastResponseId = response.Id;
                request.Status = response.NewStatus;
                await _requestService.Update(request);

                await _responseService.Insert(response);
            }
            catch(Exception e)
            {
                MessageBox(e.ToString());
            }
            finally
            {
                _eventAggregator.GetEvent<SendResponseEvent>().Publish(new SendResponseEventArgs { Response = null });
            }           
        }

        private void MessageBox(string v)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _requestId = (Guid)navigationContext.Parameters["request"];
        }

        #endregion

        #region Helpers

        private bool CanSendResponse()
        {
            Guid id;
            if (Guid.TryParse(SenderId,out id) && !string.IsNullOrEmpty(Message))
            {
                return true;
            }
            return false;
        }

        #endregion

    }

    public class SendResponseEvent : PubSubEvent<SendResponseEventArgs> { }

    public class SendResponseEventArgs
    {
        public Response Response { get; set; }
    }
}
