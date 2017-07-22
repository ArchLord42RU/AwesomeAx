using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Awesomium.Core;
using System.Threading;

namespace WebKitActiveX
{
    [ProgId("WebKitAx.Awesomium")]
    [ClassInterface(ClassInterfaceType.AutoDispatch), ComSourceInterfaces(typeof(UserControlEvents))]
    [Guid("01B02030-D7CB-405B-B579-932A442D53B3")]
    [ComVisible(true)]
    public partial class AwesomiumControl : UserControl, IDisposable//, IAwesomiumControl
    {
        private Uri ABOUT_BLANK = new Uri("about:blank");
        SynchronizationContext sc;

        public string TargetUrl
        {
            get { return AwesomiumWebControl.TargetURL.ToString(); }
        }

        public string CurrentUrl
        {
            get { return AwesomiumWebControl.Source.ToString(); }
        }

        public string Html
        {
            get { return AwesomiumWebControl.HTML; }
        }

        public bool IsLoading
        {
            get { return AwesomiumWebControl.IsLoading; }
        }

        public AwesomiumControl()
        {
            InitializeComponent();

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());

            sc = SynchronizationContext.Current;

            AwesomiumWebControl.DocumentReady += AwesomiumWebControl_DocumentReady;
            AwesomiumWebControl.TargetURLChanged += AwesomiumWebControl_TargetURLChanged;
            AwesomiumWebControl.AddressChanged += AwesomiumWebControl_AddressChanged;
            AwesomiumWebControl.ConsoleMessage += AwesomiumWebControl_ConsoleMessage;
            AwesomiumWebControl.MouseClick += AwesomiumWebControl_MouseClick;
            AwesomiumWebControl.ContextMenu = new ContextMenu();
        }

       
        #region public events

        public delegate void DocumentReadyHandler();

        public event DocumentReadyHandler DocumentReady;

        public delegate void ConsoleMessageHandler(string message);

        public event ConsoleMessageHandler ConsoleMessage;

        public delegate void TargetURLChangeHandler(string url);

        public event TargetURLChangeHandler TargetUrlChanged;

        public delegate void AddressChangedHandler(string address);

        public event AddressChangedHandler AddressChanged;

        #endregion

        #region private events

        [ComVisible(false)]
        private void AwesomiumWebControl_ConsoleMessage(object sender, Awesomium.Core.ConsoleMessageEventArgs e)
        {
            try
            {
                ConsoleMessage?.Invoke(e.Message);
            }
            catch (Exception)
            {
                //В некоторых случаях, 1Ска подписывается на событие, хотя явного вызова ДобавитьОбработчик нет,
                //при срабатывании ком обьект просто падает, с ошибкой 0x80020003
            }

        }

        [ComVisible(false)]
        private void AwesomiumWebControl_TargetURLChanged(object sender, Awesomium.Core.UrlEventArgs e)
        {
            try
            {
                TargetUrlChanged?.Invoke(e.OriginalString);
            }
            catch (Exception)
            {
                //В некоторых случаях, 1Ска подписывается на событие, хотя явного вызова ДобавитьОбработчик нет,
                //при срабатывании ком обьект просто падает, с ошибкой 0x80020003  
            }
        }

        [ComVisible(false)]
        private void AwesomiumWebControl_DocumentReady(object sender, Awesomium.Core.DocumentReadyEventArgs e)
        {
            try
            {
                if (e.ReadyState == DocumentReadyState.Ready)
                    DocumentReady?.Invoke();
            }
            catch (Exception)
            {
                //В некоторых случаях, 1Ска подписывается на событие, хотя явного вызова ДобавитьОбработчик нет,
                //при срабатывании ком обьект просто падает, с ошибкой 0x80020003
            }
        }

        [ComVisible(false)]
        private void AwesomiumWebControl_AddressChanged(object sender, UrlEventArgs e)
        {
            try
            {
                AddressChanged?.Invoke(e.OriginalString);
            }
            catch (Exception)
            {
                //В некоторых случаях, 1Ска подписывается на событие, хотя явного вызова ДобавитьОбработчик нет,
                //при срабатывании ком обьект просто падает, с ошибкой 0x80020003
            }
        }

        [ComVisible(false)]
        private void AwesomiumWebControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (AwesomiumWebControl.HasTargetURL)
                    AwesomiumWebControl.Source = AwesomiumWebControl.TargetURL;
        }

        #endregion

        #region public methods

        public void OpenDevTool()
        {
            
        }

        public string ExecuteJS(string script, bool withResult = false)
        {
            checkLive();

            if (withResult)
                return AwesomiumWebControl.ExecuteJavascriptWithResult(script);

            AwesomiumWebControl.ExecuteJavascript(script);
            return null;
        }

        public bool CanGoBack()
        {
            checkLive();

            return AwesomiumWebControl.CanGoBack();
        }

        public void GoBack()
        {
            checkLive();

            AwesomiumWebControl.GoBack();
        }

        public bool CanGoForward()
        {
            checkLive();

            return AwesomiumWebControl.CanGoForward();
        }

        public void GoForward()
        {
            checkLive();

            AwesomiumWebControl.GoForward();
        }

        public bool Reload(bool ignoreCache = false)
        {
            checkLive();

            return AwesomiumWebControl.Reload(ignoreCache);
        }

        public void Navigate(string url)
        {
            checkLive();

            Uri uri;

            bool validUrl = false;

            if (!url.ToLower().StartsWith("http") && url.IndexOf(".") > 0)
                url = "http://" + url;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                string scheme = uri.Scheme;
                validUrl = scheme == "http" || scheme == "https";
            }

            if (validUrl)
                AwesomiumWebControl.Source = uri;
            else
                AwesomiumWebControl.Source = new Uri($"https://www.google.ru/#newwindow=1&q={url}");

        }
        
        public void LoadHtml(string html)
        {
            checkLive();

            AwesomiumWebControl.LoadHTML(html);
        }

        public new void Dispose()
        {
            AwesomiumWebControl?.Dispose();
            WebCore.Shutdown();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(this);
        }

        #endregion

        private void checkLive()
        {
            if (!AwesomiumWebControl.IsLive)
                throw new COMException("Component is not init");
        }
    }

    [Guid("0A415E38-372F-45fb-813B-D9558C787EA0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface UserControlEvents
    {
        [DispId(0x60020001)]
        void TargetURLChanged(string targetUrl);

        [DispId(0x60020002)]
        void DocumentReady();

        [DispId(0x60020003)]
        void ConsoleMessage(string message);

        [DispId(0x60020004)]
        void AddressChanged(string address);
    }
}
