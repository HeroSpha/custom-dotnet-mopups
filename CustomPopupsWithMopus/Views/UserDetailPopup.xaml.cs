using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPopupsWithMopus.ViewModels;
using Mopups.Pages;
using Mopups.PreBaked.Interfaces;

namespace CustomPopupsWithMopus.Views;

public partial class UserDetailPopup : PopupPage, IGenericViewModel<UserDetailPopupViewModel>
{
    public UserDetailPopup()
    {
        InitializeComponent();
    }

    public UserDetailPopupViewModel ViewModel
    {
        get => BindingContext as UserDetailPopupViewModel;
        set => BindingContext = value;
    }
    public void SetViewModel(UserDetailPopupViewModel viewModel) => ViewModel = viewModel;

    public UserDetailPopupViewModel GetViewModel() => ViewModel;
    
    protected override bool OnBackButtonPressed()
    {
        ViewModel.SafeCloseModal<UserDetailPopup>();
        return base.OnBackButtonPressed();
    }

    protected override bool OnBackgroundClicked()
    {
        ViewModel.SafeCloseModal<UserDetailPopup>();
        return base.OnBackgroundClicked();
    }
}