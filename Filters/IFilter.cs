using AnReshWebApp.Models;
using AnReshWebApp.Models.FilterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Filters
{
    public interface IFilter<T> where T : BaseFilterEntity
    {
        T Model { get; set; }
        string Row { get; set; }


    }
}