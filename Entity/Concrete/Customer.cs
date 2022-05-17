using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Customer: User, IEntity
    {
        //Change Customer table's Id Column into UserId.
        public string CompanyName { get; set; }
    }
}
