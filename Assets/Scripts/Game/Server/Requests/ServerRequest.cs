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

        private string _requestURL;
        public ServerRequest(string requestURL, Action<DownloadHandler> callback)
        {
            _requestURL = requestURL;
            _callback = callback;
        }
        
        public IEnumerator SendRequestAsync(CancellationToken cancellationToken)
        {
            using (_activeRequest = new UnityWebRequest(_requestURL))
            {
                _activeRequest.downloadHandler = new DownloadHandlerBuffer();

                yield return WaitUntilComplete(_activeRequest, cancellationToken);

                if (_activeRequest.isNetworkError || _activeRequest.isHttpError)
                    Debug.LogError(_activeRequest.error);
                else
                    OnComplete(_activeRequest.downloadHandler);

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