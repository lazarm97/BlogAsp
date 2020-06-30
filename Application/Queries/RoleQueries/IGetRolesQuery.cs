using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.RoleQueries
{
    public interface IGetRolesQuery : IQuery<RoleSearch, IEnumerable<RoleDto>>
    {
    }
}
