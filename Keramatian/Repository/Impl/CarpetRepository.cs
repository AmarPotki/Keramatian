﻿using System.Collections.Generic;
using System.Linq;
using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class CarpetRepository : RepositoryBase<Carpet>, ICarpetRepository
    {
        public CarpetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IEnumerable<Carpet> GetCarpets(int priority)
        {
            if (!Database.Carpets.Any())
            {
                return new List<Carpet>();
            }

            return Database.Carpets.Where(x => x.Design.Priority == priority);
        }
        public IEnumerable<Carpet> GetCarpets(int? page, int pageSize)
        {
            if (!Database.Carpets.Any())
            {
                return new List<Carpet>();
            }
            var pageIndex = (page ?? 1) - 1;
            return Database.Carpets.OrderBy(x => x.Priority).Skip(pageSize * pageIndex).Take(pageSize);
        }
        public Carpet GetCarpetWithSize(int carpetId)
        {
            return Database.Carpets.Include("Sizes").First(x => x.Id == carpetId);
        }

        public int GetDesignCount()
        {
            var designs = from ds in Database.Carpets group ds by new { ds.Design } into g select new { name = g.Key };
            return designs.Count();
        }
        public override IEnumerable<Carpet> All()
        {
            return Database.Carpets.Include("Design");
        }
    }
}