using System;
using System.Collections.Generic;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullname, string email, DateTime birthDate)
        {
            Fullname = fullname;
            Email = email;
            BirthDate = birthDate;
            Active = true;

            CreatedAt = DateTime.Now;
            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();
        }

        public string Fullname { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool Active { get; private set; }


        public List<UserSkill> Skills { get; private set; }

        public List<Project> OwnedProjects { get; private set; }

        public List<Project> FreelanceProjects { get; private set; }


        public void ActiveUser()
        {
            Active = true;
        }

        public void DeactiveUser()
        {
            Active = false;
        }

        public void Update(string fullname, string email, DateTime birthdate)
        {
            Fullname = fullname;
            Email = email;
            BirthDate = birthdate;
        }
    }
}
