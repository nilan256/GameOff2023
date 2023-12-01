using System.Collections;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Containers;

namespace Game.UI
{

    public class UIViewRequest
    {

        private readonly string viewCategory;
        private readonly string viewName;
        private readonly string responseCategory;
        private readonly string responseName;
        private readonly SignalReceiver receiver;
        private SignalStream stream;
        private Signal response;

        public bool IsStarted { get; private set; }
        public bool ResponseReceived { get; private set; }
        public object ResponseValue => response.valueAsObject;
        public int ResponseInt => CastOrDefault<int>(ResponseValue);
        public string ResponseString => CastOrDefault<string>(ResponseValue);
        public bool ResponseBool => CastOrDefault<bool>(ResponseValue);
        public float ResponseFloat => CastOrDefault<float>(ResponseValue);

        public UIViewRequest(string viewCategory, string viewName, string responseCategory, string responseName)
        {
            this.viewCategory = viewCategory;
            this.viewName = viewName;
            this.responseCategory = responseCategory;
            this.responseName = responseName;
            receiver = new SignalReceiver().SetOnSignalCallback(OnSignalCallback);
        }

        public UIViewRequest(UIViewId viewId, StreamId responseId) 
            : this(viewId.Category, viewId.Name, responseId.Category, responseId.Name) 
        { }

        public UIViewRequest(string viewName, string responseName)
        : this(UIConstants.DefaultCategory, viewName, UIConstants.DefaultCategory, responseName)
        { }

        private void OnSignalCallback(Signal signal)
        {
            IsStarted = false;
            ResponseReceived = true;
            response = signal;
            stream.Close();
            stream = null;
        }

        public void Start()
        {
            if (IsStarted) return;
            IsStarted = true;

            ResponseReceived = false;
            stream = SignalStream.Get(responseCategory, responseName);
            stream.ConnectReceiver(receiver);
            UIView.Show(viewCategory, viewName);
        }

        public IEnumerator StartCoroutine()
        {
            if (!IsStarted)
            {
                Start();
            }

            while (!ResponseReceived)
            {
                yield return null;
            }
        }

        private T CastOrDefault<T>(object obj)
        {
            if (obj is T value) return value;
            return default;
        }

    }

}