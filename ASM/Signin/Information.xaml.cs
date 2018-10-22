using ASM.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.Signin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Information : Page
    {
        private Token currenttoken;
        public Information()
        {
            this.currenttoken = new Token();
            this.InitializeComponent();
            LoadInformation();
        }
        private async void LoadInformation()
        {
            this.UserNameInfor.Text = "User Name";

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("Token.txt");
            currenttoken.token = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);


            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + currenttoken.token);
            Debug.WriteLine(currenttoken.token);
            string reponese = await httpClient.GetStringAsync(Service.ServiceUrl.MEMBER_INFORMATION);

            Member member = JsonConvert.DeserializeObject<Member>(reponese);
            Uri url = new Uri(member.avatar, UriKind.Absolute);

            BitmapImage bmImage = new BitmapImage(url);
            this.AvatarInfor.Source = bmImage;
            Debug.WriteLine(reponese);

            //this.UserNameInfor.Text = "User Name";
            this.userNameInfor.Text = ": " + member.firstName + member.lastName;
            this.EmailInfor.Text = "Email";
            this.emailInfor.Text = ": " + member.email;
            this.PasswordInfor.Text = "Password";
            this.passwordInfor.Text = ": " + member.password;
            this.PhoneInfor.Text = "Phone";
            this.GenderInfor.Text = "Gender";
            this.phoneInfor.Text = ": " + member.phone;
            this.DateInfor.Text = "Birthday";
            this.dateInfor.Text = ": " + member.birthday;
            this.AddressInfor.Text = "Address";
            this.addressInfor.Text = ": " + member.address;
            this.IntroductionInfor.Text = "Introduction";
            this.introductionInfor.Text = ": " + member.introduction;
           
            switch (member.gender.ToString())
            {
                case "0":
                    this.genderInfor.Text = ": Other";
                    break;
                case "1":
                    this.genderInfor.Text = ": Male";
                    break;
                case "2":
                    this.genderInfor.Text = ": Female";
                    break;
                default:
                    break;
            }

        }

    }
}

