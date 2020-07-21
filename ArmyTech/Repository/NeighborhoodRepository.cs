using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public class NeighborhoodRepository : INeighborhoodReposiotry
    {
        readonly DataContext _context;
        public NeighborhoodRepository()
        {
            _context = new DataContext();
        }
        public async Task<List<Neighborhood>> GetNeighborhoods()
        {
            var neighborhoods = await _context.Neighborhoods.ToListAsync();
            if (neighborhoods != null)
                return neighborhoods;
            return new List<Neighborhood>();
        }
    }
}