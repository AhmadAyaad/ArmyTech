using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArmyTech.Dtos
{
    public class StudentForCreateDto
    {
        public string StudentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FieldName { get; set; }
        public int SelectedFieldId { get; set; }
        public ICollection<Field> Fields { get; set; }
        public string NeighbourhoodName { get; set; }
        public int SelectedNeigbhourhoodId { get; set; }
        public ICollection<Neighborhood> Neighborhoods { get; set; }
        public string GovernateName { get; set; }
        public int SelectedGovernateId { get; set; }
        public ICollection<Governorate> Governorates { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public List<int> TeachersIndex { get; set; }
        public List<StudentTeacher> StudentTeachers { get; set; }
        public int StudentId { get; set; }
    }
}