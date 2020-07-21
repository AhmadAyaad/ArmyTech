using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public class GovernateRepostiory : IGovernateRepository
    {
        readonly DataContext _context;
        public GovernateRepostiory()
        {
            _context = new DataContext();
        }
        public async Task<List<Governorate>> GetGovernorates()
        {
            var governates = await _context.Governorates.ToListAsync();
            if (governates != null)
                return governates;
            return new List<Governorate>();
        }
    }
}