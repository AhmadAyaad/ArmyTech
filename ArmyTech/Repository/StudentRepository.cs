using ArmyTech.Dtos;
using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public class StudentRepository : IStudentRepository
    {
        readonly DataContext _context;
        public StudentRepository()
        {
            _context = new DataContext();
        }
        public StudentRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateStudent(StudentForCreateDto studentForCreateDto)
        {
            if (studentForCreateDto != null)
            {

                var selectedTeachers = _context.Teachers
                     .Where(teacher => studentForCreateDto.TeachersIndex.
                     Any(id => studentForCreateDto.TeachersIndex.Contains(teacher.ID))).ToList();

                var st = selectedTeachers.Select(t => new StudentTeacher { TeacherId = t.ID, Teacher = t });

                var student = new Student
                {
                    BirthDate = studentForCreateDto.BirthDate,
                    FieldId = studentForCreateDto.SelectedFieldId,
                    GovernorateId = studentForCreateDto.SelectedGovernateId,
                    NeighborhoodId = studentForCreateDto.SelectedNeigbhourhoodId,
                    Name = studentForCreateDto.StudentName,
                    StudentTeachers = st.ToList()
                };


                _context.Students.Add(student);


                _context.SaveChanges();


            }
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students.Include(s => s.Governorate).Include(s => s.Field)
                     .Include(s => s.Neighborhood).Include(s => s.StudentTeachers)
                     .FirstOrDefaultAsync(s => s.ID == id);
            if (student != null)
                return student;

            return new Student();
        }
        public List<StudentTeacher> GetStudentTeachers(int id)
        {
            var teachers = _context.Students
                            .Include(s => s.StudentTeachers)
                            .SelectMany(s => s.StudentTeachers)
                            .Where(s => s.StudentId == id).ToList();
            if (teachers != null)
                return teachers;
            return new List<StudentTeacher>();
        }

        public async Task<List<Student>> GetStudents()
        {
            var students = await _context.Students.Include(s => s.Governorate).Include(s => s.Neighborhood)
                    .Include(s => s.Field).Include(s => s.StudentTeachers).ToListAsync();
            if (students != null)
                return students;
            return new List<Student>();
        }

        //public void EditStudent(Student s)
        //{
        //    if (s != null)
        //    {
        //        var student = _context.Students.Find(s.ID);
        //        student.Name = s.Name;
        //        student.BirthDate = s.BirthDate;
        //        student.FieldId = s.FieldId;
        //        student.GovernorateId = s.GovernorateId;
        //        student.NeighborhoodId = s.NeighborhoodId;
        //        _context.SaveChanges();
        //    }
        //}
        public void DeleteStudent(Student s)
        {
            var student = _context.Students.Find(s.ID);
            var st = _context.StudentTeachers.Where(sts => sts.StudentId == s.ID).ToList();

            if(student !=null)
            {
                foreach (var item in st)
                {
                    _context.StudentTeachers.Remove(item);
                }
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        
        }
    }
}