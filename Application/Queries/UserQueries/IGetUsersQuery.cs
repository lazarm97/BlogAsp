using Application.DataTransfer;
using Application.Queries.Paginations;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.UserQueries
{
    public interface IGetUsersQuery : IQuery<UserSearch, PagedResponse<UserShowDto>>
    {
    }
}
