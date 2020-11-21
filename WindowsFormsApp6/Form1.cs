using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace WebView2HelloWorld
{
    public partial class Form1 : Form
    {
        //https://docs.microsoft.com/ja-jp/microsoft-edge/webview2/concepts/distribution
        //https://qiita.com/NagaJun/items/4925a63ce7b93b80639e

        public Form1()
        {
            InitializeComponent();
            webView21.NavigationCompleted += WebView21_NavigationCompleted;
        }

        JsToCs CsClass = new JsToCs();

        private void button1_Click(object sender, EventArgs e)
        {
            if (webView21 != null && webView21.CoreWebView2 != null)
            {
                webView21.CoreWebView2.NavigateToString(File.ReadAllText("page\\index.html"));
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await CallJS();
        }

        private void WebView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (webView21?.CoreWebView2 != null)
            {
                webView21.CoreWebView2.AddHostObjectToScript("aaa", CsClass);
            }
        }

        async Task CallJS()
        {
            var str1 = await webView21.ExecuteScriptAsync("func1(\"C#からの呼び出し\")");
            Debug.WriteLine(str1);
        }
    }

    //[ClassInterface(ClassInterfaceType.AutoDual)]
    //[ComVisible(true)]
    public class JsToCs
    {
        public void MessageShow(string strText)
        {
            MessageBox.Show("JSからの呼び出し:" + strText);
        }
    }
}
