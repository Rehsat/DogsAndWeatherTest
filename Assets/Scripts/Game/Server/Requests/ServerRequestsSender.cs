using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Game.Server.Requests
{
    public class ServerRequestsSender
    {
        private CoroutineHandler _coroutineHandler;
        private Queue<ServerRequest> _requestsQueue;
        private ServerRequest _currentServerRequest;
        private CancellationTokenSource _currentCancellationTokenSource;
        private Coroutine _currentCoroutine;

        public ServerRequestsSender(CoroutineHandler coroutineHandler)
        {
            _coroutineHandler = coroutineHandler;
            _requestsQueue = new Queue<ServerRequest>();
        }

        public void AddRequest(ServerRequest serverRequest)
        {
            _requestsQueue.Enqueue(serverRequest);
        
            if (_currentCoroutine == null)
                _currentCoroutine = _coroutineHandler.StartNewCoroutine(ProcessQueue());
        }

        private IEnumerator ProcessQueue()
        {
            while (_requestsQueue.Count > 0)
            {
                _currentServerRequest = _requestsQueue.Dequeue();
                _currentCancellationTokenSource = new CancellationTokenSource();
            
                yield return _currentServerRequest.SendRequestAsync(_currentCancellationTokenSource.Token);
                ClearCurrentRequest();
            }
        
            _currentCoroutine = null;
        }

        private void ClearCurrentRequest()
        {
            _currentServerRequest.Dispose();
            _currentCancellationTokenSource.Dispose();
            _currentServerRequest = null;
            _currentCancellationTokenSource = null;
        }

        public void CancelCurrentRequest()
        {
            _currentCancellationTokenSource?.Cancel();
            _currentServerRequest?.CancelRequest();
            _currentServerRequest?.Dispose();
            _currentServerRequest = null;
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