using System;

namespace DevFreela.Application.InputModels
{
    public class UpdateUserInputModel
    {
        public int Id { get; private set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
