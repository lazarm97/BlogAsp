using Application.DataTransfer;
using Application.Queries.Paginations;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.PostQueries
{
    public interface IGetPostsQuery : IQuery<PostSearch, PagedResponse<PostShowDto>>
    {
    }
}
