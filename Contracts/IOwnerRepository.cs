﻿using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
        IEnumerable<OwnerList> GetOwnerList();
        Owner GetOwnerById(Guid ownerId);
        OwnerExtended GetOwnerWithDetails(Guid ownerId);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner dbOwner, Owner owner);
        void DeleteOwner(Owner owner);
    }
}
