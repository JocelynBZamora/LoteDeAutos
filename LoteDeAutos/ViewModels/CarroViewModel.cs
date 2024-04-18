using LoteDeAutos.Data;
using LoteDeAutos.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoteDeAutos.ViewModels
{
    public class CarroViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CarroModel> Autos { get; set; }
        CarrosDBContext data = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public CarroModel NuevoAuto { get; set; } = new();
        public ICommand AgregarCommand => new AsyncCommand(Agregar);
        public ICommand ActualizarCommand => new AsyncCommand(Actualizar);
        public ICommand DetallesCommand => new AsyncCommand<int>(Detalles);
        public ICommand EliminarCommand => new AsyncCommand<int>(Eliminar);

        private async Task Detalles(int id)
        {
            NuevoAuto = await data.GetById(id);
            OnPropertyChanged(nameof(NuevoAuto));
        }

        private async Task Actualizar()
        {
            var autoact = await data.Actualizar(NuevoAuto);
            if (autoact != null)
            {
                llenarCarroos();

                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }


        private async Task Eliminar(int id)
        {
            var eliminado = await data.Eliminar(id);
            if (eliminado == true)
            {
                llenarCarroos();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public CarroViewModel()
        {
            llenarCarroos();
        }

        private async void llenarCarroos()
        {
            Autos = new(await data.GetAll());

        }

        private async Task Agregar()
        {
            await data.Add(NuevoAuto);
            llenarCarroos();
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        public void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}