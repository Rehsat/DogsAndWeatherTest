using System;
using System.Collections;
using System.Threading;
using Game.Server.Parsers.Weather;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Server.Requests
{
    public abstract class RequestSender : IDisposable
    {
        protected string RequestURL { get; }
        private UnityWebRequest _activeRequest;

        public RequestSender(string requestURL)
        {
            RequestURL = requestURL;
        }
        public IEnumerator SendRequestAsync(CancellationToken cancellationToken)
        {
            using (_activeRequest = new UnityWebRequest(RequestURL))
            {
                _activeRequest.downloadHandler = new DownloadHandlerBuffer();

                yield return WaitUntilComplete(_activeRequest, cancellationToken);

                if (_activeRequest.isNetworkError || _activeRequest.isHttpError)
                    Debug.LogError(_activeRequest.error);
                else
                    OnComplete(_activeRequest.downloadHandler.text);

                Clear();
            }
        }
        private IEnumerator WaitUntilComplete(UnityWebRequest request, CancellationToken cancellationToken)
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    CancelRequest();
                    yield break;
                }

                yield return null;
            }
        }

        public void CancelRequest()
        {
            if (_activeRequest != null)
            {
                _activeRequest.Abort();
                Clear();
            }
        }

        private void Clear()
        {
            _activeRequest?.Dispose();
            _activeRequest = null;
        }

        protected abstract void OnComplete(string serverCallback);

        public void Dispose()
        {
            CancelRequest();
        }
    }
}