using System;
using System.Collections.Generic;
using System.Text;
using A.Views;
using AvaloniaClientMetal.Models;
using MessageBox.Avalonia.Enums;

namespace A.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadingApplication();
            
        }

        private static async void LoadingApplication()
        {
            try
            {
                PreparedLocalStorage.LoadLocalStorage();
                TokenPair tokenPair = PreparedLocalStorage.GetTokenPairFromLocalStorage();
                tokenPair = await UserImplementation.RefreshTokenPair(tokenPair.RefreshToken);
                PreparedLocalStorage.PutTokenPairFromLocalStorage(tokenPair);
                KeepRoleId.RoleId = tokenPair.IdRole;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                var messageBox = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Успех",
                    ex.Message+"\t", ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Success);
                await messageBox.Show();
            }
        }
    }
}