using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DAL.Repositories;
using GUI.Data;

namespace GUI.Models
{
    public class GuestModel
    {
        public GuestModel()
        {
        }

        public ObservableCollection<PetData> GetPets()
        {
            return new ObservableCollection<PetData>(Mapping.GetPets());
        }
    }
}
