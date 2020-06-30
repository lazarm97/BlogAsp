using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Tags
{
    public interface IGetTagsQuery : IQuery<TagSearch, IEnumerable<TagDto>>
    {
    }
}
