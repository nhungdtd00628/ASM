using ASM.Entity;
using ASM.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

namespace ASM.Signin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn : Page
    {
        private string currentUploadUrl;
        private LogInMember currentLogInMember;
        private Token currentToken;
        private string contents;
        public SignIn()
        {
            this.currentLogInMember = new LogInMember();
            this.currentToken = new Token();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        
        private async void Click_Login(object sender, RoutedEventArgs e)
        {
            this.currentLogInMember.email = this.Mail.Text;
            this.currentLogInMember.password = this.Pass.Password;

            string jsonMember = JsonConvert.SerializeObject(this.currentLogInMember);
            Debug.WriteLine(jsonMember);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(Service.ServiceUrl.MEMBER_LOGIN, content);
            var contents = await response.Result.Content.ReadAsStringAsync();
            Token tokenResponse = JsonConvert.DeserializeObject<Token>(contents);
            string jsonUser = JsonConvert.SerializeObject(tokenResponse);
            Debug.WriteLine("" + contents);
            currentToken = JsonConvert.DeserializeObject<Token>(contents);
            Debug.WriteLine(currentToken.token);
            this.Mail.Text = string.Empty;
            this.Pass.Password = string.Empty;
            if (response.Result.StatusCode == HttpStatusCode.Created)

            {
                Debug.WriteLine("Dang nhap thanh cong");
                CreatedFileToken();
                this.Frame.Navigate(typeof(Signin.Information));

            }
            else
            {
                Errorcode errorcode = JsonConvert.DeserializeObject<Errorcode>(contents);
                this.errorkey.Text = errorcode.message;

                if (errorcode.error.Count > 0)
                {
                    foreach (var key in errorcode.error.Keys)
                    {
                        var objectBykey = this.FindName(key);
                        var value = errorcode.error[key];
                        if (objectBykey != null)
                        {
                            TextBlock textblock = objectBykey as TextBlock;
                            textblock.Text = "* " + value;
                            textblock.Visibility = Visibility.Visible;
                        }
                    }
                }

            }
        }
        private async void CreatedFileToken()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("Token.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            sampleFile = await storageFolder.GetFileAsync("Token.txt");
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, currentToken.token);
        }
    }
}


