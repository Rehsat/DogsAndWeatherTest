using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Game.Server.Requests
{
    public class RequestSendHandler
    {
        private CoroutineHandler _coroutineHandler;
        private Queue<RequestSender> _requestsQueue;
        private RequestSender _currentRequest;
        private CancellationTokenSource _currentCancellationTokenSource;
        private Coroutine _currentCoroutine;

        public RequestSendHandler(CoroutineHandler coroutineHandler)
        {
            _coroutineHandler = coroutineHandler;
            _requestsQueue = new Queue<RequestSender>();
        }

        public void AddRequest(RequestSender request)
        {
            _requestsQueue.Enqueue(request);
        
            if (_currentCoroutine == null)
                _currentCoroutine = _coroutineHandler.StartNewCoroutine(ProcessQueue());
        }

        private IEnumerator ProcessQueue()
        {
            while (_requestsQueue.Count > 0)
            {
                _currentRequest = _requestsQueue.Dequeue();
                _currentCancellationTokenSource = new CancellationTokenSource();
            
                yield return _currentRequest.SendRequestAsync(_currentCancellationTokenSource.Token);
                ClearCurrentRequest();
            }
        
            _currentCoroutine = null;
        }

        private void ClearCurrentRequest()
        {
            _currentRequest.Dispose();
            _currentCancellationTokenSource.Dispose();
            _currentRequest = null;
            _currentCancellationTokenSource = null;
        }

        public void CancelCurrentRequest()
        {
            _currentCancellationTokenSource?.Cancel();
            _currentRequest?.CancelRequest();
            _currentRequest?.Dispose();
            _currentRequest = null;
        }

        public void CancelAllRequests()
        {
            CancelCurrentRequest();
            foreach (var request in _requestsQueue)
            {
                request.Dispose();
            }
            _requestsQueue.Clear();
        }

        public void Dispose()
        {
            CancelAllRequests();
            _currentCancellationTokenSource?.Dispose();
        }
    }
}