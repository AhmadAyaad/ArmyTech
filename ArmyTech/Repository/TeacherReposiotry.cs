using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public class TeacherReposiotry : ITeacherReposiotry
    {
        readonly DataContext _context;
        public TeacherReposiotry()
        {
            _context = new DataContext();
        }
        public async Task<List<Teacher>> GetTeachers()
        {
            var teachers = await _context.Teachers.ToListAsync();
            if (teachers != null)
                return teachers;
            return new List<Teacher>();
        }

    }
}