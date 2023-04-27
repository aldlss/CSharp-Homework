using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using AyaTools;
using HtmlAgilityPack;

namespace homework7
{
    internal class ViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AsyncDelegationButton SearchButton { get; }

        public string Res1
        {
            get => _res1;
            set
            {
                _res1 = value;
                OnPropertyChanged();
            }
        }

        public string Res2
        {
            get => _res2;
            set
            {
                _res2 = value;
                OnPropertyChanged();
            }
        }

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged();
            }
        }

        private string _res1 = string.Empty, _res2 = string.Empty, _input = string.Empty;

        public ViewModel()
        {
            var search = async (object? _) =>
            {
                HttpClient sogouReq = new(), bingReq = new();
                string wd = Uri.EscapeDataString(Input);
                sogouReq.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/113.0");
                sogouReq.DefaultRequestHeaders.Add("Cookie", Environment.GetEnvironmentVariable("SOGOU_COOKIE"));
                bingReq.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/113.0");
                bingReq.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY"));

                var sogouRes = await sogouReq.GetAsync($"https://www.sogou.com/web?query={wd}");
                var bingRes =
                    await bingReq.GetAsync($"https://api.bing.microsoft.com/v7.0/search?q={wd}&count=1&mkt=zh-CN&responseFilter=Webpages");

                if (!sogouRes.IsSuccessStatusCode)
                    Res1 = "搜狗搜索失败";
                else
                {
                    await sogouRes.Content.LoadIntoBufferAsync(200);
                    Res1 = await sogouRes.Content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(Res1);
                    var node = doc.DocumentNode.SelectSingleNode("//div[@class='vrwrap']/div[1]");
                    if (node == null)
                    {
                        Res1 = "搜狗搜索失败";
                        return;
                    }
                    var headNode = node.SelectSingleNode("//h3/a");
                    var headStr = headNode.InnerText;
                    var bodyNode = node.SelectSingleNode("//div[@class='fz-mid space-txt']");
                    var bodyStr = bodyNode.InnerText;
                    Res1 = $"{headStr}{bodyStr}";
                }

                if (!bingRes.IsSuccessStatusCode)
                {
                    Res2 = "必应搜索失败" + $"\n{bingRes}";
                }
                else
                { 
                    // await bingRes.Content.LoadIntoBufferAsync(200);
                    var json2 = await bingRes.Content.ReadAsStringAsync();
                    using JsonDocument document = JsonDocument.Parse(json2);
                    JsonElement rootElement = document.RootElement;
                    if (rootElement.GetProperty("_type").ToString() != "SearchResponse")
                    {
                        Res2 = "必应搜索失败" + $"\n{rootElement.GetProperty("errors")}";
                        return;
                    }

                    var webpage = rootElement.GetProperty("webPages").GetProperty("value").EnumerateArray().First();
                    Res2 = $"{webpage.GetProperty("name")}\n{webpage.GetProperty("snippet")}";
                }
                
            };
            SearchButton = new AsyncDelegationButton(search, TimeSpan.FromSeconds(10));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
