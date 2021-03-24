using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DAL.Repositories;
using GUI.Data;

namespace GUI.Models
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserData GetUser(int id)
        {
            return Mapping.GetUser(id);
        }

        public ObservableCollection<PetData> GetUserPets(int userId)
        {
            return new ObservableCollection<PetData>(Mapping.GetUserPets(userId));
        }

        public void DeletePhoto(PhotoData photo)
        {
            Mapping.DeletePhoto(photo);
        }

        public int? AddPhoto(int petId, string path)
        {
            return Mapping.AddPhoto(petId, path);
        }
    }
}
