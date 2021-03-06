﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using J_LinkMemDump.Model;
using J_LinkMemDump.Properties;

namespace J_LinkMemDump.ViewModel
{
    public sealed class VarViewModel : INotifyPropertyChanged
    {
        public string Output
        {
            get => _mOutput;
            set
            {
                if (value == _mOutput)
                    return;
                _mOutput = value;
                OnPropertyChanged();
            }
        }

        private string _mOutput;

        public string Address
        {
            get => _mAddress;
            set
            {
                if (value == _mAddress)
                    return;
                _mAddress = value;
                OnPropertyChanged();
            }
        }

        private string _mAddress;

        public string Size
        {
            get => _mSize;
            set
            {
                if (value == _mSize)
                    return;
                _mSize = value;
                OnPropertyChanged();
            }
        }

        private string _mSize;

        public VarViewModel()
        {
            Output = "Output hehehere";
            Address = "0x20004534";
            Size = "0x400";
            _canExecute = true;
        }

        private ICommand _dumpCommand;
        public ICommand DumpCommand => _dumpCommand ?? (_dumpCommand = new CommandHandler(DumpAction, _canExecute));

        private readonly bool _canExecute;

        private async void DumpAction() => Output = await DumpVar.GetDataFromJlinkAsync(Size, Address);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class CommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly bool _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute;

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter) => _action();
    }
}