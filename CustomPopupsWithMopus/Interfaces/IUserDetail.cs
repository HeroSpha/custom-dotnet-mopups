using System.Collections.ObjectModel;
using System.Windows.Input;
using CustomPopupsWithMopus.Models;

namespace CustomPopupsWithMopus.Interfaces;

public interface IUserDetail
{
    ICommand SubmitCommand { get; set; }
    ObservableCollection<UserDetail> UserDetails {get; set; }
}