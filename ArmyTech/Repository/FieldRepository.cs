using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public class FieldRepository : IFieldRepository
    {
        readonly DataContext _context;
        public FieldRepository()
        {
            _context = new DataContext();
        }
        public async Task<List<Field>> GetFields()
        {
            var fields = await _context.Fields.ToListAsync();
            if (fields != null)
                return fields;
            return new List<Field>();

        }
    }
}