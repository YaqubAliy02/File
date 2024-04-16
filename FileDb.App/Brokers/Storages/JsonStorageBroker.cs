//----------------------------------------
// Tarteeb School (c) All rights reserved |
//----------------------------------------
using FileDb.App.Brokers.Storages;
using FileDb.App.Models.Users;
using System.Text.Json;

namespace FileDB.App.Brokers.Storages
{
    internal class JsonStorageBroker : IStorageBroker
    {
        private const string FilePath = "../../../Assets/Jsons/Users.json";

        public JsonStorageBroker()
        {
            EnsureFileExists();
        }

        public User AddUser(User user)
        {

            string usersString = File.Exists(FilePath) ? File.ReadAllText(FilePath) : "[]";
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);
            users.Add(user);
            string serializedUsers = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, serializedUsers);

            return user;
        }

        public List<User> ReadAllUsers()
        {
            string usersString = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);

            return users;
        }

        public User UpdateUser(User user)
        {
            string usersString = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);
            User updatedUser = users.FirstOrDefault(u => u.Id == user.Id);
            updatedUser.Name = user.Name;

            string serializedUsers = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, serializedUsers);

            return updatedUser;
        }

        public bool DeleteUser(int id)
        {
            string usersString = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);
            User user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
            string serializedUsers = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, serializedUsers);

            return true;
        }

        private void EnsureFileExists()
        {
            bool fileExists = File.Exists(FilePath);
            if (fileExists is false)
            {
                File.Create(FilePath).Close();
                File.WriteAllText(FilePath, "[]");
            }

        }
    }
}