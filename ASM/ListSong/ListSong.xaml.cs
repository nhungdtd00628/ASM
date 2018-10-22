using ASM.Entity;
using ASM.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.ListSong
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSong : Page
    {
        private static string SONG_API = "https://2-dot-backup-server-002.appspot.com/_api/v2/songs";
        public ObservableCollection<Song> ListSongs { get; set; }
        public ListSong()
        {
            this.ListSongs = new ObservableCollection<Song>();
            this.InitializeComponent();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "nK2K29CsbDl09HzaZR9JmdBTkCogDO5B9XdnjPTBqN0LOc2pTTqzHpaRJwJmVMYY");
            HttpResponseMessage responseMessage = httpClient.GetAsync(SONG_API).Result;
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(content);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                ObservableCollection<Song> songResponse = JsonConvert.DeserializeObject<ObservableCollection<Song>>(content);
                foreach (var song in songResponse)
                {
                    this.ListSongs.Add(song);
                }
                Debug.WriteLine("");
            }
            else
            {
                Errorcode errorResponse = JsonConvert.DeserializeObject<Errorcode>(content);
                foreach (var key in errorResponse.error.Keys)
                {
                    if (this.FindName(key) is TextBlock textBlock)
                    {
                        textBlock.Text = errorResponse.error[key];
                    }
                }
            }
        }
    }
}
