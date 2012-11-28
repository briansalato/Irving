using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class DbModel
    {
        public int Id { get; set; }

        #region -Object Overrides
        public static bool operator ==(DbModel o1, DbModel o2)
        {
            if ((o1 as object) != null)
            {
                return o1.Equals(o2);
            }

            return (o2 as object) == null;
        }

        public static bool operator !=(DbModel o1, DbModel o2)
        {
            return !(o1 == o2);
        }

        public override bool Equals(object otherObj)
        {
            var otherAsDbModel = otherObj as DbModel;
            if (otherAsDbModel == null)
            {
                return false;
            }

            return this.GetHashCode() == otherObj.GetHashCode();
        }

        public override int GetHashCode()
        {
            if (Id > 0)
            {
                return (this.GetType().FullName + Id.ToString()).GetHashCode();
            }

            return base.GetHashCode();
        }
        #endregion
    }
}