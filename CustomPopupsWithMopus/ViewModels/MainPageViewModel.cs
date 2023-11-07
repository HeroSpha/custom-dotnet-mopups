using System.Collections.ObjectModel;
using System.Windows.Input;
using CustomPopupsWithMopus.Models;

namespace CustomPopupsWithMopus.ViewModels;

public class MainPageViewModel
{
    public ICommand OpenPopupCommand { get; set; }

    public MainPageViewModel()
    {
        OpenPopupCommand = new Command(OpenPopup);
    }

    private async 
        void OpenPopup(object obj)
    {
        var users = new List<UserDetail>
        {
           new UserDetail{Name = "Siphamandla", Email = "hero@gmail.com", Surname = "Ngwenya", Phone = "123"},
           new UserDetail{Name = "Phindile", Email = "phindile@gmail.com", Surname = "Nkhutha", Phone = "123"},
           new UserDetail{Name = "Luba", Email = "john@gmail.com", Surname = "Ngwenya", Phone = "123"},
           new UserDetail{Name = "Sphesihle", Email = "john@gmail.com", Surname = "Ngwenya", Phone = "123"},
        };
       var selectedUser =  await UserDetailPopupViewModel.AutoGenerateBasicPopup(new ObservableCollection<UserDetail>(users));
       if (selectedUser is not null)
       {
           
       }
    }
}