using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DAL.Entities;
using DAL.Repositories;
using GUI.Data;

namespace GUI.Models
{
    public class ManagerModel
    {

        public UserData GetUser(int id)
        {
            return Mapping.GetUser(id);
        }
        public ObservableCollection<PetData> GetPets()
        {
            return new ObservableCollection<PetData>(Mapping.GetPets());
        }

        public ObservableCollection<BreedData> GetBreeds()
        {
            return new ObservableCollection<BreedData>(Mapping.GetBreeds());
        }
        public ObservableCollection<KindData> GetKinds()
        {
            return new ObservableCollection<KindData>(Mapping.GetKinds());
        }

        public void AddPet(PetData pet)
        {
            Mapping.AddPet(pet);
        }

        public void DeletePet(PetData pet)
        {
            Mapping.DeletePet(pet);
        }

        public void EditPet(PetData pet)
        {
            Mapping.EditPet(pet);
        }
        public void DeletePhoto(PhotoData photo)
        {
            Mapping.DeletePhoto(photo);
        }

        public int? AddPhoto(int petId, string path)
        {
            return Mapping.AddPhoto(petId, path);
        }

        public void DeleteBreed(BreedData selectedBreed)
        {
            Mapping.DeleteBreed(selectedBreed);
        }

        public void AddBreed(string breedName)
        {
            Mapping.AddBreed(new BreedData {Name = breedName});
        }
        public void RenameBreed(BreedData breed, string newBreedName)
        {
            Mapping.RenameBreed(new BreedData{ Id = breed.Id, Name = newBreedName});
        }

        public void AddKind(string kindName)
        {
            Mapping.AddKind(kindName);
        }

        public void RenameKind(KindData selectedKind, string newKindName)
        {
            Mapping.RenameKind(new KindData{Id = selectedKind.Id, Name = newKindName});
        }

        public void DeleteKind(KindData selectedKind)
        {
            Mapping.DeleteKind(selectedKind);
        }

        public List<PetData> GetAvailablePets()
        {
            return Mapping.GetAvailablePets();
        }

        public List<UserData> GetUsers()
        {
            return Mapping.GetUsers();
        }
    }
}
