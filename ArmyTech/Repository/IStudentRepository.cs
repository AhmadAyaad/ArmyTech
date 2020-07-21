using ArmyTech.Dtos;
using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public interface IStudentRepository
    {
        void CreateStudent(StudentForCreateDto studentForCreateDto);
        Task<List<Student>> GetStudents();
        Task<Student> GetStudent(int id);
    
        void DeleteStudent(Student student);
    }
}