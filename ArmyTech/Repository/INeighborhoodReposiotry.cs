﻿using ArmyTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArmyTech.Repository
{
    public interface INeighborhoodReposiotry
    {
        Task<List<Neighborhood>> GetNeighborhoods();
    }
}