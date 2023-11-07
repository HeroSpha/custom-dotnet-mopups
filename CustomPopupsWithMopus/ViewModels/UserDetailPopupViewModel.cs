using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using CustomPopupsWithMopus.Interfaces;
using CustomPopupsWithMopus.Models;
using CustomPopupsWithMopus.Views;
using Mopups.Pages;
using Mopups.PreBaked.AbstractClasses;
using Mopups.PreBaked.Interfaces;
using Mopups.PreBaked.Services;

namespace CustomPopupsWithMopus.ViewModels;

public class UserDetailPopupViewModel : PopupViewModel<UserDetail>, IUserDetailPopupViewModel
{

    private ObservableCollection<UserDetail> _userDetails;

    public ObservableCollection<UserDetail> UserDetails
    {
        get => _userDetails;
        set => SetValue(ref _userDetails, value);
    }
    
    private UserDetail _userDetail;

    public UserDetail UserDetail
    {
        get => _userDetail;
        set => SetValue(ref _userDetail, value);
    }
    public UserDetailPopupViewModel(IPreBakedMopupService popupService, int heightRequest, int widthRequest) : base(popupService, heightRequest, widthRequest)
    {
    }

    public UserDetailPopupViewModel(IPreBakedMopupService popupService) : base(popupService)
    {
    }

    private ICommand _submitCommand;
    public ICommand SubmitCommand
    {
        get => _submitCommand;
        set => SetValue(ref _submitCommand, value);
    }
    
    private ICommand _closeCommand;
    public ICommand CloseCommand
    {
        get => _closeCommand;
        set => SetValue(ref _closeCommand, value);
    }

    public async Task<UserDetail> GeneratePopup(Dictionary<string, object> propertyDictionary)
    {
        return await GeneratePopup<UserDetailPopup>(propertyDictionary);
    }

    public async Task<UserDetail> GeneratePopup<TPopupPage>(Dictionary<string, object> optionalProperties) where TPopupPage : PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new()
    {
        try
        {
            InitialiseOptionalProperties(optionalProperties);
            return await PreBakedMopupService.GetInstance()
                .PushAsync<UserDetailPopupViewModel, TPopupPage, UserDetail>(this);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<UserDetail> GeneratePopup<TPopupPage>() where TPopupPage : PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new()
    {
        return await PreBakedMopupService.GetInstance()
            .PushAsync<UserDetailPopupViewModel, TPopupPage, UserDetail>(this);
    }

    public async Task<UserDetail> GeneratePopup()
    {
        return await GeneratePopup<UserDetailPopup>();
    }

    public async Task<UserDetail> GeneratePopup(IUserDetail userDetail)
    {
        return await GeneratePopup<UserDetailPopup>(userDetail);
    }

    public async Task<UserDetail> GeneratePopup<TPopupPage>(IUserDetail propertyInterface) where TPopupPage : PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new()
    {
        PropertyInfo[] properties = typeof(IUserDetail).GetProperties();
        for (int propertyIndex = 0; propertyIndex < properties.Count(); propertyIndex++)
        {
            GetType().GetProperty(properties[propertyIndex].Name).SetValue(this, properties[propertyIndex].GetValue(propertyInterface, null), null);
        }
        return await PreBakedMopupService.GetInstance()
            .PushAsync<UserDetailPopupViewModel, TPopupPage, UserDetail>(this);
    }

    public Dictionary<string, (object propertty, Type propertyType)> PullViewModelProperties()
    {
        return base.PullViewModelProperties<UserDetailPopupViewModel>();
    }
    public static async Task<UserDetail?> AutoGenerateBasicPopup(ObservableCollection<UserDetail> userDetails)
    {
        return await AutoGenerateBasicPopup<UserDetailPopup>(userDetails);
    }
    public static async Task<UserDetail?> AutoGenerateBasicPopup<TPopupPage>(IEnumerable<UserDetail> userDetails) where TPopupPage : Mopups.Pages.PopupPage, IGenericViewModel<UserDetailPopupViewModel>, new()
    {
        var autoGeneratePopupViewModel = new UserDetailPopupViewModel(PreBakedMopupService.GetInstance());
        ICommand submitButtonCommand = new Command(() => autoGeneratePopupViewModel.SafeCloseModal<TPopupPage>(autoGeneratePopupViewModel.UserDetail));
        ICommand closeButtonCommand = new Command(() => autoGeneratePopupViewModel.SafeCloseModal<TPopupPage>(default(UserDetail)));

        var propertyDictionary = new Dictionary<string, object>
        {
            { "SubmitCommand", submitButtonCommand },
            { "CloseCommand", closeButtonCommand },
            { "UserDetails", userDetails },
        };
        
        return await autoGeneratePopupViewModel.GeneratePopup<TPopupPage>(propertyDictionary);
    }
}