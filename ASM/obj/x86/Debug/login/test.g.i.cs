#pragma checksum "C:\Users\Administrator\source\repos\ASM\ASM\login\test.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A0C3F239EB872AC596F8DDE252CEE129"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASM.login
{
    partial class test : global::Windows.UI.Xaml.Controls.Page
    {


        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", " 10.0.17.0")]
        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", " 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///login/test.xaml");
            global::Windows.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
        }

        partial void UnloadObject(global::Windows.UI.Xaml.DependencyObject unloadableObject);

        private global::Windows.UI.Xaml.Controls.TextBox txtUser;
        private global::Windows.UI.Xaml.Controls.PasswordBox txtPass;
        private global::Windows.UI.Xaml.Controls.TextBlock errorMessage;
        private global::Windows.UI.Xaml.Controls.CheckBox ckbRemember;
        private global::Windows.UI.Xaml.Controls.Button btnLogin;
    }
}

