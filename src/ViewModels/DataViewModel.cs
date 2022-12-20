using DataModels;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DataViewModel:ViewModelBase
    {
        public Action? ReginOk { private get; set; }
        public Action? LoginOk { private get; set; }

        private readonly DataManager model = MainViewModel.Data;
        private readonly ObservableCollection<User> users;
        public ObservableCollection<string> Names { get; }

        #region Commands
        public CommandAsync ReginAsync { get; }
        public CommandAsync RememberAsync { get; }
        public Command Login { get; }
        public Command ReginExit { get; }
        #endregion

        public static Guid SelectedId { get; set; }

        public DataViewModel()
        {
            users = new ObservableCollection<User>(model.UserRep.Users);
            Names = new ObservableCollection<string>(model
                .UserRep
                .Users
                .Include(u => u.Histories)
                .Select(u => new
                {
                    u.Nick,
                    u.Histories
                        .FirstOrDefault()
                        .LastTime
                })
                .OrderByDescending(h => h.LastTime)
                .Select(s => s.Nick));
            Login = new Command(login, canExecuteLogin, MainViewModel.ErrorHundler);
            RememberAsync = new CommandAsync(rememberAsync, () => Login.CanExecute(null), MainViewModel.ErrorHundler);
            ReginAsync = new CommandAsync(reginAsync, canExecuteReginAsync, MainViewModel.ErrorHundler);
            ReginExit = new Command(() => ReginOk?.Invoke(), errorHundler: MainViewModel.ErrorHundler);
            SelectedName = User.GuestNick;
           
        }

        private async Task rememberAsync()
        {
            var suser = await model.UserRep.GetItemByIdAsync(SelectedId);
            if (isRemember)
            {
                await Task.Run(async () =>
                {
                    foreach (var user in model.UserRep.Users)
                    {
                        if (user.Id == SelectedId)
                        {
                            user.RememberMe = true;
                            await model.UserRep.UpdateAsync(user);
                        }
                        else if (user.RememberMe)
                        {
                            user.RememberMe = false;
                            await model.UserRep.UpdateAsync(user);
                        }
                    }
                });
            }
            else
            {
                suser!.RememberMe = false;
                await model.UserRep.UpdateAsync(suser);
            }

        }

        private void login()
        {
            LoginOk?.Invoke();

        }

        private bool canExecuteLogin()
        {
            if (selectedName == User.GuestNick)
            {
                SelectedId = User.GustGuid;
                return true;
            }
            var user = model.UserRep.Users.Include(u => u.Authorization).FirstOrDefault(y => y.Nick == SelectedName)?.Authorization;
            if (user != null) SelectedId = user.UserId;
            return pass != null && user != null && user.Password == Authorization.ToHashString(pass);
        }

        private bool canExecuteReginAsync()
        {
            if (pass == null || selectedName == null || pass != confPass || Pass?.Length < 2 || selectedName?.Length < 2) return false;
            return !users.Any(u => u.Nick.ToLower() == selectedName!.ToLower());
        }

        private async Task reginAsync()
        {
            Guid id = Guid.NewGuid();
            var user = new User()
            {
                Id = id,
                Nick = SelectedName
            };

            user.Authorization = new Authorization()
            {
                UserId = user.Id,
                Password = Authorization.ToHashString(pass!),
                Email = ""
            };

            await model.UserRep.UpdateAsync(user);
            Names.Add(SelectedName);
            Pass = "";
            ReginOk?.Invoke();
        }

        private string selectedName ="";
        public string SelectedName
        {
            get => selectedName;
            set
            {
                selectedName = value.Trim();
                OnPropertyChanged("SelectedName");
                ReginAsync.RaiseCanExecuteChanged();
                Login.RaiseCanExecuteChanged();
                RememberAsync.RaiseCanExecuteChanged();
                var user = model.UserRep.Users.FirstOrDefault(u => u.Nick == selectedName);
                IsRemember = user != null && canExecuteLogin() && user.RememberMe;
            }
        }

        private string? pass;
        public string? Pass
        {
            get => pass;
            set
            {
                pass = value?.Trim();
                OnPropertyChanged("Pass");
                ReginAsync.RaiseCanExecuteChanged();
                Login.RaiseCanExecuteChanged();
                RememberAsync.RaiseCanExecuteChanged();
            }
        }

        private string? confPass;
        public string? ConfPass
        {
            get => confPass;
            set
            {
                confPass = value?.Trim();
                OnPropertyChanged("Pass");
                ReginAsync.RaiseCanExecuteChanged();
            }
        }

        private bool isRemember;
        public bool IsRemember
        {
            get => isRemember;
            set
            {
                if (isRemember == value) return;
                isRemember = value;
                OnPropertyChanged("IsRemember");
            }
        }

        public bool Start()
        {
            var user = model.UserRep.Users.FirstOrDefault(u => u.RememberMe);
            if (user == null) return false;

            SelectedId = user.Id;
            SelectedName = user.Nick;
            return true;
        }
    }
}
