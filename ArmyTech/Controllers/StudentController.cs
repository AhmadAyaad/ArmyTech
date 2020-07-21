using ArmyTech.Dtos;
using ArmyTech.Models;
using ArmyTech.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArmyTech.Controllers
{
    public class StudentController : Controller
    {
        readonly StudentRepository _studentRepository;
        readonly FieldRepository _fieldRepository;
        readonly GovernateRepostiory _governateRepostiory;
        readonly NeighborhoodRepository _neighborhoodRepository;
        readonly TeacherReposiotry _teacherReposiotry;
        DataContext _context;
        public StudentController()
        {
            _studentRepository = new StudentRepository();
            _fieldRepository = new FieldRepository();
            _governateRepostiory = new GovernateRepostiory();
            _neighborhoodRepository = new NeighborhoodRepository();
            _teacherReposiotry = new TeacherReposiotry();
            _context = new DataContext();
            //_studentRepository = new StudentRepository(_context);
        }

        // GET: Student
        public async Task<ActionResult> Index()
        {
            var students = await _studentRepository.GetStudents();

            return View(students);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {

            StudentForCreateDto studentForCreateDto = new StudentForCreateDto();
            studentForCreateDto.Fields = await _fieldRepository.GetFields();
            studentForCreateDto.Governorates = await _governateRepostiory.GetGovernorates();
            studentForCreateDto.Neighborhoods = await _neighborhoodRepository.GetNeighborhoods();
            studentForCreateDto.Teachers = await _teacherReposiotry.GetTeachers();

            ViewBag.Teachers = new MultiSelectList(studentForCreateDto.Teachers, "ID", "Name");

            return View(studentForCreateDto);
        }

        [HttpPost]
        public ActionResult Create(StudentForCreateDto studentForCreateDto)
        {
            _studentRepository.CreateStudent(studentForCreateDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetStudent(id);
            if (student != null)
            {
                StudentForCreateDto studentForCreateDto = new StudentForCreateDto
                {
                    BirthDate = student.BirthDate,
                    FieldName = student.Field.Name,
                    SelectedFieldId = student.Field.ID,
                    StudentName = student.Name,
                    StudentId = id,
                    GovernateName = student.Governorate.Name,
                    SelectedGovernateId = student.Governorate.ID,

                    NeighbourhoodName = student.Neighborhood.Name,
                    SelectedNeigbhourhoodId = student.Neighborhood.ID,
                    Fields = await _fieldRepository.GetFields(),
                    Neighborhoods = await _neighborhoodRepository.GetNeighborhoods(),
                    Governorates = await _governateRepostiory.GetGovernorates(),
                    Teachers = await _teacherReposiotry.GetTeachers(),
                    StudentTeachers = _studentRepository.GetStudentTeachers(id),

                };

                ViewBag.Teachers = new MultiSelectList(studentForCreateDto.Teachers, "ID", "Name");
                return View(studentForCreateDto);

            }
            return View();

        }



        [HttpPost]
        public ActionResult Edit(StudentForCreateDto studentForCreateDto)
        {
            if (ModelState.IsValid)
            {
                var selectedTeachers = _context.Teachers
                    .Where(teacher => studentForCreateDto.TeachersIndex.
                    Any(id => studentForCreateDto.TeachersIndex.Contains(teacher.ID))).ToList();

                var st = selectedTeachers.Select(t => new StudentTeacher
                {
                    TeacherId = t.ID,
                    Teacher = t,
                    StudentId = studentForCreateDto.StudentId,
                    Student = _context.Students.Find(studentForCreateDto.StudentId)
                });

                var studentt = new Student
                {
                    BirthDate = studentForCreateDto.BirthDate,
                    Name = studentForCreateDto.StudentName,
                    FieldId = studentForCreateDto.SelectedFieldId,
                    GovernorateId = studentForCreateDto.SelectedGovernateId,
                    NeighborhoodId = studentForCreateDto.SelectedNeigbhourhoodId,
                    StudentTeachers = st.ToList(),
                    ID = studentForCreateDto.StudentId
                };

                List<StudentTeacher> ss = new List<StudentTeacher>
                {
                    new StudentTeacher{StudentId = studentt.ID , TeacherId =0}
                };
                var student = _context.Students.Find(studentt.ID);
                student.Name = studentt.Name;
                student.BirthDate = studentt.BirthDate;
                student.FieldId = studentt.FieldId;
                student.GovernorateId = studentt.GovernorateId;
                student.NeighborhoodId = studentt.NeighborhoodId;
                student.StudentTeachers = st.ToList();
                
                    _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();

        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetStudent(id);
            if (student != null)
            {
                _studentRepository.DeleteStudent(student);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}