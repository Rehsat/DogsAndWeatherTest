using System;
using System.Collections;
using System.Threading;
using Game.Server.Parsers.Weather;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Server.Requests
{
    public class ServerRequest : IDisposable
    {
        private UnityWebRequest _activeRequest;
        
        private readonly Action<DownloadHandler> _callback;

        private string _url;

        public string URL => _url;

        public ServerRequest(string url, Action<DownloadHandler> callback)
        {
            _url = url;
            _callback = callback;
        }
        
        public IEnumerator SendRequestAsync(CancellationToken cancellationToken)
        {
            using (_activeRequest = new UnityWebRequest(_url))
            {
                yield return HandleRequestSend(_activeRequest, cancellationToken);
            }
        }

        private IEnumerator HandleRequestSend(UnityWebRequest request, CancellationToken cancellationToken)
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return WaitUntilComplete(request, cancellationToken);

            try
            {
                if (request.isNetworkError || request.isHttpError)
                    Debug.LogError(_activeRequest.error);
                else
                    OnComplete(request.downloadHandler);
            }
            catch 
            {
                Debug.LogError("request was canceled");
            }

            Clear();
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

        
        private void OnComplete(DownloadHandler serverCallback)
        {
            _callback.Invoke(serverCallback);
        }

        public void Dispose()
        {
            CancelRequest();
        }
    }
}