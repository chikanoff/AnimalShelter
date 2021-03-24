using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using DAL.Entities;
using DAL.Repositories;
using GUI.Models;

namespace GUI.Data
{
    public class Mapping
    {
        public static List<PhotoData> GetPhotos()
        {
            PhotoRepository rep = new PhotoRepository();
            List<Photo> photos = rep.GetAll().ToList();
            List<PhotoData> newPhotos = new List<PhotoData>();
            photos.ForEach(x => newPhotos.Add(new PhotoData
            {
                Id = x.Id,
                PetId = x.PetId,
                Path = x.Path
            }));
            return newPhotos;
        }

        public static List<PetData> GetPets()
        {
            PetRepository petRep = new PetRepository();
            KindRepository kindRep = new KindRepository();
            BreedRepository breedRep = new BreedRepository();
            UserRepository userRep = new UserRepository();
            UserTypeRepository userTypeRep = new UserTypeRepository();
            OperationRepository operRep = new OperationRepository();

            List<PhotoData> photos = GetPhotos();

            List<PetData> pets = new List<PetData>();
            petRep.GetAll().ToList().ForEach(x => pets.Add(new PetData
            {
                Id = x.Id,
                Nickname = x.Nickname,
                KindId = x.KindId,
                BreedId = x.BreedId,
                Kind = kindRep.Get(x.KindId).Name,
                Breed = breedRep.Get(x.BreedId).Name,
                Conditions = x.Conditions,
                ArrivalDate = x.ArrivalDate,
                Color = x.Color,
                HealthStatus = x.HealthStatus,
                Photos = photos.Where(k => k.PetId == x.Id).ToList()
            }));

            List<OperationData> op = new List<OperationData>(operRep.GetAll().ToList().Select(x => new OperationData
            {
                Id = x.Id,
                PetId = x.PetId,
                OperationDate = x.OperationDate,
                UserId = x.UserId
            }));

            List<UserData> users = new List<UserData>();
            userRep.GetAll().ToList().ForEach(x => users.Add(new UserData
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                UserType = new UserTypeData
                {
                    Id = userTypeRep.Get(x.UserTypeId).Id,
                    Name = userTypeRep.Get(x.UserTypeId).Name
                },
                UserTypeId = x.UserTypeId,
                FullName = x.FullName,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber
            }));

            op.ForEach(x =>
                pets.Where(p => p.Id == x.PetId).ToList()
                    .ForEach(pp => pp.User = users.FirstOrDefault(u => u.Id == x.UserId)));
            
            
            return pets;
        }

        public static List<PetData> GetUserPets(int userId)
        {
            PetRepository petRep = new PetRepository();
            KindRepository kindRep = new KindRepository();
            BreedRepository breedRep = new BreedRepository();

            List<PhotoData> photos = GetPhotos();

            List<PetData> pets = new List<PetData>();
            petRep.GetUserPets(userId).ToList().ForEach(x => pets.Add(new PetData
            {
                Id = x.Id,
                Nickname = x.Nickname,
                KindId = x.KindId,
                BreedId = x.BreedId,
                Kind = kindRep.Get(x.KindId).Name,
                Breed = breedRep.Get(x.BreedId).Name,
                Conditions = x.Conditions,
                ArrivalDate = x.ArrivalDate,
                Color = x.Color,
                HealthStatus = x.HealthStatus,
                Photos = photos.Where(k => k.PetId == x.Id).ToList()
            }));
            return pets;
        }

        public static UserData GetUser(int id)
        {
            UserRepository userRep = new UserRepository();
            UserTypeRepository userTypeRep = new UserTypeRepository();
            User userDao = userRep.Get(id); 
            UserData user = new UserData
            {
                Id = userDao.Id,
                Username = userDao.Username,
                Password = userDao.Password,
                UserType = new UserTypeData
                {
                    Id = userTypeRep.Get(userDao.UserTypeId).Id,
                    Name = userTypeRep.Get(userDao.UserTypeId).Name
                },
                UserTypeId = userDao.UserTypeId,
                FullName = userDao.FullName,
                Address = userDao.Address,
                PhoneNumber = userDao.PhoneNumber
            };
            return user;
        }

        public static void DeletePhoto(PhotoData photo)
        {
            PhotoRepository photoRep = new PhotoRepository();
            photoRep.Delete(photo.Id);
        }

        public static int? AddPhoto(int id, string path)
        {
            PhotoRepository photoRep = new PhotoRepository();
            photoRep.Create(new Photo
            {
                PetId = id,
                Path = path
            });
            return photoRep.FindPhoto(id, path);
        }

        public static List<BreedData> GetBreeds()
        {
            BreedRepository breedRep = new BreedRepository();
            List<BreedData> breeds = new List<BreedData>();
            breedRep.GetAll().ToList().ForEach(x => breeds.Add(new BreedData()
            {
                Id = x.Id,
                Name = x.Name
            }));
            return breeds;
        }

        public static List<KindData> GetKinds()
        {
            KindRepository kindRep = new KindRepository();
            List<KindData> kinds = new List<KindData>();
            kindRep.GetAll().ToList().ForEach(x => kinds.Add(new KindData
            {
                Id = x.Id,
                Name = x.Name
            }));
            return kinds;
        }

        public static void AddPet(PetData pet)
        {
            PetRepository petRep = new PetRepository();
            petRep.Create(new Pet
            {
                Nickname = pet.Nickname,
                KindId = pet.KindId,
                BreedId = pet.BreedId,
                Conditions = pet.Conditions,
                ArrivalDate = pet.ArrivalDate,
                Color = pet.Color,
                HealthStatus = pet.HealthStatus
            });
        }

        public static void DeletePet(PetData pet)
        {
            PetRepository petRep = new PetRepository();
            petRep.Delete(pet.Id);
        }

        public static void EditPet(PetData pet)
        {
            PetRepository petRep = new PetRepository();
            petRep.Update(new Pet
            {
                Id = pet.Id,
                Nickname = pet.Nickname,
                KindId = pet.KindId,
                BreedId = pet.BreedId,
                Conditions = pet.Conditions,
                ArrivalDate = pet.ArrivalDate,
                Color = pet.Color,
                HealthStatus = pet.HealthStatus
            });
        }

        public static void DeleteBreed(BreedData selectedBreed)
        {
            BreedRepository breedRep = new BreedRepository();
            breedRep.Delete(selectedBreed.Id);
        }

        public static void AddBreed(BreedData breed)
        {
            BreedRepository breedRep = new BreedRepository();
            breedRep.Create(new Breed
            {
                Id = breed.Id,
                Name = breed.Name
            });
        }

        public static void RenameBreed(BreedData breed)
        {
            BreedRepository breedRep = new BreedRepository();
            breedRep.Update(new Breed
            {
                Id = breed.Id,
                Name = breed.Name
            });
        }

        public static void AddKind(string kindName)
        {
            KindRepository kindRep = new KindRepository();
            kindRep.Create(new Kind{Name = kindName});
        }

        public static void RenameKind(KindData kindData)
        {
            KindRepository kindRep = new KindRepository();
            kindRep.Update(new Kind{Id = kindData.Id, Name = kindData.Name});
        }

        public static void DeleteKind(KindData selectedKind)
        {
            KindRepository kindRep = new KindRepository();
            kindRep.Delete(selectedKind.Id);
        }

        public static void AddOperation(PetData pet, DateTime date, UserData user)
        {
            OperationRepository opRep = new OperationRepository();
            opRep.Create(new Operation
            {
                PetId = pet.Id,
                OperationDate = date.Date,
                UserId = user.Id
            });
        }

        public static List<UserData> GetUsers()
        {
            UserRepository userRep = new UserRepository();
            List<UserData> users = new List<UserData>();
            userRep.GetAll().ToList().ForEach(x => users.Add(GetUser(x.Id)));
            return users;
        }

        public static List<PetData> GetAvailablePets()
        {
            PetRepository petRep = new PetRepository();
            KindRepository kindRep = new KindRepository();
            BreedRepository breedRep = new BreedRepository();

            List<PetData> pets = new List<PetData>();
            petRep.GetAvailablePets().ToList().ForEach(x => pets.Add(new PetData
            {
                Id = x.Id,
                Nickname = x.Nickname,
                KindId = x.KindId,
                BreedId = x.BreedId,
                Kind = kindRep.Get(x.KindId).Name,
                Breed = breedRep.Get(x.BreedId).Name,
                Conditions = x.Conditions,
                ArrivalDate = x.ArrivalDate,
                Color = x.Color,
                HealthStatus = x.HealthStatus,
                Photos = GetPhotos().Where(p => p.PetId == x.Id).ToList(),
                User = null
            }));

            return pets;
        }

        public static void AddUser(UserData user)
        {
            UserRepository userRep = new UserRepository();
            userRep.Create(new User
            {
                Username = user.Username,
                Password = user.Password,
                UserTypeId = user.UserType.Id,
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            });
        }

        public static void EditUser(UserData user)
        {
            UserRepository userRep = new UserRepository();
            userRep.Update(new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                UserTypeId = user.UserType.Id,
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            });
        }

        public static void DeleteUser(User user)
        {
            UserRepository userRep = new UserRepository();
            userRep.Delete(user.Id);
        }

        public static List<UserTypeData> GetUserTypes()
        {
            UserTypeRepository userTypeRep = new UserTypeRepository();
            List<UserTypeData> userTypes = new List<UserTypeData>();
            userTypeRep.GetAll().ToList().ForEach(x => userTypes.Add(new UserTypeData
            {
                Id = x.Id,
                Name = x.Name
            }));
            return userTypes;
        }
    }
}
