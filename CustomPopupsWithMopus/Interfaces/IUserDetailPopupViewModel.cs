using CustomPopupsWithMopus.Models;
using CustomPopupsWithMopus.ViewModels;
using Mopups.Pages;
using Mopups.PreBaked.Interfaces;

namespace CustomPopupsWithMopus.Interfaces;

public interface IUserDetailPopupViewModel :  IUserDetail
{
   Task<UserDetail> GeneratePopup(Dictionary<string, object> propertyDictionary);

   Task<UserDetail> GeneratePopup<TPopupPage>(Dictionary<string, object> propertyDictionary)
       where TPopupPage : PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new();

   Task<UserDetail> GeneratePopup<TPopupPage>()
       where TPopupPage : PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new();

   Task<UserDetail> GeneratePopup();

   Task<UserDetail> GeneratePopup(IUserDetail userDetail);

   Task<UserDetail> GeneratePopup<TPopupPage>(IUserDetail user)
       where TPopupPage : PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new();

   Dictionary<string, (object propertty, Type propertyType)> PullViewModelProperties();
}